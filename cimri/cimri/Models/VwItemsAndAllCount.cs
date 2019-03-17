using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class VwItemsAndAllCount
    {
        public int count { get; set; }
        public List<Item> Items { get; set; }
    }
}