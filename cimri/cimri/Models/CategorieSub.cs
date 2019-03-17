using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class CategorieSub
    {
        public int id { get; set; }
        public int CategoryID { get; set; }
        public int SubcategoryID { get; set; }

        public virtual Category Category { get; set; }
    }
}