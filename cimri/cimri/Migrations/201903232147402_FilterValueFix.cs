namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FilterValueFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FilterValues", "ItemsCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FilterValues", "ItemsCount");
        }
    }
}
