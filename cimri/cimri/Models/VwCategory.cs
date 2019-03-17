using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class VwCategory
    {
        public Category Category { get; set; }
        public int CategotyItemCount { get; set; }
        public List<VwSubcategory> Subcategories { get; set; }
        public List<Item> Items { get; set; }
    }
}