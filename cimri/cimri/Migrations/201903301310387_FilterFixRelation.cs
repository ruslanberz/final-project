namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FilterFixRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FilterCategories", "Filter_id", "dbo.Filters");
            DropForeignKey("dbo.FilterCategories", "Category_id", "dbo.Categories");
            DropIndex("dbo.FilterCategories", new[] { "Filter_id" });
            DropIndex("dbo.FilterCategories", new[] { "Category_id" });
            AddColumn("dbo.Filters", "Category_id", c => c.Int());
            CreateIndex("dbo.Filters", "Category_id");
            AddForeignKey("dbo.Filters", "Category_id", "dbo.Categories", "id");
            DropTable("dbo.FilterCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FilterCategories",
                c => new
                    {
                        Filter_id = c.Int(nullable: false),
                        Category_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Filter_id, t.Category_id });
            
            DropForeignKey("dbo.Filters", "Category_id", "dbo.Categories");
            DropIndex("dbo.Filters", new[] { "Category_id" });
            DropColumn("dbo.Filters", "Category_id");
            CreateIndex("dbo.FilterCategories", "Category_id");
            CreateIndex("dbo.FilterCategories", "Filter_id");
            AddForeignKey("dbo.FilterCategories", "Category_id", "dbo.Categories", "id", cascadeDelete: true);
            AddForeignKey("dbo.FilterCategories", "Filter_id", "dbo.Filters", "id", cascadeDelete: true);
        }
    }
}
