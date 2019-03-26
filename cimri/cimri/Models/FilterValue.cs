using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class FilterValue
    {
        public int id { get; set; }
        public string SatrValue { get; set; }
        public string EndValue { get; set; }
        public bool IsSingle { get; set; }
        public int FilterID { get; set; }
        public string Prefix { get; set; }
        public virtual Filter Filter { get; set; }
        public int ItemsCount { get; set; }

    }
}