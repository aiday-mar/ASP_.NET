using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blogee.Models
{
    public static class OtherUserModel
    {
        public static string OtherName { get; set; }

        public static string OtherUsername { get; set; }

        public static string OtherEmail { get; set; }

        public static string OtherPassword { get; set; }

        public static bool OtherLoggedIn = false;
    }
}