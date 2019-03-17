namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ItemMerches", "PriceSale", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ItemMerches", "PriceSale", c => c.Double(nullable: false));
        }
    }
}
