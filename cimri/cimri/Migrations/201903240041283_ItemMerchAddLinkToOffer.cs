namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemMerchAddLinkToOffer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemMerches", "LinkToOffer", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemMerches", "LinkToOffer");
        }
    }
}
