using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_App.Models
{
    public class Feedback
    {
        public int CustomerID;
        public int invoice_no;
        public string Date_Time;
        public string Feed_back;
        public string Email;
        public string First_Name;
        public string Last_Name;
        public string Phone;
        public string Address;

    }
    //Customer
    public class feedback
    {
        public string invoice_num { get; set; }
        public string feed { get; set; }
    }
}

