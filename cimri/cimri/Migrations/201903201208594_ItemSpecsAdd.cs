namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemSpecsAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemSpecs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ItemID = c.Int(nullable: false),
                        FilterId = c.Int(nullable: false),
                        FilterValueID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Filters", t => t.FilterId, cascadeDelete: true)
                .ForeignKey("dbo.FilterValues", t => t.FilterValueID, cascadeDelete: false)
                .ForeignKey("dbo.Items", t => t.ItemID, cascadeDelete: true)
                .Index(t => t.ItemID)
                .Index(t => t.FilterId)
                .Index(t => t.FilterValueID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemSpecs", "ItemID", "dbo.Items");
            DropForeignKey("dbo.ItemSpecs", "FilterValueID", "dbo.FilterValues");
            DropForeignKey("dbo.ItemSpecs", "FilterId", "dbo.Filters");
            DropIndex("dbo.ItemSpecs", new[] { "FilterValueID" });
            DropIndex("dbo.ItemSpecs", new[] { "FilterId" });
            DropIndex("dbo.ItemSpecs", new[] { "ItemID" });
            DropTable("dbo.ItemSpecs");
        }
    }
}
