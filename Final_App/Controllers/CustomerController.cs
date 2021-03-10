using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_App.Models;
using System.Threading;

namespace Final_App.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        //FUNCTIONS

        //SIMPLE
        //Cart To Invoice
        public ActionResult Cart_to_Invoice(Product obj)
        {

            if (Session["cust_email"] == null)
            {
                return View("CustLogin");
            }
            else
            {
                string c_user = Session["cust_email"].ToString();
                Customer_Functions.populate_invoice_items(c_user, obj.ID);
                return RedirectToAction("Cart_Empty");
            }

        }
        //Filter Catgeory
        public ActionResult Filter_Category(string select, string upper, string lower, string p_name, string p_location)
        {
            if (select == null && upper == "" && lower == "" && p_name == "" && p_location == "")
            {
                return RedirectToAction("CustStore");
            }
            List<Store> product;
            List<Category> categories = Customer_Functions.Show_All_Categories();
            Store_With_Categories obj = new Store_With_Categories();

            obj.Categories = categories;
            if (select != null && upper == "" && lower == "" && p_name == "" && p_location == "")
            {
                product = Customer_Functions.Search_by_categ(select);
                if (product == null || product.Count == 0)
                {
                    return View("Empty_Store_Search", obj);
                }
                obj.Store_Products = product;
                return View("CustStore", obj);
            }
            if (select == null && upper == "" && lower == "" && p_name == "" && p_location == "")
            {
                return View("Empty_Store_Search", obj);
            }
            if (upper == "" || lower == "")
            {
                return View("Empty_Store_Search", obj);
            }
            product = Customer_Functions.filter_search(select, upper, lower, p_name, p_location);
            if (product == null || product.Count == 0)
            {
                return View("Empty_Store_Search", obj);
            }
            else
            {
                obj.Store_Products = product;
                return View("CustStore", obj);
            }

        }
        //Cust Sign In
        public ActionResult authenticateSignin(string email, string password)
        {
            int result = Customer_Functions.Signin(email, password);

            if (result == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return View("CustLogin", (object)data);
            }
            else if (result == 0)
            {

                String data = "Wrong Credentials!";
                return View("CustLogin", (object)data);
            }
            else if (result == 2)
            {
                String data = "User Not Found with these Credentials";
                return View("CustLogin", (object)data);
            }
            ViewBag.id = 1;
            Session["cust_email"] = email;
            return RedirectToAction("CustHome");
        }
        //Cust Sign Up
        public ActionResult authenticateSignup(string Fname, string Lname, string Phone, string gender, string email, string password)
        {


            int result = Customer_Functions.Signup(Fname, Lname, Phone, gender, email, password);

            if (result == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return View("CustReg", (object)data);
            }
            else if (result == 0)
            {

                String data = "User Already Exists";
                return View("CustReg", (object)data);
            }
            Session["cust_email"] = email;
            return RedirectToAction("CustHome");
        }
        //Cust Update Profile
        public ActionResult updateCusProfile(string Fname, string Lname, string Email,
        string address, string city, string zipcode, string password1, string password2)
        {
            if (password1 != password2)
            {
                return RedirectToAction("CustProfile");
            }
            int result = Customer_Functions.UpdateCustProfile(Fname, Lname, Email, address, city, zipcode, password1, password2);
            if (result == -1)
            {
                return View("CustProfile");
            }
            else if (result == 0)
            {
                return View("CustProfile");
            }
            Thread.Sleep(2000);
            return RedirectToAction("CustProfile");
        }

        //WITH VIEWS
        //Profile Without Notifications (View)
        public ActionResult Profile_without_notifications()
        {
            if (Session["cust_email"] == null)
            {
                return RedirectToAction("CustLogin");
            }
            else
            {
                string c_user = Session["cust_email"].ToString();
                CurrentUser user = Customer_Functions.Current_LoggedIN_User(c_user);
                if (user.address == "")
                {
                    user.address = "Address";
                }
                if (user.city == "")
                {
                    user.city = "City";
                }
                if (user.zipcode == "")
                {
                    user.zipcode = "Zip";
                }
                return View(user);
            }

        }
        //Store With No Products (View)
        public ActionResult Store_with_no_products()
        {
            return View();
        }
        //Empty Store Search (View)
        public ActionResult Empty_Store_Search()
        {
            return View();
        }
        //Cart Empty (View)
        public ActionResult Cart_Empty()
        {
            return View();
        }
        //Empty Home (View)
        public ActionResult Empty_Home()
        {
            return View();
        }
        //Cust Home (View)
        public ActionResult CustHome()
        {
            if (Session["cust_email"] == null)
            {
                return View("CustLogin");
            }
            else
            {
                List<Invoice> invoices = new List<Invoice>();
                string c_user = Session["cust_email"].ToString();

                List<int> arr = Customer_Functions.get_invoice_information(c_user);
                if (arr.Count != 0)
                {
                    for (int i = 0; i < arr.Count; i++)
                    {
                        Customer_Functions.get_invoices(invoices, arr[i]);
                        Customer_Functions.get_payment_from_Invoice(invoices, i, arr[i]);
                        Customer_Functions.get_invoice_customer(invoices, i, c_user);
                    }
                    return View("CustHome", invoices);
                }
                else
                {
                    return View("Empty_Home");
                }
            }
        }
        //Cust Reg (View)
        public ActionResult CustReg()
        {
            return View();
        }
        //Cust Login (View)
        public ActionResult CustLogin()
        {
            return View();
        }
        //Add to cart (View)
        public ActionResult AddtoCart()
        {
            if (Session["cust_email"] == null)
            {
                return View("CustLogin");
            }
            else
            {
                string c_user = Session["cust_email"].ToString();
                List<Cart_Entries> lst = Customer_Functions.get_cart_items(c_user);
                List<Payment> payments1 = Admin_Functions.Show_All_Payments();
                if (lst == null || lst.Count == 0)
                {
                    return View("Cart_Empty");
                }
                Cart_Entries_With_Payments obj = new Cart_Entries_With_Payments();
                obj.Cart_Products = lst;
                obj.payments = payments1;
                return View(obj);
            }

        }
        //Cust Profile (View)
        public ActionResult CustProfile()
        {
            if (Session["cust_email"] == null)
            {
                return View("CustLogin");
            }
            else
            {
                Profile_with_notifications user = new Profile_with_notifications();
                string c_user = Session["cust_email"].ToString();
                user.c = Customer_Functions.Current_LoggedIN_User(c_user);
                if (user.c.address == "")
                {
                    user.c.address = "Address";
                }
                if (user.c.city == "")
                {
                    user.c.city = "City";
                }
                if (user.c.zipcode == "")
                {
                    user.c.zipcode = "Zip";
                }
                user.n = Customer_Functions.get_notifications();
                if (user.n == null || user.n.Count == 0)
                {
                    return RedirectToAction("Profile_without_notifications");
                }
                else
                {
                    return View(user);
                }

            }

        }
        //Cust Store (View)
        public ActionResult CustStore()
        {
            if (Session["cust_email"] == null)
            {
                return View("CustLogin");
            }
            else
            {
                List<Store> product;
                product = Customer_Functions.get_product_info();
                if (product == null || product.Count == 0)
                {
                    return RedirectToAction("Store_with_no_products");
                }
                List<Category> categories = Customer_Functions.Show_All_Categories();
                if (categories == null || product.Count == 0)
                {
                    return RedirectToAction("Store_with_no_products");
                }
                Store_With_Categories obj = new Store_With_Categories();
                obj.Store_Products = product;
                obj.Categories = categories;
                return View(obj);
            }
        }

        //WITH HTTP REQUEST
        //Add Feedback (HTTP)
        [HttpPost]
        public void Feedback(feedback obj)
        {

            Thread.Sleep(2000);
            string c_user = Session["cust_email"].ToString();
            Customer_Functions.add_feedback_to_db(obj, c_user);
            RedirectToAction("CustHome");
        }
        //Update Quantity (HTTP)
        [HttpPost]
        public ActionResult Update_Quantity(Quantity obj)
        {
            if (Session["cust_email"] == null)
            {
                return View("CustLogin");
            }
            else
            {
                string c_user = Session["cust_email"].ToString();
                Customer_Functions.update_quantity(obj, c_user);
                return View("AddtoCart");
            }
        }
        //Add To cart (HTTP)
        [HttpPost]
        public ActionResult AddtoCart(Product obj)
        {
            if (Session["cust_email"] == null)
            {
                return View("CustLogin");
            }
            else
            {
                string c_user = Session["cust_email"].ToString();
                Customer_Functions.Add_to_Cart(obj, c_user);
                List<Cart_Entries> lst = Customer_Functions.get_cart_items(c_user);
                if (lst == null || lst.Count == 0)
                {
                    return View("Cart_Empty");
                }
                return View(lst);
            }

        }
        //Execulde Cart Item (HTTP)
        [HttpPost]
        public ActionResult exclude_cart_item(Product obj)
        {
            if (Session["cust_email"] == null)
            {
                return View("CustLogin");
            }
            else
            {
                string c_user = Session["cust_email"].ToString();
                Customer_Functions.remove_cart_product(obj, c_user);
                List<Cart_Entries> lst = Customer_Functions.get_cart_items(c_user);
                if (lst == null || lst.Count == 0)
                {
                    return View("Cart_Empty");
                }
                return View("AddtoCart", lst);
            }
        }
    }
}
