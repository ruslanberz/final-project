namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FilterFixDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Filters",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.FilterCategories",
                c => new
                    {
                        Filter_id = c.Int(nullable: false),
                        Category_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Filter_id, t.Category_id })
                .ForeignKey("dbo.Filters", t => t.Filter_id, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_id, cascadeDelete: true)
                .Index(t => t.Filter_id)
                .Index(t => t.Category_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FilterCategories", "Category_id", "dbo.Categories");
            DropForeignKey("dbo.FilterCategories", "Filter_id", "dbo.Filters");
            DropIndex("dbo.FilterCategories", new[] { "Category_id" });
            DropIndex("dbo.FilterCategories", new[] { "Filter_id" });
            DropTable("dbo.FilterCategories");
            DropTable("dbo.Filters");
        }
    }
}
