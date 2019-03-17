namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BrandDbFix3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Brands", "CategoryID");

        }
        
        public override void Down()
        {
            AddColumn("dbo.Brands", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Brands", "CategoryID");
            AddForeignKey("dbo.Brands", "CategoryID", "dbo.Categories", "id", cascadeDelete: false);
        }
    }
}
