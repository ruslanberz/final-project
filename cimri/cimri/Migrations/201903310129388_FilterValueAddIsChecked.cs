namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FilterValueAddIsChecked : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FilterValues", "IsChecked", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FilterValues", "IsChecked");
        }
    }
}
