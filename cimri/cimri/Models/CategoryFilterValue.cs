using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class CategoryFilterValue
    {
        public int id { get; set; }
        public string StartValue { get; set; }
        public string EndValue { get; set; }
        public bool IsSingle { get; set; }
        public int CategoryFilterID { get; set; }

        public virtual CategoryFilter CategoryFilter { get; set; }
    }
}