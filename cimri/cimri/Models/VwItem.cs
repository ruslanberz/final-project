
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class VwItem
    {
        public Item Item { get; set; }
        public List<ItemMerch> SponsoredMerchs { get; set; }
        public List<ItemMerch> OrdinarMerchs { get; set; }
        public int CommentFiveCount { get; set; }
        public int CommentFourCount { get; set; }
        public int CommentThreeCount { get; set; }
        public int CommentTwoCount { get; set; }
        public int CommentOneCount { get; set; }
        public double OverrallRating { get; set; }
        public List<Item> RelatedItems { get; set; }
        public List<VwItemSpec> ItemSpecs { get; set; }
    }
}