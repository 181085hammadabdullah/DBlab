using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_App.Models
{
    public class Invoice
    {
        public int invoiceno;
        public string date;
        public int grand_total;
        public string payment;
        public List<Invoice_Product> products;
        public invoice_customer_Info customer;
    }

    public class Invoice_Admin
    {
        public int invoiceno;
        public string date;
        public int grand_total;
        public string Email;
        public List<Invoice_Product> products;
    }
    public class Invoice_Cust
    {
        public int invoiceno;
        public string Email;
    }
}
