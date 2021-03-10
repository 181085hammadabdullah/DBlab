using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_App.Models
{
    public class Cart_Entries
    {
        public string Product_id;
        public string Product_Name;
        public string Description;
        public string MAX_Quantity;
        public string Unit_price;
        public string quantity;
        public int id;
        public int total;
        public int grandtotal;
        public int Number;
    }
    public class Cart_Entries_With_Payments
    {
        public List<Cart_Entries> Cart_Products;
        public List<Payment> payments;
    }
}

