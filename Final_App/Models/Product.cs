using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_App.Models
{
    public class Product
    {
        public string ID { get; set; }
    }
    public class CategoryWithProduct
    {
        public Product1 product;
        public List<Category> Categories;
    }
    //Seller Product
    public class Product1
    {
        public int ProductID;
        public string Product_Name;
        public int Unit_price;
        public int Stock;
        public string City;
        public string Date_Time_of_Entry;
        public string Manufacturer;
        public string Model;
        public string Released_Date;
        public int CategoryID;
        public int SellerID;
    }
    public class ProductList
    {

        public List<Product1> products;

    }
}

