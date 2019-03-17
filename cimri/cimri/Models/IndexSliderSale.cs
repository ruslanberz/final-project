using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class IndexSliderSale
    {
        public int id { get; set; }
        public bool IsActive { get; set; }
        public int ItemMerchID { get; set; }

        public virtual ItemMerch ItemMerch { get; set; }
    }
}