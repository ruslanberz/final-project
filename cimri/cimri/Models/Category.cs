using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class Category
    {
        public int id { get; set; }
        public string Name { get; set; }
        
        public int ClickCount { get; set; }
        public int? SiteRecommendID { get; set; }
        public string DescriptionShort { get; set; }
        public string DescriptionLong { get; set; }
        public int? ParentCategryId { get; set; }
        public virtual Category Parent { get; set; }
        public virtual ICollection<Filter> Filters { get; set; }
        public virtual ICollection<Category> Children { get; set; }
        public virtual ICollection<CategoryPhoto> CategoryPhotos{ get; set; }
        public virtual ICollection<SeaarchTag> SeaarchTags { get; set; }
        public virtual ICollection<SiteRecommend> SiteRecommends { get; set; }
        public virtual ICollection<CategorieSub> CategorieSubs { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}