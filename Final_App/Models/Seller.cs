using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_App.Models
{
    public class Seller
    {
        public int SellerID;
        public string Password;
        public string Name;
        public string Gender;
        public string Address;
        public string ContactNo;
        public string DOB;
        public string Email;
    }
    public class CurrentSeller
    {
        public int SellerID;
        public string Password;
        public string Name;
        public string Gender;
        public string City;
        public string Address;
        public string ContactNo;
        public string DOB;
        public string Email;
    }
    public class Blocked_Seller
    {
        public int SellerID;
        public string Date_time;
        public string Name;
        public string Email;
        public string Gender;
    }
}

