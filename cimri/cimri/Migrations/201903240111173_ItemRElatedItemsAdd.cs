namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemRElatedItemsAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "RelatedItem1", c => c.Int());
            AddColumn("dbo.Items", "RelatedItem2", c => c.Int());
            AddColumn("dbo.Items", "RelatedItem3", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "RelatedItem3");
            DropColumn("dbo.Items", "RelatedItem2");
            DropColumn("dbo.Items", "RelatedItem1");
        }
    }
}
