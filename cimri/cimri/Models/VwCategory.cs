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
        public List<Filter> Filters { get; set; }
        public int ItemsCount { get; set; }
        public int PagesCount { get; set; }
        public int CurrentPage { get; set; }
        public bool HasChildren { get; set; }
        public List<Item> SiteSelected { get; set; }
        public string SearchQuery { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
    }
}