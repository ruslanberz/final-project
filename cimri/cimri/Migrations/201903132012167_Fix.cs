namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IndexDownPromoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        IsActive = c.Boolean(nullable: false),
                        Image = c.String(),
                        Link = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.IndexSliderBigItems",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Link = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.IndexSliderSales",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        IsActive = c.Boolean(nullable: false),
                        ItemMerchID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.ItemMerches", t => t.ItemMerchID, cascadeDelete: true)
                .Index(t => t.ItemMerchID);
            
            CreateTable(
                "dbo.IndexUnderSearchLinks",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Link = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.IndexUpperPromoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        IsActive = c.Boolean(nullable: false),
                        Image = c.String(),
                        Link = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IndexSliderSales", "ItemMerchID", "dbo.ItemMerches");
            DropIndex("dbo.IndexSliderSales", new[] { "ItemMerchID" });
            DropTable("dbo.IndexUpperPromoes");
            DropTable("dbo.IndexUnderSearchLinks");
            DropTable("dbo.IndexSliderSales");
            DropTable("dbo.IndexSliderBigItems");
            DropTable("dbo.IndexDownPromoes");
        }
    }
}
