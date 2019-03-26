using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class ItemSpec
    {
        public int id { get; set; }
        public int ItemID { get; set; }
        public int FilterId { get; set; }
        public int FilterValueID { get; set; }
        public virtual Filter Filter { get; set; }
        public virtual FilterValue FilterValue { get; set; }
        public virtual Item Item { get; set; }
    }
}