using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// to add the extension from the controller, you must add the below mvc library
using System.Web.Mvc;

namespace Blogee.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            // we found out that when the submit form is clicked then the program enters the Index method of the Dashboard Controller 
            return View();
        }

        // here we found out that when we write Dashboard/AddNewUser after localhost, then we access the AddNewUser method in the DashboardController
        // this means that all the cshtml files in the Dashboard folder of the View folder are related to some method in the action
        
        [HttpGet]
        public void AddNewUser()
        {
            // GET: rec
            string username = "";
            string email = "";
            string password = "";

            List<string> myList = new List<string>();
            
            // check if the data posted is accessed here 
        }
    }
}