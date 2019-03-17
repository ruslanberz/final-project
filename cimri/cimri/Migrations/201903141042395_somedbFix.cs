namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class somedbFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "SiteRecommendID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "SiteRecommendID", c => c.Int(nullable: false));
        }
    }
}
