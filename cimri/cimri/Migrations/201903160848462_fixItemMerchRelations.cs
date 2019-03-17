namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixItemMerchRelations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemMerches", "MerchID", c => c.Int(nullable: false));
            CreateIndex("dbo.ItemMerches", "MerchID");
            AddForeignKey("dbo.ItemMerches", "MerchID", "dbo.Merches", "id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemMerches", "MerchID", "dbo.Merches");
            DropIndex("dbo.ItemMerches", new[] { "MerchID" });
            DropColumn("dbo.ItemMerches", "MerchID");
        }
    }
}
