using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class Filter
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int FilterGroupID { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<FilterValue> FilterValues { get; set; }
        public virtual FilterGroup FilterGroup { get; set; }
        public bool? IsFiltered { get; set; }
        public bool? IsComparable { get; set; }
    }
}