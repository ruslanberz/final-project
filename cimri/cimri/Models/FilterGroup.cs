using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class FilterGroup
    {
        public int id { get; set; }
        public string Name { get; set; }
        public ICollection<Filter> Filters { get; set; }
    }
}