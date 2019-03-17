using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class CategoryPhoto
    {
        public int id { get; set; }
        public int CategoryID { get; set; }
        public string SmallPhoto { get; set; }
        public string NormalPhoto { get; set; }
        public string BigPhoto { get; set; }

        public virtual Category Category { get; set; }
    }
}