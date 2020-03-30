using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blogee.Models;
using System.Data.SqlClient;
using System.Text;
using System.Web.Helpers;
using System.Data;

namespace Blogee.Controllers
{
    public class MessagesController : Controller
    {

        // GET: Messages
        public ActionResult Messages() { return View(); }

        [HttpPost]
        public ActionResult NewContact(SearchModel s) {

            string potentialContact = s.SearchTerm;
            List<UserModel> listPotentialContacts = new List<UserModel>(); // needs to be populated hereby

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "sqlserver-aiday.database.windows.net";
                builder.UserID = "serverusername";
                builder.Password = "ConnectDB1";
                builder.InitialCatalog = "Blogee_db";

                using (SqlConnection openCon = new SqlConnection(builder.ConnectionString))
                {
                    // here we try to find the user not the post
                    string findContact = "SELECT * FROM dbo.Users WHERE Username LIKE @PotentialContact ";

                    using (SqlCommand queryFindContact = new SqlCommand(findContact))
                    {
                        queryFindContact.Connection = openCon;
                        queryFindContact.Parameters.AddWithValue("@PotentialContact", "%" + potentialContact + "%");
                        openCon.Open();
                        var reader = queryFindContact.ExecuteReader();

                        // for some reason we don't enter into the while loop below
                        while (reader.Read())
                        {
                            listPotentialContacts.Add(item: new UserModel
                            {
                                Username = reader["Username"].ToString(),
                                Name = reader["Name"].ToString(),
                            });
                        }
                    }

                    s.ListUsers = listPotentialContacts;
                    openCon.Close();
                }


            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }

            return View("~/Views/Messages/Messages.cshtml", model:s);
        }


        [HttpPost]
        public void ChooseChat(string username)
        {
            OtherUserModel.OtherUsername = username;
            
            // code below written in order to retrieve the rest of the information about the user like the
            // common contacts, the name, etc. So far only the username of the person is known.
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "sqlserver-aiday.database.windows.net";
                builder.UserID = "serverusername";
                builder.Password = "ConnectDB1";
                builder.InitialCatalog = "Blogee_db";

                using (SqlConnection openCon = new SqlConnection(builder.ConnectionString))
                {
                    string findContactChatWith = "SELECT * FROM dbo.Users WHERE Username = @ChatWith";

                    using (SqlCommand queryFindContactChatWith = new SqlCommand(findContactChatWith))
                    {
                        queryFindContactChatWith.Connection = openCon;
                        queryFindContactChatWith.Parameters.AddWithValue("@ChatWith", username);
                        openCon.Open();
                        var reader = queryFindContactChatWith.ExecuteReader();

                        while (reader.Read())
                        {
                            OtherUserModel.OtherName = reader["Name"].ToString();
                            
                        }
                    }

                    openCon.Close();
                }

            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }

        public ActionResult Chat()
        {
            SearchModel contactChatWithModel = new SearchModel() ;
            contactChatWithModel.UserOfInterest.Username = OtherUserModel.OtherUsername;

            return View("/Views/Messages/Chat.cshtml", model:contactChatWithModel);
        }

    }
}