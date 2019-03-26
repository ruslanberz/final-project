using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class Merch
    {
        public int id { get; set; }
        public string Link { get; set; }
        public string LinkWeb { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public string İmage { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<ItemMerch> ItemMerches { get; set; }
    }
}