using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class ItemDetail
    {
        public int id { get; set; }
        public int ItemID { get; set; }
        public int UserID { get; set; }
        public string Text { get; set; }

        public virtual Item Item { get; set; }
    }
}