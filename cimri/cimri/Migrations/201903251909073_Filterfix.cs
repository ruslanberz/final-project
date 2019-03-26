namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Filterfix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Filters", "IsComparable", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Filters", "IsComparable");
        }
    }
}
