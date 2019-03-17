namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryDbFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "ParentCategryId", c => c.Int());
            AddColumn("dbo.Categories", "Parent_id", c => c.Int());
            CreateIndex("dbo.Categories", "Parent_id");
            AddForeignKey("dbo.Categories", "Parent_id", "dbo.Categories", "id");
            DropColumn("dbo.Categories", "IsFinal");
            DropColumn("dbo.Categories", "IsMain");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "IsMain", c => c.Boolean(nullable: false));
            AddColumn("dbo.Categories", "IsFinal", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Categories", "Parent_id", "dbo.Categories");
            DropIndex("dbo.Categories", new[] { "Parent_id" });
            DropColumn("dbo.Categories", "Parent_id");
            DropColumn("dbo.Categories", "ParentCategryId");
        }
    }
}
