using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blogee.Models
{
    public static class CurrentUserModel
    {
        public static string CurrentName { get; set; }

        public static string CurrentUsername { get; set; }

        public static string CurrentEmail { get; set; }

        public static string CurrentPassword { get; set; }

        public static bool CurrentLoggedIn = false;
    }
}