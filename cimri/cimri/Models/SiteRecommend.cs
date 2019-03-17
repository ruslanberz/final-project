using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class SiteRecommend
    {
        public int id { get; set; }
        public int ItemID { get; set; }
        public int CategoryID { get; set; }
        public bool IsAcive { get; set; }

        public virtual Category Category { get; set; }
        public virtual Item Item { get; set; }
    }
}