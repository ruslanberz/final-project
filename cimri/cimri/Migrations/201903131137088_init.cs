namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsFinal = c.Boolean(nullable: false),
                        IsMain = c.Boolean(nullable: false),
                        ClickCount = c.Int(nullable: false),
                        SiteRecommendID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.CategorieSubs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        SubcategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.CategoryFilters",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.CategoryFilterValues",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        StartValue = c.String(),
                        EndValue = c.String(),
                        IsSingle = c.Boolean(nullable: false),
                        CategoryFilterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.CategoryFilters", t => t.CategoryFilterID, cascadeDelete: true)
                .Index(t => t.CategoryFilterID);
            
            CreateTable(
                "dbo.CategoryPhotoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        SmallPhoto = c.String(),
                        NormalPhoto = c.String(),
                        BigPhoto = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        Name = c.String(),
                        DescriptionShort = c.String(),
                        DescriptionLong = c.String(),
                        ClickCount = c.Int(nullable: false),
                        Feature1 = c.String(),
                        Feature2 = c.String(),
                        Feature3 = c.String(),
                        Feature4 = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        PublishDate = c.DateTime(nullable: false),
                        ItemID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Items", t => t.ItemID, cascadeDelete: true)
                .Index(t => t.ItemID);
            
            CreateTable(
                "dbo.ItemDetails",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ItemID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Items", t => t.ItemID, cascadeDelete: true)
                .Index(t => t.ItemID);
            
            CreateTable(
                "dbo.ItemMerches",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ItemID = c.Int(nullable: false),
                        PriceNormal = c.Double(nullable: false),
                        IsSale = c.Boolean(nullable: false),
                        PriceSale = c.Double(nullable: false),
                        ShippingTypeID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        SalePercent = c.Int(nullable: false),
                        ShippiingType_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Items", t => t.ItemID, cascadeDelete: true)
                .ForeignKey("dbo.ShippiingTypes", t => t.ShippiingType_id)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ItemID)
                .Index(t => t.UserID)
                .Index(t => t.ShippiingType_id);
            
            CreateTable(
                "dbo.ShippiingTypes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Description = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        MerchID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Merches", t => t.MerchID, cascadeDelete: true)
                .Index(t => t.MerchID);
            
            CreateTable(
                "dbo.Merches",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Link = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ItemPhotoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Photo = c.String(),
                        ItemID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Items", t => t.ItemID, cascadeDelete: true)
                .Index(t => t.ItemID);
            
            CreateTable(
                "dbo.SiteRecommends",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ItemID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.ItemID, cascadeDelete: false)
                .Index(t => t.ItemID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.SeaarchTags",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        Text = c.String(),
                        SearchCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Statics",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Logo = c.String(),
                        Facebook = c.String(),
                        Twitter = c.String(),
                        LinkedIn = c.String(),
                        Instagram = c.String(),
                        Youtube = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SeaarchTags", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.SiteRecommends", "ItemID", "dbo.Items");
            DropForeignKey("dbo.SiteRecommends", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.ItemPhotoes", "ItemID", "dbo.Items");
            DropForeignKey("dbo.Users", "MerchID", "dbo.Merches");
            DropForeignKey("dbo.ItemMerches", "UserID", "dbo.Users");
            DropForeignKey("dbo.ItemMerches", "ShippiingType_id", "dbo.ShippiingTypes");
            DropForeignKey("dbo.ItemMerches", "ItemID", "dbo.Items");
            DropForeignKey("dbo.ItemDetails", "ItemID", "dbo.Items");
            DropForeignKey("dbo.Comments", "ItemID", "dbo.Items");
            DropForeignKey("dbo.Items", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.CategoryPhotoes", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.CategoryFilterValues", "CategoryFilterID", "dbo.CategoryFilters");
            DropForeignKey("dbo.CategoryFilters", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.CategorieSubs", "CategoryID", "dbo.Categories");
            DropIndex("dbo.SeaarchTags", new[] { "CategoryID" });
            DropIndex("dbo.SiteRecommends", new[] { "CategoryID" });
            DropIndex("dbo.SiteRecommends", new[] { "ItemID" });
            DropIndex("dbo.ItemPhotoes", new[] { "ItemID" });
            DropIndex("dbo.Users", new[] { "MerchID" });
            DropIndex("dbo.ItemMerches", new[] { "ShippiingType_id" });
            DropIndex("dbo.ItemMerches", new[] { "UserID" });
            DropIndex("dbo.ItemMerches", new[] { "ItemID" });
            DropIndex("dbo.ItemDetails", new[] { "ItemID" });
            DropIndex("dbo.Comments", new[] { "ItemID" });
            DropIndex("dbo.Items", new[] { "CategoryID" });
            DropIndex("dbo.CategoryPhotoes", new[] { "CategoryID" });
            DropIndex("dbo.CategoryFilterValues", new[] { "CategoryFilterID" });
            DropIndex("dbo.CategoryFilters", new[] { "CategoryID" });
            DropIndex("dbo.CategorieSubs", new[] { "CategoryID" });
            DropTable("dbo.Statics");
            DropTable("dbo.SeaarchTags");
            DropTable("dbo.SiteRecommends");
            DropTable("dbo.ItemPhotoes");
            DropTable("dbo.Merches");
            DropTable("dbo.Users");
            DropTable("dbo.ShippiingTypes");
            DropTable("dbo.ItemMerches");
            DropTable("dbo.ItemDetails");
            DropTable("dbo.Comments");
            DropTable("dbo.Items");
            DropTable("dbo.CategoryPhotoes");
            DropTable("dbo.CategoryFilterValues");
            DropTable("dbo.CategoryFilters");
            DropTable("dbo.CategorieSubs");
            DropTable("dbo.Categories");
        }
    }
}
