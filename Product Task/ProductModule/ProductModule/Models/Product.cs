using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business;
using System.Collections;

namespace ProductModule.Models
{

    public class test
    {
        public static string productName;


    }

    public class Product
    {

        ProductBL Pm = new ProductBL();
        public string SaveProduct(string ItemName, string Description, decimal Price, int Quantity, int Tax, int Status)
        {
            return Pm.SaveProduct(ItemName, Description, Price, Quantity, Tax, Status);


        }
        public object ProductDetails()
        {

            return Pm.ProductDetails();

        }

        public object UpdateProduct(int ItemId, string ItemName, string Descriptions, decimal Price, int Quantity, int Tax)
        {

            return Pm.UpdateProduct(ItemId, ItemName, Descriptions, Price, Quantity, Tax);

        }
        public object DeleteProduct(int Pid)
        {
            return Pm.DeleteProduct(Pid);
        }

    }
}