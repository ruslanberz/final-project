using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class VwMobileCategoryAndItems
    {
        public Category Category { get; set; }
        public List<Item> Items { get; set; }
    }
}