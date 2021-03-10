using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;

namespace Final_App.Models
{
    public class Seller_Functions
    {
        public static string connectionString = "data source=localhost; Initial Catalog=Project;Integrated Security=true";
        public static int Signup(string name, string gender, string email, string password)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("Seller_SignUp", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = name;
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 25).Value = email;
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 20).Value = password;
                cmd.Parameters.Add("@gender", SqlDbType.VarChar, 8).Value = gender;
                cmd.Parameters.Add("@output", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@output"].Value);
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1; //-1 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;
        }
        public static int Signin(string email, string password)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("Seller_Login", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 25).Value = email;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar, 20).Value = password;


                cmd.Parameters.Add("@output", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@output"].Value);
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1; //-1 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;
        }
        public static CurrentSeller Get_Seller_Profile(string email)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("Get_Seller_profile", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 25).Value = email;
                cmd.ExecuteNonQuery();
                CurrentSeller seller = new CurrentSeller();
                SqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                seller.Name = rdr["Name"].ToString();
                seller.ContactNo = rdr["ContactNo"].ToString();
                seller.Gender = rdr["Gender"].ToString();
                seller.Email = rdr["Email"].ToString();
                seller.Password = rdr["Password"].ToString();
                seller.Address = rdr["Address"].ToString();
                seller.City = rdr["City"].ToString();
                seller.DOB = rdr["DOB"].ToString();
                rdr.Close();
                con.Close();
                return seller;
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;

            }

        }
        public static int Update_Seller_Profile(string Name, string old_email, string new_email, string new_Password,
            string ContactNo, string Gender, string City, string Address, string DOB)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("Update_Seller_Profile", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 25).Value = old_email;
                cmd.Parameters.Add("@newemail", SqlDbType.NVarChar, 25).Value = new_email;
                cmd.Parameters.Add("@newpassword", SqlDbType.NVarChar, 20).Value = new_Password;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = Name;
                cmd.Parameters.Add("@gender", SqlDbType.NVarChar, 8).Value = Gender;
                cmd.Parameters.Add("@address", SqlDbType.NVarChar, 500).Value = Address;
                cmd.Parameters.Add("@city", SqlDbType.NVarChar, 20).Value = City;
                cmd.Parameters.Add("@contactno", SqlDbType.Char, 11).Value = ContactNo;
                cmd.Parameters.Add("@DOB", SqlDbType.Date).Value = Convert.ToDateTime(DOB);

                cmd.Parameters.Add("@output", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@output"].Value);
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1;
            }
            finally
            {
                con.Close();
            }
            return result;

        }
        public static int AddProduct(string product_Name, int unit_Price, int stock, string city,
            string manufacturer, string model, string released_date, int categoryID, int sellerID)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("Add_Product", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@p_name", SqlDbType.VarChar, 100).Value = product_Name;
                cmd.Parameters.Add("@unit_price", SqlDbType.Int).Value = unit_Price;
                cmd.Parameters.Add("@stock", SqlDbType.Int).Value = stock;
                cmd.Parameters.Add("@city", SqlDbType.VarChar, 20).Value = city;
                cmd.Parameters.Add("@Manufacturer_Name", SqlDbType.VarChar, 100).Value = manufacturer;
                cmd.Parameters.Add("@model", SqlDbType.Char, 4).Value = model;
                cmd.Parameters.Add("@released_date", SqlDbType.Date).Value = Convert.ToDateTime(released_date);
                cmd.Parameters.Add("@category_id", SqlDbType.Int).Value = categoryID;
                cmd.Parameters.Add("@seller_id", SqlDbType.Int).Value = sellerID;

                cmd.Parameters.Add("@output", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@output"].Value);
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1; //-1 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;
        }
        public static int Update_Product(int product_ID, string product_Name, int unit_Price, int stock, string city,
            string manufacturer, string model, int categoryID)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("Update_Product", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = product_ID;
                cmd.Parameters.Add("@p_name", SqlDbType.NVarChar, 100).Value = product_Name;
                cmd.Parameters.Add("@unit_price", SqlDbType.Int).Value = unit_Price;
                cmd.Parameters.Add("@stock", SqlDbType.Int).Value = stock;
                cmd.Parameters.Add("@city", SqlDbType.NVarChar, 20).Value = city;
                cmd.Parameters.Add("@Manufacturer_Name", SqlDbType.NVarChar, 100).Value = manufacturer;
                cmd.Parameters.Add("@model", SqlDbType.Char, 4).Value = model;
                cmd.Parameters.Add("@category_id", SqlDbType.Int).Value = categoryID;

                cmd.Parameters.Add("@output", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@output"].Value);
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1;
            }
            finally
            {
                con.Close();
            }
            return result;

        }
        public static int Delete_Seller_Product(int ProductID)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("Delete_Product", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@p_ID", SqlDbType.Int).Value = ProductID;

                cmd.Parameters.Add("@output", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@output"].Value);
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1; //-1 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;
        }
        public static int GetSellerID(string email)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("GetSellerID", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 25).Value = email;

                cmd.Parameters.Add("@output", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@output"].Value);
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1;  //-1 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;
        }
        public static List<Product1> Show_Seller_Products(int sellerid)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("Show_Seller_Products", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Sellerid", SqlDbType.Int).Value = sellerid;


                SqlDataReader rdr = cmd.ExecuteReader();
                List<Product1> list = new List<Product1>();

                while (rdr.Read())
                {
                    Product1 P = new Product1();
                    P.ProductID = Convert.ToInt32(rdr["ProductID"]);
                    P.Product_Name = rdr["Product_Name"].ToString();
                    P.Stock = Convert.ToInt32(rdr["Stock"]);
                    P.Unit_price = Convert.ToInt32(rdr["Unit_price"]);
                    P.City = rdr["City"].ToString();
                    P.Date_Time_of_Entry = rdr["Date_Time_of_Entry"].ToString();
                    P.Manufacturer = rdr["Manufacturer"].ToString();
                    P.Model = rdr["Model"].ToString();
                    P.Released_Date = rdr["Released_Date"].ToString();
                    P.CategoryID = Convert.ToInt32(rdr["CategoryID"]);
                    P.SellerID = Convert.ToInt32(rdr["SellerID"]);


                    list.Add(P);
                }
                rdr.Close();
                con.Close();

                return list;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
        }
        public static List<SellerOrders> Show_Seller_Orders(int sellerid)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("See_Seller_Orders", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Sellerid", SqlDbType.Int).Value = sellerid;


                SqlDataReader rdr = cmd.ExecuteReader();
                List<SellerOrders> list = new List<SellerOrders>();

                while (rdr.Read())
                {
                    SellerOrders P = new SellerOrders();
                    P.InvoiceNo = Convert.ToInt32(rdr["InvoiceNo"]);
                    P.CustomerID = Convert.ToInt32(rdr["CustomerID"]);
                    P.Date_Time = rdr["Date_Time"].ToString();
                    P.Payment_Method_id = Convert.ToInt32(rdr["Payment_Method_id"]);
                    P.ProductID = Convert.ToInt32(rdr["ProductID"]);
                    P.Product_Name = rdr["Product_Name"].ToString();
                    P.Quantity = Convert.ToInt32(rdr["Quantity"]);

                    list.Add(P);
                }
                rdr.Close();
                con.Close();

                return list;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
        }
        public static List<Category> Show_All_Categories()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("Show_All_Categories", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader rdr = cmd.ExecuteReader();

                List<Category> list = new List<Category>();
                while (rdr.Read())
                {
                    Category category = new Category();
                    category.CategoryID = rdr.GetInt32(rdr.GetOrdinal("CategoryID"));
                    category.Name = rdr["Name"].ToString();
                    category.Description = rdr["Description"].ToString();

                    list.Add(category);
                }
                rdr.Close();
                con.Close();

                return list;


            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;

            }

        }
        public static Product1 get_product_information(int p_id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("Get_Product_Information", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@p_ID", SqlDbType.Int).Value = p_id;

                cmd.Parameters.Add("@output", SqlDbType.Int).Direction = ParameterDirection.Output;

                SqlDataReader rdr = cmd.ExecuteReader();
                Product1 product = new Product1();
                while (rdr.Read())
                {
                    product.ProductID = Convert.ToInt32(rdr["ProductID"]);
                    product.Product_Name = rdr["Product_Name"].ToString();
                    product.Unit_price = Convert.ToInt32(rdr["Unit_price"]);
                    product.Stock = Convert.ToInt32(rdr["Stock"]);
                    product.City = rdr["City"].ToString();
                    product.Manufacturer = rdr["Manufacturer"].ToString();
                    product.Model = rdr["Model"].ToString();
                    product.CategoryID = Convert.ToInt32(rdr["CategoryID"]);

                }
                rdr.Close();
                con.Close();

                return product;


            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;

            }

        }
        public static List<Notification> Show_Seller_Notifications()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("show_all_Notifications", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader rdr = cmd.ExecuteReader();
                List<Notification> list = new List<Notification>();

                while (rdr.Read())
                {
                    Notification P = new Notification();
                    P.ID = Convert.ToInt32(rdr["ID"]);
                    P.Title = rdr["Title"].ToString();
                    P.Description = rdr["Description"].ToString();
                    P.Time_Date = rdr["Time_Date"].ToString();

                    list.Add(P);
                }
                rdr.Close();
                con.Close();

                return list;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
        }



    }
}
