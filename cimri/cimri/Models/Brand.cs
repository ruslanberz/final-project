using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class Brand
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public int Rating { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}