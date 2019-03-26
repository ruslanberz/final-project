namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemMerchFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemMerches", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemMerches", "Name");
        }
    }
}
