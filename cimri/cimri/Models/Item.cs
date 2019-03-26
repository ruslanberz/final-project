using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class Item
    {
        public int id { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string DescriptionShort { get; set; }
        public string DescriptionLong { get; set; }
        public int ClickCount { get; set; }
        public string Feature1 { get; set; }
        public string Feature2 { get; set; }
        public string Feature3 { get; set; }
        public string Feature4 { get; set; }
        public int? RelatedItem1 { get; set; }
        public int? RelatedItem2 { get; set; }
        public int? RelatedItem3 { get; set; }
        public int? RelatedItem4 { get; set; }
        public int? RelatedItem5 { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ItemPhoto> ItemPhotos { get; set; }
        public virtual ICollection<ItemMerch> ItemMerches { get; set; }
        public virtual ICollection<ItemDetail> ItemDetails { get; set; }
        public virtual ICollection<SiteRecommend> SiteRecommends { get; set; }
        public virtual ICollection<ItemSpec> ItemSpecs { get; set; }
    }
}