namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemMerchIsSPONSOREDadd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemMerches", "IsSponsored", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemMerches", "IsSponsored");
        }
    }
}
