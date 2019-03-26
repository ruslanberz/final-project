namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemSpecsAddfix2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Filters", "IsFiltered", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Filters", "IsFiltered");
        }
    }
}
