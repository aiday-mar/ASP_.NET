using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// to add the extension from the controller, you must add the below mvc library
using System.Web.Mvc;
using Blogee.Models;
using System.Data.SqlClient;
using System.Text;
using System.Web.Helpers;
using System.Data;

namespace Blogee.Controllers
{
    public class DashboardController : Controller
    {

        string name = "";
        string username = "";
        string email = "";
        string hashedpassword = "";
        string password = "";
        bool isnewuser = false;


        public ActionResult Index()
        {
            // we found out that when the submit form is clicked then the program enters the Index method of the Dashboard Controller 
            return View();
        }

        // here we found out that when we write Dashboard/AddNewUser after localhost, then we access the AddNewUser method in the DashboardController
        // this means that all the cshtml files in the Dashboard folder of the View folder are related to some method in the action

        [HttpPost]
        public ActionResult Index(UserModel u)
        {
            // using the debug mode we find that this works
            name = u.Name;
            username = u.Username;
            email = u.Email;
            // using debug mode indeed we find that the below does output a string hashpassword
            password = u.Password;
            hashedpassword = Crypto.HashPassword(password);
            // the problem is that this boolean is still evaluated to false
            isnewuser = u.NewUser;

            ViewBag.User = u;

            // we ran the code below and found that this code really does store the name, username etc in the dbo.Users table
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "sqlserver-aiday.database.windows.net";
                builder.UserID = "serverusername";
                builder.Password = "ConnectDB1";
                builder.InitialCatalog = "Blogee_db";

                // string connectionString = "Server=tcp:sqlserver-aiday.database.windows.net,1433;Initial Catalog=Blogee_db;Persist Security Info=False;User ID=serverusername;Password=ConnectDB1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
               
                if (isnewuser == true)
                {
                    using (SqlConnection openCon = new SqlConnection(builder.ConnectionString))
                    {
                        string saveUser = "INSERT into dbo.Users (Username, Name, Email, Password) VALUES (@Username,@Name, @Email,@Password)";

                        using (SqlCommand querySaveUser = new SqlCommand(saveUser))
                        {
                            querySaveUser.Connection = openCon;
                            querySaveUser.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = username;
                            querySaveUser.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = name;
                            querySaveUser.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = email;
                            querySaveUser.Parameters.Add("@Password", SqlDbType.NVarChar, 250).Value = hashedpassword;
                            openCon.Open();
                            querySaveUser.ExecuteNonQuery();
                        }
                    }
                }

                if (isnewuser == false)
                {
                    using (SqlConnection openCon = new SqlConnection(builder.ConnectionString))
                    {
                        string verifyUser = "SELECT Username, Password FROM dbo.Users WHERE Username = @Username AND Password = @Password";
                        using (SqlCommand queryVerifyUser = new SqlCommand(verifyUser))
                        {
                            queryVerifyUser.Connection = openCon;
                            queryVerifyUser.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = username;
                            queryVerifyUser.Parameters.Add("@Password", SqlDbType.NVarChar, 250).Value = hashedpassword;
                            openCon.Open();
                            var dataReader = queryVerifyUser.ExecuteReader();

                            if (!dataReader.HasRows)
                            {
                                return RedirectToAction("Login", "Home");
                            }

                            dataReader.Close();
                            // otherwise we just display the correct view
                        }
                    }
                }

                // I changed the code above which was used to append the data into the SQL server, and now it is working with a hashed password
                // I see now that if a solution is not working, then I just need to find another working solution even if I don't understand why the first one is not working 
            }
            // to catch the exeption I had to put a breakpoint next to the exception to make it appear
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }

            return View();
        }
    }
}