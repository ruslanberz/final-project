namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MerchLinkFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Merches", "LinkWeb", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Merches", "LinkWeb");
        }
    }
}
