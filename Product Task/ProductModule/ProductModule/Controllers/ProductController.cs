using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductModule.Models;
using System.IO;
using System.Web.Security;
using System.Xml;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;

using System.Data.Sql;



namespace ProductModule.Controllers
{
    public class ProductController : Controller
    {

        Product Productmodel = new Product();
        //
        // GET: /Product/

        public ActionResult Product()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveProduct(string ItemName, string Description, decimal Price, int Quantity, int Tax, int Status)
        {


            return new JsonResult { Data = Productmodel.SaveProduct(ItemName, Description, Price, Quantity, Tax, Status) };
        }

        [HttpPost]
        public ActionResult ProductDetails()
        {

            return new JsonResult { Data = Productmodel.ProductDetails() };

        }

        [HttpPost]
        public ActionResult ExcelUpload(HttpPostedFileBase file)
        {
            DataSet ds = new DataSet();
            if (Request.Files["file"].ContentLength > 0)
            {
                string fileExtension = System.IO.Path.GetExtension(Request.Files["file"].FileName);

                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files["file"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {

                        System.IO.File.Delete(fileLocation);
                    }
                    Request.Files["file"].SaveAs(fileLocation);
                    string excelConnectionString = string.Empty;
                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                    fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    
                    if (fileExtension == ".xls")
                    {
                        excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                   
                    else if (fileExtension == ".xlsx")
                    {
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                   
                    OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    excelConnection.Open();
                    DataTable dt = new DataTable();

                    dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt == null)
                    {
                        return null;
                    }

                    String[] excelSheets = new String[dt.Rows.Count];
                    int t = 0;
                    
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[t] = row["TABLE_NAME"].ToString();
                        t++;
                    }
                    OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);


                    string query = string.Format("Select * from [{0}]", excelSheets[0]);
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    {
                        dataAdapter.Fill(ds);
                    }
                }
                if (fileExtension.ToString().ToLower().Equals(".xml"))
                {
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files["FileUpload"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }

                    Request.Files["FileUpload"].SaveAs(fileLocation);
                    XmlTextReader xmlreader = new XmlTextReader(fileLocation);
                   
                    ds.ReadXml(xmlreader);
                    xmlreader.Close();
                }

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    
                    SqlConnection Con = new SqlConnection();
                    Con.ConnectionString = "data source=DESKTOP-2TF0GND\\SQLEXPRESS;initial catalog=ProductDB;integrated security=True;multipleactiveresultsets=True;App=EntityFramework&quot;";
                    
                    string query = "Insert into Product(ItemName,Descriptions,Price,Quantity,Tax,Status) Values('" +
                    ds.Tables[0].Rows[i][0].ToString() + "','" + ds.Tables[0].Rows[i][1].ToString() +
                    "','" + ds.Tables[0].Rows[i][2].ToString() + "','" + ds.Tables[0].Rows[i][3].ToString() + "','" + ds.Tables[0].Rows[i][4].ToString() + "',0)";
                    Con.Open();
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                }
            }
           // return View();
            return Json("Uploaded successfully");  
        }


        [HttpPost]
        public ActionResult UpdateProduct(int ItemId, string ItemName, string Descriptions, decimal Price, int Quantity, int Tax)
        {

            return new JsonResult { Data = Productmodel.UpdateProduct(ItemId, ItemName, Descriptions, Price, Quantity, Tax) };

        }

        [HttpPost]
        public ActionResult DeleteProduct(int Pid)
        {
            return new JsonResult { Data = Productmodel.DeleteProduct(Pid) };
        }

       
    }
}
