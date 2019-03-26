using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class FiltersCont
    {
        public int FilterID { get; set; }
        public List<int?> FilterValues { get; set; }
    }
}