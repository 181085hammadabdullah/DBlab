using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_App.Models
{
    public class Customer
    {
        public int CustomerID;
        public string FIrst_Name;
        public string Last_Name;
        public string Phone;
        public string Gender;
        public string Email;
        public string Password;
        public string Zipcode;
        public string Address;
        public string City;


    }
    //Customer Side
    public class CurrentUser
    {
        public string Fname;
        public string Lname;
        public string Phone;
        public string gender;
        public string email;
        public string password;
        public string address;
        public string city;
        public string zipcode;
    }
    public class Customer_Signup
    {
        public string Fname;
        public string Lname;
        public string Phone;
        public string gender;
        public string email;
        public string password;
    }
}

