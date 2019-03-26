namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemFixRElated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "RelatedItem4", c => c.Int());
            AddColumn("dbo.Items", "RelatedItem5", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "RelatedItem5");
            DropColumn("dbo.Items", "RelatedItem4");
        }
    }
}
