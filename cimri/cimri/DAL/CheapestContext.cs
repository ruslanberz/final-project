using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using cimri.Models;

namespace cimri.DAL
{
    public class CheapestContext:DbContext
    {
        public CheapestContext() : base("CheapestContext")
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategorieSub> CategorieSubs { get; set; }
        public DbSet<CategoryFilter> CategoryFilters{ get; set; }
        public DbSet<CategoryFilterValue> CategoryFilterValues { get; set; }
        public DbSet<CategoryPhoto> CategoryPhotos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemDetail> ItemDetails { get; set; }
        public DbSet<ItemMerch> ItemMerches { get; set; }
        public DbSet<ItemPhoto> ItemPhotos { get; set; }
        public DbSet<Merch> Merches { get; set; }
        public DbSet<SeaarchTag> SeaarchTags { get; set; }
        public DbSet<ShippiingType> ShippiingTypes { get; set; }
        public DbSet<Static> Statics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<IndexDownPromo> indexDownPromos { get; set; }
        public DbSet<IndexSliderBigItem> indexSliderBigItems { get; set; }
        public DbSet<IndexSliderSale> indexSliderSales { get; set; }
        public DbSet<IndexUnderSearchLink> indexUnderSearchLinks { get; set; }
        public DbSet<IndexUpperPromo> indexUpperPromos { get; set; }
        public DbSet<CategoryIDIndex> CategoryIDIndexes { get; set; }
    }
}