using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Models
{
    public class Feed
    {
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
        public int Likes { get; set; }
        public string Author { get; set; }
    }
}