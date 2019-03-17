using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class SeaarchTag
    {
        public int id { get; set; }
        public int ?CategoryID { get; set; }
        public string Text { get; set; }
        public int SearchCount { get; set; }

        public virtual Category Category { get; set; }
    }
}