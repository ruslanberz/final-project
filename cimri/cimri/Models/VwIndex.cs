﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class VwIndex
    {
        public List<IndexSliderBigItem> indexSliderBigItems { get; set; }
        public IndexUpperPromo indexUpperPromo { get; set; }
        public IndexDownPromo IndexDownPromo { get; set; }
        public List<Merch> Merches { get; set; }
        public List<SeaarchTag> SeaarchTags { get; set; }
        public Static Static { get; set; }
        public List<VwCategoryIndex> CategoryIndexes { get; set; }
        public List<CategoryIDIndex> CategoryIDIndexes { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Item> PopularItems { get; set; }
        public List<Item> SiteSelect { get; set; }
        public List<Category> Categories { get; set; }

    }
}