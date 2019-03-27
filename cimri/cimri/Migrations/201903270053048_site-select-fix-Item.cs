namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class siteselectfixItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "SiteSelection", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "SiteSelection");
        }
    }
}
