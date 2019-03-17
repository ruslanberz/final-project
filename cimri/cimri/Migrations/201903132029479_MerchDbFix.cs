namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MerchDbFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Merches", "İmage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Merches", "İmage");
        }
    }
}
