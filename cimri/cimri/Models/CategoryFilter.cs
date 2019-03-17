using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class CategoryFilter
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }

        public virtual ICollection<CategoryFilterValue> CategoryFilterValues { get; set; }
        public virtual Category Category { get; set; }
    }
}