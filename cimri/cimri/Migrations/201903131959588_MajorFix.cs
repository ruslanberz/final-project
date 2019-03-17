namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MajorFix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Logo = c.String(),
                        Rating = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            AddColumn("dbo.Categories", "DescriptionShort", c => c.String());
            AddColumn("dbo.Categories", "DescriptionLong", c => c.String());
            AddColumn("dbo.Merches", "Rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Brands", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Brands", new[] { "CategoryID" });
            DropColumn("dbo.Merches", "Rating");
            DropColumn("dbo.Categories", "DescriptionLong");
            DropColumn("dbo.Categories", "DescriptionShort");
            DropTable("dbo.Brands");
        }
    }
}
