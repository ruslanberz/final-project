namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FilterFixDB2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Filters", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Filters", "Name", c => c.Int(nullable: false));
        }
    }
}
