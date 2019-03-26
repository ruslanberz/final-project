namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemSpecsAddfix3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FilterValues", "Prefix", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FilterValues", "Prefix");
        }
    }
}
