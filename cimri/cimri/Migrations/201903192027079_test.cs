namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CategoryFilters", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.CategoryFilterValues", "CategoryFilterID", "dbo.CategoryFilters");
            DropIndex("dbo.CategoryFilters", new[] { "CategoryID" });
            DropIndex("dbo.CategoryFilterValues", new[] { "CategoryFilterID" });
            DropTable("dbo.CategoryFilters");
            DropTable("dbo.CategoryFilterValues");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.CategoryFilters",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateIndex("dbo.CategoryFilterValues", "CategoryFilterID");
            CreateIndex("dbo.CategoryFilters", "CategoryID");
            AddForeignKey("dbo.CategoryFilterValues", "CategoryFilterID", "dbo.CategoryFilters", "id", cascadeDelete: true);
            AddForeignKey("dbo.CategoryFilters", "CategoryID", "dbo.Categories", "id", cascadeDelete: true);
        }
    }
}
