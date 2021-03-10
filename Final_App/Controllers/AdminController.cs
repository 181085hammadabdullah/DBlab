using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_App.Models;
namespace Final_App.Controllers
{
    public class AdminController : Controller
    {
        //Index Function
        public ActionResult Index()
        {
            return View();
        }

        //DATABASE FUNCTIONS

        //Admin SignUp
        public ActionResult Admin_SignUp(string email, string Password, string First_Name, string Last_Name)
        {


            int result = Admin_Functions.Admin_SignUp(email, Password, First_Name, Last_Name);

            if (result == -1)
            {

                String data = "Something went wrong while connecting with the database.";
                return View("SignUp", (object)data);
            }
            else if (result == 0)
            {

                String data = "Valiadtion Errors";
                return View("SignUp", (object)data);
            }

            else if (result == 2)
            {
                String data = "* Email Already Exists";
                return View("SignUp", (object)data);
            }
            //Sample View
            return RedirectToAction("AdminLogin");

        }
        //Admin Login
        public ActionResult Admin_Login(string email, string password)
        {

            int result = Admin_Functions.Admin_Login(email, password);

            if (result == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return View("AdminLogin", (object)data);
            }
            else if (result == 0)
            {

                String data = "Invalid Email Or Password";
                return View("AdminLogin", (object)data);
            }


            //Sucessfully Login
            Session["Admin_Email"] = email;
            return RedirectToAction("AdminHome");


        }
        //Admin Complete Profile
        public ActionResult Admin_Update_Profile(string First_Name, string Last_Name,
           string new_email, string new_Password, string phone, string cnic,
            string Gender, string Qualification, string Address, string DOB)
        {
            if (Session["Admin_Email"] != null)
            {

                string old_email = Session["Admin_Email"].ToString();
                if (DOB == "")
                    DOB = "2001/12/8";
                int result = Admin_Functions.Update_Admin_Profile(old_email, new_email, new_Password,
                               First_Name, Last_Name, Gender, phone, cnic, Qualification, DOB, Address);

                if (result == -1)
                {
                    String data = "Something went wrong while connecting with the database.";
                    return View("AdminHome");
                }
                else if (result == 0)
                {

                    String data = "Valiadtion Errors";
                    return View("AdminHome");
                }
                //Sucessfully Updated
                Session["Admin_Email"] = new_email;
                return View("AdminHome");
            }
            else
            {

                return View("AdminLogin");
            }

        }
        //Add New Category
        public ActionResult Add_New_Category(string name, string description)
        {
            if (Session["Admin_Email"] != null)
            {
                int result = Admin_Functions.Add_New_Category(name, description);

                if (result == -1)
                {
                    String data = "Database Connection Problem";
                    return View("AddNewCategory", (object)data);
                }
                else if (result == 0)
                {

                    String data = "Category With This Name Already Exists";
                    return View("AddNewCategory", (object)data);
                }
                //Sucessfully Added
                return RedirectToAction("ShowCategories");
            }
            else
            {
                return View("AdminLogin");
            }
        }
        //Delete A Category
        public ActionResult deleteCategory(int Id)
        {
            if (Session["Admin_Email"] != null)
            {
                int result = Admin_Functions.Delete_Category(Id);
                return RedirectToAction("ShowCategories");
            }
            else
            {
                return View("AdminLogin");
            }
        }
        //Update A Category
        public ActionResult UpdateCategory(int Id, string name, string description)
        {
            if (Session["Admin_Email"] != null)
            {
                int result = Admin_Functions.Update_Category(Id, name, description);
                return RedirectToAction("ShowCategories");
            }
            else
            {

                return View("Admin_Login");
            }

        }
        //Add New Payment
        public ActionResult Add_New_Payments(string name, string description)
        {
            if (Session["Admin_Email"] != null)
            {
                int result = Admin_Functions.Add_New_Payment(name, description);

                if (result == -1)
                {
                    String data = "Something went wrong while connecting with the database.";
                    return View("NewPayment", (object)data);
                }
                else if (result == 0)
                {
                    String data = "Valiadtion Errors";
                    return View("NewPayment", (object)data);
                }
                else if (result == 2)
                {

                    String data = "Payment With This Name Already Exists";
                    return View("NewPayment", (object)data);
                }
                //Sucessfully Added
                return RedirectToAction("ShowPayments");
            }
            else
            {
                return View("AdminLogin");
            }

        }
        //Delete A Payment
        public ActionResult deletePayment(int ID)
        {
            if (Session["Admin_Email"] != null)
            {
                int result = Admin_Functions.Delete_New_Payment(ID);
                return RedirectToAction("ShowPayments");
            }
            else
            {
                return View("AdminLogin");
            }
        }
        //Update A Payment
        public ActionResult UpdatePayment(int Id, string Name, string description)
        {
            if (Session["Admin_Email"] != null)
            {
                int result = Admin_Functions.Update_New_Payment(Id, Name, description);
                return RedirectToAction("ShowPayments");
            }
            else
            {
                return View("AdminLogin");
            }

        }
        //Add New Notification
        public ActionResult Add_New_Notifications(string Title, string description)
        {
            int result = Admin_Functions.Add_New_Notification(Title, description);

            if (result == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return RedirectToAction("AdminHome");
            }
            else if (result == 0)
            {
                String data = "Valiadtion Errors Or somthing else Error";
                return RedirectToAction("AdminHome");
            }
            //Sucessfully Added
            return RedirectToAction("AdminHome");

        }
        //Block A Seller
        public ActionResult Block_A_Sellers(int ID)
        {
            if (Session["Admin_Email"] == null)
                return RedirectToAction("AdminLogin");
            else
            {
                int result = Admin_Functions.Block_A_Seller(ID);
                if (result == -1)
                {
                    String data = "Something went wrong while connecting with the database.";
                    return RedirectToAction("Blocked_Sellers");
                }
                else if (result == 0)
                {
                    String data = "Valiadtion Errors Or somthing else Error";
                    return RedirectToAction("Blocked_Sellers");
                }
                return RedirectToAction("Blocked_Sellers");

            }
        }
        //Unblock A Seller
        public ActionResult UnBlock_A_Seller(int ID)
        {
            if (Session["Admin_Email"] == null)
                return RedirectToAction("AdminLogin");
            else
            {
                int result = Admin_Functions.UnBlock_A_Seller(ID);
                if (result == -1)
                {
                    String data = "Something went wrong while connecting with the database.";
                    return RedirectToAction("Non_Block_Sellers");
                }
                else if (result == 0)
                {
                    String data = "Valiadtion Errors Or somthing else Error";
                    return RedirectToAction("Non_Block_Sellers");
                }
                return RedirectToAction("Non_Block_Sellers");

            }
        }
        //Logout Function
        public ActionResult Logout()
        {
            Session["Admin_Email"] = null;
            return View("AdminLogin");
        }

