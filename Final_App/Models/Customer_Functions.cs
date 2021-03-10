using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;


namespace Final_App.Models
{

    public class Customer_Functions
    {

        public static string connectionString = "data source=localhost; Initial Catalog=Project;Integrated Security=true";
        //Sign Up
        public static int Signup(string Fname, string Lname, string Phone, string gender, string email, string password)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("Customer_sign_up", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@FName", SqlDbType.NVarChar, 50).Value = Fname;
                cmd.Parameters.Add("@LName", SqlDbType.NVarChar, 50).Value = Lname;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 50).Value = Phone;
                cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 50).Value = gender;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = email;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar, 50).Value = password;
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
        //Sign In
        public static int Signin(string email, string password)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("Customer_Login", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = email;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar, 50).Value = password;


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
        //Get All Customers
        public static List<Customer_Signup> getAllCustomers()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("show_all_customers", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader rdr = cmd.ExecuteReader();
                List<Customer_Signup> list = new List<Customer_Signup>();
                while (rdr.Read())
                {
                    Customer_Signup user = new Customer_Signup();

                    user.Fname = rdr["First_Name"].ToString();
                    user.Lname = rdr["Last_Name"].ToString();
                    user.Phone = rdr["Phone"].ToString();
                    user.gender = rdr["Gender"].ToString();
                    user.email = rdr["Email"].ToString();
                    user.password = rdr["Password"].ToString();
                    list.Add(user);
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
        //Get Current_Loggined User
        public static CurrentUser Current_LoggedIN_User(string c_user)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("Current_LoggedIN_User", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = c_user;
                cmd.ExecuteNonQuery();
                CurrentUser user = new CurrentUser();
                SqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                user.Fname = rdr["First_Name"].ToString();
                user.Lname = rdr["Last_Name"].ToString();
                user.Phone = rdr["Phone"].ToString();
                user.gender = rdr["Gender"].ToString();
                user.email = rdr["Email"].ToString();
                user.password = rdr["Password"].ToString();
                user.address = rdr["Address"].ToString();
                user.city = rdr["City"].ToString();
                user.zipcode = rdr["Zipcode"].ToString();
                rdr.Close();
                con.Close();
                return user;
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;

            }

        }
        //Update Cust Profile
        public static int UpdateCustProfile(string Fname, string Lname, string Email, string address,
            string city, string zipcode, string password1, string password2)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("Update_Customer_Profile", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@FName", SqlDbType.NVarChar, 50).Value = Fname;
                cmd.Parameters.Add("@address", SqlDbType.NVarChar, 50).Value = address;
                cmd.Parameters.Add("@city", SqlDbType.NVarChar, 50).Value = city;
                cmd.Parameters.Add("@zipcode", SqlDbType.NVarChar, 50).Value = zipcode;
                cmd.Parameters.Add("@LName", SqlDbType.NVarChar, 50).Value = Lname;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = Email;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar, 50).Value = password1;
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
        //Show All Products
        public static List<Store> get_product_info()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("show_all_products", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader rdr = cmd.ExecuteReader();
                List<Store> list = new List<Store>();
                int a = 1;
                while (rdr.Read())
                {
                    Store Product = new Store();
                    Product.ProductID = rdr["ProductID"].ToString();
                    Product.Product_Name = rdr["Product_Name"].ToString();
                    Product.Stock = rdr["Stock"].ToString();
                    Product.City = rdr["City"].ToString();
                    Product.Unit_price = rdr["Unit_price"].ToString();
                    Product.auto = a;
                    a++;
                    list.Add(Product);
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
        //Filter Search
        public static List<Store> filter_search(string select, string upper, string lower, string p_name, string p_location)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result;
            try
            {
                cmd = new SqlCommand("Filter_Products", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                cmd.Parameters.Add("@categ", SqlDbType.NVarChar, 50).Value = select;
                if (upper == "")
                {
                    cmd.Parameters.Add("@upper", SqlDbType.NVarChar, 50).Value = "";
                }
                else
                {
                    cmd.Parameters.Add("@upper", SqlDbType.NVarChar, 50).Value = Int32.Parse(upper);
                }
                if (lower == "")
                {
                    cmd.Parameters.Add("@lower", SqlDbType.NVarChar, 50).Value = "";
                }
                else
                {
                    cmd.Parameters.Add("@lower", SqlDbType.NVarChar, 50).Value = Int32.Parse(lower);
                }
                cmd.Parameters.Add("@p_name", SqlDbType.NVarChar, 50).Value = p_name;
                cmd.Parameters.Add("@p_location", SqlDbType.NVarChar, 50).Value = p_location;
                cmd.Parameters.Add("@output", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@output"].Value);
                List<Store> list = new List<Store>();
                if (result == 1)
                {
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Store Product = new Store();
                        Product.ProductID = rdr["ProductID"].ToString();
                        Product.Product_Name = rdr["Product_Name"].ToString();
                        Product.Stock = rdr["Stock"].ToString();
                        Product.City = rdr["City"].ToString();
                        Product.Unit_price = rdr["Unit_price"].ToString();
                        Product.status = "Search Successfull";
                        list.Add(Product);
                    }
                    rdr.Close();
                }

                con.Close();

                return list;


            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;

            }

        }
        //Add To cart
        public static int Add_to_Cart(Product obj, string c_user)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;
            try
            {

                cmd = new SqlCommand("insert_cart_product", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.NVarChar, 50).Value = obj.ID;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = c_user;
                cmd.Parameters.Add("@output", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@output"].Value);
                con.Close();
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return -1;
            }
            return result;
        }
        //Search By category
        public static List<Store> Search_by_categ(string select)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result;
            try
            {
                cmd = new SqlCommand("Search_by_categ", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@categ", SqlDbType.NVarChar, 50).Value = select;
                cmd.Parameters.Add("@output", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@output"].Value);
                List<Store> list = new List<Store>();
                if (result == 1)
                {
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Store Product = new Store();
                        Product.ProductID = rdr["ProductID"].ToString();
                        Product.Product_Name = rdr["Product_Name"].ToString();
                        Product.Stock = rdr["Stock"].ToString();
                        Product.City = rdr["City"].ToString();
                        Product.Unit_price = rdr["Unit_price"].ToString();
                        Product.status = "Search Successfull";
                        list.Add(Product);
                    }
                    rdr.Close();
                }

                con.Close();

                return list;


            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;

            }

        }
        //Get Cart Items
        public static List<Cart_Entries> get_cart_items(string c_user)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("get_cart_items", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = c_user;
                cmd.ExecuteNonQuery();
                List<Cart_Entries> list = new List<Cart_Entries>();
                SqlDataReader rdr = cmd.ExecuteReader();
                int a = -1;
                while (rdr.Read())
                {
                    Cart_Entries Product = new Cart_Entries();
                    Product.Product_id = rdr["ProductID"].ToString();
                    Product.Product_Name = rdr["Product_Name"].ToString();
                    Product.MAX_Quantity = rdr["Stock"].ToString();
                    Product.Description = rdr["Description"].ToString();
                    Product.Unit_price = rdr["Unit_price"].ToString();
                    Product.quantity = rdr["Quantity"].ToString();
                    Product.id = a;
                    a--;
                    Product.total = Convert.ToInt32(Product.quantity) * Convert.ToInt32(Product.Unit_price);
                    list.Add(Product);
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
        //Remove Cart Items
        public static void remove_cart_product(Product obj, string c_user)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            try
            {

                cmd = new SqlCommand("remove_cart_product", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.NVarChar, 50).Value = obj.ID;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = c_user;
                cmd.ExecuteNonQuery();
                con.Close();
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
        }
        //Update Quantity
        public static void update_quantity(Quantity obj, string c_user)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("Update_Quantity", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@p_id", SqlDbType.NVarChar, 50).Value = obj.ID;
                cmd.Parameters.Add("@quan", SqlDbType.NVarChar, 50).Value = obj.quantity;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = c_user;
                cmd.ExecuteNonQuery();
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
        //Get Invoices
        public static void get_invoices(List<Invoice> lst, int inv_num)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("print_invoice", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@inv_num", SqlDbType.NVarChar, 50).Value = inv_num;
                cmd.ExecuteNonQuery();
                int count = 1;
                SqlDataReader rdr = cmd.ExecuteReader();
                Invoice cust_in = new Invoice();
                cust_in.grand_total = 0;
                cust_in.invoiceno = inv_num;
                cust_in.products = new List<Invoice_Product>();
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
                    cust_in.date = rdr["Date_Time"].ToString();
                    Product.sub_total = Convert.ToInt32(Product.unit_price) * Convert.ToInt32(Product.quantity);
                    cust_in.grand_total = cust_in.grand_total + Product.sub_total;
                    cust_in.products.Add(Product);
                }
                lst.Add(cust_in);
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
        //Populate Invoice Items
        public static int populate_invoice_items(string c_user, string Payment_Method_id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result;
            try
            {
                cmd = new SqlCommand("add_invoice_entry", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = c_user;
                cmd.Parameters.Add("@Payment_Method_id", SqlDbType.Int).Value = Payment_Method_id;
                cmd.Parameters.Add("@output", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@output"].Value);
                return result;
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
        public static List<int> get_invoice_information(string c_user)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            List<int> result = new List<int>();
            try
            {
                cmd = new SqlCommand("get_invoice_information", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = c_user;
                cmd.ExecuteNonQuery();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    result.Add(Convert.ToInt32(rdr["InvoiceNo"].ToString()));
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
        //Get Final Invoice Of Customer
        public static void get_invoice_customer(List<Invoice> lst, int index, string c_user)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("get_invoice_customer", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = c_user;
                cmd.ExecuteNonQuery();
                lst[index].customer = new invoice_customer_Info();
                SqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                lst[index].customer.Fname = rdr["FIrst_Name"].ToString();
                lst[index].customer.Lname = rdr["Last_Name"].ToString();
                lst[index].customer.phone = rdr["Phone"].ToString();
                lst[index].customer.email = rdr["email"].ToString();
                lst[index].customer.address = rdr["Address"].ToString();
                lst[index].customer.city = rdr["City"].ToString();
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
        //Add Feedback
        public static void add_feedback_to_db(feedback obj, string c_user)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("add_customer_feedback", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = c_user;
                cmd.Parameters.Add("@feedback", SqlDbType.NVarChar, 500).Value = obj.feed;
                cmd.Parameters.Add("@inv_num", SqlDbType.NVarChar, 50).Value = obj.invoice_num;
                cmd.ExecuteNonQuery();

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
        //Show Notifactions
        public static List<Notifications> get_notifications()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("get_Notifications", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                SqlDataReader rdr = cmd.ExecuteReader();
                List<Notifications> lst = new List<Notifications>();
                while (rdr.Read())
                {
                    Notifications N = new Notifications();
                    N.title = rdr["Title"].ToString();
                    N.description = rdr["Description"].ToString();
                    N.Date = rdr["Date_Time"].ToString();
                    lst.Add(N);
                }

                rdr.Close();
                con.Close();
                return lst;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
            finally
            {
                con.Close();
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
        //Get Payment Method
        public static void get_payment_from_Invoice(List<Invoice> lst, int index, int invoiceno)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("get_payment_from_Invoice", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@invoiceno", SqlDbType.Int).Value = invoiceno;
                cmd.ExecuteNonQuery();
                lst[index].customer = new invoice_customer_Info();
                SqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                lst[index].payment = rdr["Name"].ToString();
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
