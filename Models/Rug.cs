using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TenRugs.Models
{
    public class Rug
    {
        public int Id { get; set; }
        public string RugName { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }
        public string Material { get; set; }  
        public string Color { get; set; }
        public string Url { get; set; }
              
    }
}