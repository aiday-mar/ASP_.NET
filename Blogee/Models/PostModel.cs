using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blogee.Models
{
    public class PostModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string Date { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }

        public string Tags { get; set; }

        public List<Like> listLikes { get; set; }

        public List<Comment> listComments { get; set; } 
    }
}