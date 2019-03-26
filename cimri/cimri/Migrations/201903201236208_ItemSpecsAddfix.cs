namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemSpecsAddfix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FilterGroups",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Filters", "FilterGroupID", c => c.Int(nullable: false));
            CreateIndex("dbo.Filters", "FilterGroupID");
            AddForeignKey("dbo.Filters", "FilterGroupID", "dbo.FilterGroups", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Filters", "FilterGroupID", "dbo.FilterGroups");
            DropIndex("dbo.Filters", new[] { "FilterGroupID" });
            DropColumn("dbo.Filters", "FilterGroupID");
            DropTable("dbo.FilterGroups");
        }
    }
}
