using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{ 
    public class User
    {
        public int id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int MerchID { get; set; }
        public bool IsAdmin { get; set; }

        public virtual Merch Merch { get; set; }
        public virtual ICollection<ItemMerch> ItemMerches { get; set; }
    }
}