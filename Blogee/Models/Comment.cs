using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blogee.Models
{
    public class Comment
    {
        public string Content { get; set; }

        public string Author { get; set; }

        public string Date { get; set; }

        public List<Like> listLikes { get; set; }
    }
}