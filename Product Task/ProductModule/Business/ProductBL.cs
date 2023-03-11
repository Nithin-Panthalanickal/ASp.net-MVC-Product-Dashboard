using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Data;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;
using System.Configuration;


namespace Business
{
    public class ProductBL
    {

        ProductDBEntities PE = new ProductDBEntities();
        public string SaveProduct(string ItemName, string Description, decimal Price, int Quantity, int Tax, int Status)
        {

            string Res = "Failed to Save";
            bool? result = PE.Sp_SaveProduct(ItemName, Description, Price, Quantity, Tax, Status).Single();
            if (result == true)
            {
                Res = "Successfuly Saved";
            }
            return Res;

            
        }

        public object ProductDetails()
        {

            var result = PE.Sp_GetProductDetails().ToList<Sp_GetProductDetails_Result>();
            return result;

        }

        public object UpdateProduct(int ItemId, string ItemName, string Descriptions, decimal Price, int Quantity, int Tax)
        {
            string Res = "Update Failed";
            bool? result = PE.Sp_UpdateProductDtls(ItemId, ItemName, Descriptions, Price, Quantity, Tax).Single();
            if (result == true)
            {
                Res = "Updated Successfully";
            }
            return Res;

        }

        public object DeleteProduct(int Pid)
        {
            string Res = "Delete Failed";
            bool? result = PE.Sp_DeleteProduct(Pid).Single();
            if (result == true)
            {
                Res = "Deleted Successfully";
            }
            return Res;

        }
    }
}
