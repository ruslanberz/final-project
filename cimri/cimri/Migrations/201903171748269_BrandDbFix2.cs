namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BrandDbFix2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Brand_id", c => c.Int());
            CreateIndex("dbo.Items", "Brand_id");
            AddForeignKey("dbo.Items", "Brand_id", "dbo.Brands", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "Brand_id", "dbo.Brands");
            DropIndex("dbo.Items", new[] { "Brand_id" });
            DropColumn("dbo.Items", "Brand_id");
        }
    }
}
