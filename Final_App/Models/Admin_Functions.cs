using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;

namespace Final_App.Models
{
    public class Admin_Functions
    {
        public static string connectionString = "data source=localhost; Initial Catalog=Project;Integrated Security=true";
        //Admin Login (1)
        public static int Admin_Login(string email, string password)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("ADMIN_LOGIN", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 20).Value = email;
                cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 10).Value = password;
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
        //Admin SignUp (2)
        public static int Admin_SignUp(string email, string Password, string First_Name, string Last_Name)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            int result = 0;

            try
            {
                cmd = new SqlCommand("Admin_SignUp", con);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 20).Value = email;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar, 10).Value = Password;
                cmd.Parameters.Add("@First_Name", SqlDbType.VarChar, 50).Value = First_Name;
                cmd.Parameters.Add("@Last_Name", SqlDbType.VarChar, 50).Value = Last_Name;
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
        //Update Admin Profile (3)
        public static int Update_Admin_Profile(string old_email,
            string new_email, string new_Password, string First_Name, string Last_Name,
            string Gender, string phone, string cnic, string Qualification,
            string DOB, string Address)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("UPDATE_ADMIN_Profile", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 20).Value = old_email;
                cmd.Parameters.Add("@newemail", SqlDbType.NVarChar, 20).Value = new_email;
                cmd.Parameters.Add("@newpassword", SqlDbType.NVarChar, 10).Value = new_Password;
                cmd.Parameters.Add("@fname", SqlDbType.NVarChar, 50).Value = First_Name;
                cmd.Parameters.Add("@lname", SqlDbType.NVarChar, 50).Value = Last_Name;
                cmd.Parameters.Add("@gender", SqlDbType.NVarChar, 8).Value = Gender;
                cmd.Parameters.Add("@phone", SqlDbType.Char, 11).Value = phone;
                cmd.Parameters.Add("@cnic", SqlDbType.Char, 15).Value = cnic;
                cmd.Parameters.Add("@qual", SqlDbType.NVarChar, 25).Value = Qualification;
                cmd.Parameters.Add("@DOB", SqlDbType.Date, 50).Value = DOB;
                cmd.Parameters.Add("@address", SqlDbType.NVarChar, 500).Value = Address;
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
        //Add New Product (4)
        public static int Add_New_Product(int Id, string name, int CategoryID, string Product_Serial)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("Add_Product", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = Id;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = name;
                cmd.Parameters.Add("@categid", SqlDbType.Int).Value = CategoryID;
                cmd.Parameters.Add("@proserial", SqlDbType.Char, 14).Value = Product_Serial;
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
        //Add New Category (5)
        public static int Add_New_Category(string name, string description)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("ADD_Category", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = name;
                cmd.Parameters.Add("@Descipt", SqlDbType.NVarChar, 200).Value = description;
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
        //Add Payment Method (6)
        public static int Add_New_Payment(string name, string description)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("Add_New_Payment_Method", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = name;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 300).Value = description;
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
        //Update Payment Method (7)
        public static int Update_New_Payment(int Id, string name, string description)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("Update_Payment_Method", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Method_id", SqlDbType.Int).Value = Id;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = name;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = description;
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
        //Delete Payment Method (8)
        public static int Delete_New_Payment(int ID)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("Delete_Payment_Method", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Method_id", SqlDbType.Int).Value = ID;
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
        //Get Payment Method (9)
        public static Payment Get_New_Payment(int ID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("Get_Payment_Method", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@P_id", SqlDbType.Int).Value = ID;
                cmd.ExecuteNonQuery();
                Payment payment = new Payment();
                SqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                payment.P_id = int.Parse(rdr["P_id"].ToString());
                payment.Name = rdr["Name"].ToString();
                payment.Description = rdr["Description"].ToString();
                rdr.Close();
                con.Close();
                return payment;
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;

            }
        }
        //ADD New Notification (9)
        public static int Add_New_Notification(string Title, string description)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("Add_New_Notification", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 100).Value = Title;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 300).Value = description;
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
        //Block A Seller (10)
        public static int Block_A_Seller(int Seller_Id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("Block_A_Seller", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Seller_Id", SqlDbType.Int).Value = Seller_Id;
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
        //Show All Non Block Sellers (11)
        public static List<Seller> Show_Non_Blocked_Sellers()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("NON_BLOCKED_Sellers", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader rdr = cmd.ExecuteReader();

                List<Seller> list = new List<Seller>();
                while (rdr.Read())
                {
                    Seller seller = new Seller();

                    seller.SellerID = rdr.GetInt32(rdr.GetOrdinal("SellerID"));
                    seller.Password = rdr["Password"].ToString();
                    seller.Name = rdr["Name"].ToString();
                    seller.Gender = rdr["Gender"].ToString();
                    seller.Address = rdr["Address"].ToString();
                    seller.ContactNo = rdr["ContactNo"].ToString();
                    seller.Email = rdr["Email"].ToString();
                    list.Add(seller);
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
        //Show All categories
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
        //Show All Payment Methods
        public static List<Payment> Show_All_Payments()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("Show_All_Payments", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader rdr = cmd.ExecuteReader();

                List<Payment> list = new List<Payment>();
                while (rdr.Read())
                {
                    Payment payment = new Payment();
                    payment.P_id = rdr.GetInt32(rdr.GetOrdinal("P_id"));
                    payment.Name = rdr["Name"].ToString();
                    payment.Description = rdr["Description"].ToString();

                    list.Add(payment);
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
        //Show All Customers (12)
        public static List<Customer> Show_All_Customers()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("Show_All_Customers", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader rdr = cmd.ExecuteReader();

                List<Customer> list = new List<Customer>();
                while (rdr.Read())
                {
                    Customer customer = new Customer();

                    customer.CustomerID = rdr.GetInt32(rdr.GetOrdinal("CustomerID"));
                    customer.FIrst_Name = rdr["FIrst_Name"].ToString();
                    customer.Last_Name = rdr["Last_Name"].ToString();
                    customer.Phone = rdr["Phone"].ToString();
                    customer.Gender = rdr["Gender"].ToString();
                    customer.Email = rdr["Email"].ToString();
                    customer.Password = rdr["Password"].ToString();
                    customer.Zipcode = rdr["Zipcode"].ToString();
                    customer.Address = rdr["Address"].ToString();
                    customer.City = rdr["City"].ToString();
                    list.Add(customer);
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
        //Show All Blocked Sellers (13)
        public static List<Blocked_Seller> Show_Blocked_Sellers()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("Blocked_Sellers", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader rdr = cmd.ExecuteReader();

                List<Blocked_Seller> list = new List<Blocked_Seller>();
                while (rdr.Read())
                {
                    Blocked_Seller B_Seller = new Blocked_Seller();

                    B_Seller.SellerID = rdr.GetInt32(rdr.GetOrdinal("Seller_id"));
                    B_Seller.Date_time = rdr["Date_Time"].ToString();
                    B_Seller.Name = rdr["Name"].ToString();
                    B_Seller.Email = rdr["Email"].ToString();
                    B_Seller.Gender = rdr["Gender"].ToString();
                    list.Add(B_Seller);
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
        //Show All FeedBacks (14)
        public static List<Feedback> Show_Feedbacks()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("show_all_feedbacks", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader rdr = cmd.ExecuteReader();

                List<Feedback> list = new List<Feedback>();
                while (rdr.Read())
                {
                    Feedback feedback = new Feedback();
                    feedback.CustomerID = rdr.GetInt32(rdr.GetOrdinal("CustomerID"));
                    feedback.invoice_no = rdr.GetInt32(rdr.GetOrdinal("invoice_no"));
                    feedback.Email = rdr["Email"].ToString();
                    feedback.Date_Time = rdr["Date_Time"].ToString();
                    feedback.Feed_back = rdr["Feed_back"].ToString();
                    feedback.First_Name = rdr["FIrst_Name"].ToString();
                    feedback.Last_Name = rdr["Last_Name"].ToString();
                    feedback.Phone = rdr["Phone"].ToString();
                    feedback.Address = rdr["Address"].ToString();
                    list.Add(feedback);
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
        //Get Login Admin (15)
        public static Admin Get_Admin_Profile(string email)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("Get_profile", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 20).Value = email;
                cmd.ExecuteNonQuery();
                Admin admin = new Admin();
                SqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                admin.First_Name = rdr["First_Name"].ToString();
                admin.Last_Name = rdr["Last_Name"].ToString();
                admin.Phone = rdr["Phone"].ToString();
                admin.Gender = rdr["Gender"].ToString();
                admin.email = rdr["Email"].ToString();
                admin.Password = rdr["Password"].ToString();
                admin.Address = rdr["Address"].ToString();
                admin.CNIC = rdr["CNIC"].ToString();
                admin.Qualfication = rdr["Qualfication"].ToString();
                admin.DOB = rdr["DOB"].ToString();
                rdr.Close();
                con.Close();
                return admin;
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;

            }

        }
        //Update Category (16)
        public static int Update_Category(int CategoryID, string name, string description)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("Update_Category", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = CategoryID;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = name;
                cmd.Parameters.Add("@Descipt", SqlDbType.NVarChar, 200).Value = description;
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
        //Delete Category (17)
        public static int Delete_Category(int CategoryID)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("Delete_Category", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = CategoryID;
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
        //Get Category (18)
        public static Category Get_Category(int CategoryID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("Get_Category", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = CategoryID;
                cmd.ExecuteNonQuery();
                Category category = new Category();
                SqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                category.CategoryID = rdr.GetInt32(rdr.GetOrdinal("CategoryID"));
                category.Name = rdr["Name"].ToString();
                category.Description = rdr["Description"].ToString();
                rdr.Close();
                con.Close();
                return category;
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;

            }

        }
        //Unblock A Seller
        public static int UnBlock_A_Seller(int Seller_Id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("UnBlock_A_Seller", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Seller_Id", SqlDbType.Int).Value = Seller_Id;
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
        //Get Invoice Info
        public static List<Invoice_Cust> get_all_invoice_information()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            List<Invoice_Cust> result = new List<Invoice_Cust>();
            try
            {
                cmd = new SqlCommand("get_all_invoice_information", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Invoice_Cust invoice_cust = new Invoice_Cust();
                    invoice_cust.invoiceno = rdr.GetInt32(rdr.GetOrdinal("invoiceNo"));
                    invoice_cust.Email = rdr["Email"].ToString();
                    result.Add(invoice_cust);
                }
                rdr.Close();
                con.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }
            return result;
        }
        // Get invoices
        public static void get_invoices(List<Invoice_Admin> lst, Invoice_Cust obj)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("print_invoice", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@inv_num", SqlDbType.NVarChar, 50).Value = obj.invoiceno;
                cmd.ExecuteNonQuery();
                int count = 1;
                SqlDataReader rdr = cmd.ExecuteReader();
                Invoice_Admin invoice_admin = new Invoice_Admin();
                invoice_admin.grand_total = 0;
                invoice_admin.invoiceno = obj.invoiceno;
                invoice_admin.Email = obj.Email;
                invoice_admin.products = new List<Invoice_Product>();
                while (rdr.Read())
                {
                    Invoice_Product Product = new Invoice_Product();
                    Product.num = count;
                    count++;
                    Product.Product_id = rdr["ProductID"].ToString();
                    Product.product_name = rdr["Product_Name"].ToString();
                    Product.description = rdr["Description"].ToString();
                    Product.unit_price = rdr["Unit_price"].ToString();
                    Product.quantity = rdr["Quantity"].ToString();
                    invoice_admin.date = rdr["Date_Time"].ToString();
                    Product.sub_total = Convert.ToInt32(Product.unit_price) * Convert.ToInt32(Product.quantity);
                    invoice_admin.grand_total = invoice_admin.grand_total + Product.sub_total;
                    invoice_admin.products.Add(Product);
                }
                lst.Add(invoice_admin);
                rdr.Close();
                con.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

    }
}