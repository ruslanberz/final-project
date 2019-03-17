namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SearchDbTableFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SeaarchTags", "CategoryID", "dbo.Categories");
            DropIndex("dbo.SeaarchTags", new[] { "CategoryID" });
            AlterColumn("dbo.SeaarchTags", "CategoryID", c => c.Int());
            CreateIndex("dbo.SeaarchTags", "CategoryID");
            AddForeignKey("dbo.SeaarchTags", "CategoryID", "dbo.Categories", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SeaarchTags", "CategoryID", "dbo.Categories");
            DropIndex("dbo.SeaarchTags", new[] { "CategoryID" });
            AlterColumn("dbo.SeaarchTags", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.SeaarchTags", "CategoryID");
            AddForeignKey("dbo.SeaarchTags", "CategoryID", "dbo.Categories", "id", cascadeDelete: true);
        }
    }
}
