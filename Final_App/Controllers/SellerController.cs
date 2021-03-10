using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_App.Models;
namespace Final_App.Controllers
{
    public class SellerController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        //Seller Home (View)
        public ActionResult Seller_Home()
        {
            if (Session["Seller_Email"] == null)
                return RedirectToAction("../Seller/Seller_Login");
            else
            {
                return View();
            }

        }
        //Seller Login (View)
        public ActionResult Seller_Login()
        {
            return View();
        }
        //Seller SignUp (View)
        public ActionResult Seller_Registration()
        {
            return View();
        }
        //Seller SignUp
        public ActionResult Authenticate_Seller_Signup(string name, string gender, string email, string password)
        {

            int result = Seller_Functions.Signup(name, gender, email, password);

            if (result == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return View("Seller_Registration", (object)data);
            }
            else if (result == 0)
            {

                String data = "This Email is Already in Use";
                return View("Seller_Registration", (object)data);
            }
            else if (result == 2)
            {

                String data = "This Email Has Been Blocked...Try to make account with another email";
                return View("Seller_Registration", (object)data);
            }
            Session["seller_email"] = email;
            Session["SellerID"] = Seller_Functions.GetSellerID(email);
            return RedirectToAction("Seller_Home");
        }
        //Seller Login
        public ActionResult Authenticate_Seller_Signin(string email, string password)
        {
            int result = Seller_Functions.Signin(email, password);

            if (result == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return View("CustLogin", (object)data);
            }
            else if (result == 0)
            {

                String data = "Email or Password is incorrect";
                return View("Seller_Login", (object)data);
            }
            else if (result == 2)
            {

                String data = "Your Account Has Been Blocked!!!";
                return View("Seller_Login", (object)data);
            }
            // ViewBag.id = 1;
            Session["seller_email"] = email;
            Session["SellerID"] = Seller_Functions.GetSellerID(email);
            return RedirectToAction("Seller_Home");
        }
        //Seller Profile (View)
        public ActionResult Seller_Profile()
        {
            if (Session["Seller_Email"] == null)
                return RedirectToAction("../Seller/Seller_Login");
            else
            {

                CurrentSeller seller;
                string email = Session["Seller_Email"].ToString();
                seller = Seller_Functions.Get_Seller_Profile(email);
                return View(seller);
            }
        }
        //Update Profile
        public ActionResult Seller_Update_Profile(string Name, string new_email, string new_Password,
            string ContactNo, string Gender, string City, string Address, string DOB)
        {
            if (Session["Seller_Email"] != null)
            {
                

                string old_email = Session["Seller_Email"].ToString();
                int result = Seller_Functions.Update_Seller_Profile(Name, old_email, new_email, new_Password,
                    ContactNo, Gender, City, Address, DOB);

                if (result == -1)
                {
                    String data = "Something went wrong while connecting with the database.";
                    return View("../Seller/Seller_Home", (object)data);
                }
                else if (result == 0)
                {

                    String data = "Valiadtion Errors";
                    return View("../Seller/Seller_Home", (object)data);
                }
                //Sucessfully Updated
                Session["Seller_Email"] = new_email;
                String data1 = "Profile Successfully Updated";
                return View("../Seller/Seller_Home", (object)data1);
            }
            else
            {

                return View("../Seller/Seller_Login");
            }

        }
        //Add Product (View)
        public ActionResult Seller_Add_Product()
        {
            if (Session["Seller_Email"] == null)
            {
                return View("../Seller/Seller_Login");
            }
            else
            {
                List<Category> categories = Seller_Functions.Show_All_Categories();
                if (categories == null)
                {
                    return RedirectToAction("Admin_has_not_added_categories, Contact Admin");
                }

                return View(categories);
            }

        }
        //Add Product
        public ActionResult Add_Product(string FormProductName, int FormPrice, int FormStock, string FormCity,
          string FormManufacturer, string FormModel, string released_date, int category_id)
        {
            if (Session["Seller_Email"] != null)
            {
                int sellerid = Convert.ToInt32(Session["SellerID"]);
                int result = Seller_Functions.AddProduct(FormProductName, FormPrice, FormStock, FormCity, FormManufacturer,
                    FormModel, released_date, category_id, sellerid);

                if (result == -1)
                {
                    String data = "Something went wrong while connecting with the database.";
                    return View("../Seller/Seller_Home", (object)data);
                }
                else if (result == 0)
                {

                    String data = "Valiadtion Errors";
                    return View("../Seller/Seller_Home", (object)data);
                }
                //Sucessfully Updated
                String data1 = "Product Successfully Added";


                List<Product1> product;
                product = Seller_Functions.Show_Seller_Products(sellerid);
                return View("../Seller/Seller_Show_Products", product);
            }
            else
            {
                return View("../Seller/Seller_Login");
            }
        }
        //Seller Update Product (View)
        public ActionResult Seller_Update_Product(int ProductID)
        {
            if (Session["Seller_Email"] != null)
            {

                Product1 product = Seller_Functions.get_product_information(ProductID);
                List<Category> categories = Seller_Functions.Show_All_Categories();
                if (categories == null)
                {
                    return RedirectToAction("Admin_has_not_added_categories, Contact Admin");
                }

                CategoryWithProduct obj = new CategoryWithProduct();
                obj.product = product;
                obj.Categories = categories;

                return View(obj);


            }
            else
            {
                return View("../Seller/Seller_Login");
            }
        }
        //Update Product
        public ActionResult Update_Seller_Product(int FormProductID, string FormProductName, int FormPrice, int FormStock, string FormCity,
          string FormManufacturer, string FormModel, int category_id)
        {
            if (Session["Seller_Email"] != null)
            {
                int sellerid = Convert.ToInt32(Session["SellerID"]);
                int result = Seller_Functions.Update_Product(FormProductID, FormProductName, FormPrice, FormStock, FormCity, FormManufacturer,
                    FormModel, category_id);

                if (result == -1)
                {
                    String data = "Something went wrong while connecting with the database.";
                    return View("../Seller/Seller_Home", (object)data);
                }
                else if (result == 0)
                {

                    String data = "Valiadtion Errors";
                    return View("../Seller/Seller_Home", (object)data);
                }
                //Sucessfully Updated
                String data1 = "Product Successfully Updated";

                List<Product1> product;
                product = Seller_Functions.Show_Seller_Products(sellerid);
                return View("../Seller/Seller_Show_Products", product);
            }
            else
            {
                return View("../Seller/Seller_Login");
            }
        }
        //Delete Product
        public ActionResult Delete_Product(int ProductID)
        {
            if (Session["Seller_Email"] != null)
            {
                int sellerid = Convert.ToInt32(Session["SellerID"]);
                int result = Seller_Functions.Delete_Seller_Product(ProductID);

                if (result == -1)
                {
                    String data = "Something went wrong while connecting with the database.";
                    return View("../Seller/Seller_Home", (object)data);
                }
                else if (result == 0)
                {

                    String data = "Valiadtion Errors";
                    return View("../Seller/Seller_Home", (object)data);
                }
                //Sucessfully Deleted

                List<Product1> product;
                product = Seller_Functions.Show_Seller_Products(sellerid);
                return View("../Seller/Seller_Show_Products", product);
            }
            else
            {
                return View("../Seller/Seller_Login");
            }
        }
        //Show All Products (View)
        public ActionResult Seller_Show_Products()
        {
            if (Session["Seller_Email"] == null)
            {
                return View("../Seller/Seller_Login");
            }
            else
            {
                int sellerid = Convert.ToInt32(Session["SellerID"]);
                List<Product1> product;
                product = Seller_Functions.Show_Seller_Products(sellerid);
                if (product == null)
                    return View("../Seller/Seller_Home");

                return View(product);
            }
        }
        //Seller Invoices (View)
        public ActionResult Seller_Show_Orders()
        {
            if (Session["Seller_Email"] == null)
            {
                return View("../Seller/Seller_Login");
            }
            else
            {
                int sellerid = Convert.ToInt32(Session["SellerID"]);
                List<SellerOrders> list;
                list = Seller_Functions.Show_Seller_Orders(sellerid);
                if (list == null)
                {
                    String data = "You have no orders";
                    return View("../Seller/Seller_Home", data);
                }

                return View(list);
            }
        }
        //Show All Notifications (View)
        public ActionResult Seller_Show_Notifications()
        {
            if (Session["Seller_Email"] == null)
            {
                return View("../Seller/Seller_Login");
            }
            else
            {
                int sellerid = Convert.ToInt32(Session["SellerID"]);
                List<Notification> list;
                list = Seller_Functions.Show_Seller_Notifications();
                if (list == null)
                    return View("../Seller/Seller_Home");

                return View(list);
            }
        }
    }
}
