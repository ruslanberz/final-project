using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class VwCategoryIndex
    {
        public int id { get; set; }
        public List<Item> Items { get; set; }
        public List<ItemPhoto> itemPhotos { get; set; }
        public List<ItemMerch> itemMerches { get; set; }
        public Category Category { get; set; }
    }
}