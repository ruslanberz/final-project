using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class ShippiingType
    {
        public int id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ItemMerch> ItemMerches { get; set; }
    }
}