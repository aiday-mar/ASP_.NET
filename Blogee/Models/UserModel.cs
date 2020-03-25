using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blogee.Models
{
    public class UserModel
    {
        public string Name { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool NewUser { get; set; }

        public bool loggedIn = false;

        // I am not sure if the below is a necessary method since the above already defines a public parameter
       /*
        public bool changeLoggedIn
        {
            get { return loggedIn; }
            set { loggedIn = value; }
        }
        */

    }

}