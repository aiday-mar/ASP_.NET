using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blogee.Models
{
    public class SearchModel
    {
        public SearchModel()
        {
            this.UserOfInterest = new UserModel();
        }
        // don't forget to add the get and set in order to be able to add the TextBoxFor functionality
        public string SearchTerm { get; set; }

        // the user we want to chat with
        public UserModel UserOfInterest { get; set; }

        public List<PostModel> ListPosts { get; set; }

        public List<UserModel> ListUsers { get; set; }
    }
}