        //VIEWS
        //Show Non Block Sellers
        public ActionResult Non_Block_Sellers()
        {
            if (Session["Admin_Email"] == null)
                return RedirectToAction("AdminLogin");
            else
            {
                List<Seller> sellers = Admin_Functions.Show_Non_Blocked_Sellers();
                if (sellers == null)
                    return RedirectToAction("AdminHome");
                else
                    return View(sellers);
            }
        }
        //Show Blocked Sellers
        public ActionResult Blocked_Sellers()
        {
            if (Session["Admin_Email"] == null)
                return RedirectToAction("AdminLogin");
            else
            {
                List<Blocked_Seller> B_sellers = Admin_Functions.Show_Blocked_Sellers();
                if (B_sellers == null)
                {
                    return RedirectToAction("AdminHome");
                }

                else
                    return View(B_sellers);
            }
        }
        //Admin Login View
        public ActionResult AdminLogin()
        {
            return View();
        }
        //Admin Sign Up View
        public ActionResult SignUp()
        {
            return View();
        }
        //Admin Update Profile View
        public ActionResult AdminProfile()
        {
            if (Session["Admin_Email"] == null)
                return RedirectToAction("AdminLogin");
            else
            {

                Admin admin;
                string email = Session["Admin_Email"].ToString();
                admin = Admin_Functions.Get_Admin_Profile(email);
                return View(admin);
            }
        }
        //Admin Home View
        public ActionResult AdminHome()
        {
            if (Session["Admin_Email"] == null)
                return RedirectToAction("AdminLogin");
            else
                return View();
        }
        //Show All Customers View
        public ActionResult Show_All_Customers()
        {
            if (Session["Admin_Email"] == null)
                return RedirectToAction("AdminLogin");
            else
            {
                List<Customer> customers = Admin_Functions.Show_All_Customers();
                if (customers == null)
                {
                    return RedirectToAction("AdminHome");
                }
                return View(customers);
            }
        }
        //Show All Categories
        public ActionResult ShowCategories()
        {
            if (Session["Admin_Email"] != null)
            {
                List<Category> categories = Admin_Functions.Show_All_Categories();
                if (categories == null)
                {
                    return View("AdminHome");
                }

                else
                    return View(categories);
            }
            else
            {

                return View("AdminLogin");
            }

        }
        //Show all Payments
        public ActionResult ShowPayments()
        {
            if (Session["Admin_Email"] != null)
            {
                List<Payment> payments = Admin_Functions.Show_All_Payments();
                if (payments == null)
                {
                    return RedirectToAction("AdminHome");
                }

                else
                    return View(payments);
            }
            else
            {

                return View("Admin_Login");
            }

        }
        //Edit A Payment
        public ActionResult EditAPayment(int ID)
        {
            if (Session["Admin_Email"] != null)
            {
                Payment obj = Admin_Functions.Get_New_Payment(ID);
                return View(obj);
            }
            else
            {
                return View("AdminLogin");
            }
        }
        //Add new Category View
        public ActionResult AddNewCategory()
        {
            if (Session["Admin_Email"] != null)
            {
                return View();
            }
            else
            {

                return View("AdminLogin");
            }
        }
        //Edit A Category View
        public ActionResult EditACategory(int ID)
        {
            if (Session["Admin_Email"] != null)
            {
                Category obj = Admin_Functions.Get_Category(ID);
                return View(obj);
            }
            else
            {

                return View("AdminLogin");
            }
        }
        //Add New Notication View
        public ActionResult NewNotification()
        {
            return View();
        }
        //Add New Payment View
        public ActionResult NewPayment()
        {
            return View();
        }
        //Show All Feedbacks View
        public ActionResult Show_All_Feedbacks()
        {
            if (Session["Admin_Email"] == null)
                return RedirectToAction("AdminLogin");
            else
            {
                List<Feedback> feedbacks = Admin_Functions.Show_Feedbacks();
                if (feedbacks == null)
                {
                    return View("AdminHome");
                }
                Console.Write(feedbacks);
                return View(feedbacks);
            }
        }
        //Show All Invoices View
        public ActionResult Show_Invoices()
        {
            if (Session["Admin_Email"] == null)
            {
                return View("AdminLogin");
            }
            else
            {
                List<Invoice_Admin> invoices = new List<Invoice_Admin>();
                List<Invoice_Cust> arr = Admin_Functions.get_all_invoice_information();
                if (arr.Count != 0)
                {
                    for (int i = 0; i < arr.Count; i++)
                    {
                        Admin_Functions.get_invoices(invoices, arr[i]);
                    }
                    return View("Show_Invoices", invoices);
                }
                else
                {
                    return View("AdminHome");
                }
            }
        }


    }
}
