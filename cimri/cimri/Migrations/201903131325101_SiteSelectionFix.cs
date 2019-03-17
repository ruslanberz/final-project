namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SiteSelectionFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteRecommends", "IsAcive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SiteRecommends", "IsAcive");
        }
    }
}
