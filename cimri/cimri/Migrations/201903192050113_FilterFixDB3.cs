namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FilterFixDB3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FilterValues",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        SatrValue = c.String(),
                        EndValue = c.String(),
                        IsSingle = c.Boolean(nullable: false),
                        FilterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Filters", t => t.FilterID, cascadeDelete: true)
                .Index(t => t.FilterID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FilterValues", "FilterID", "dbo.Filters");
            DropIndex("dbo.FilterValues", new[] { "FilterID" });
            DropTable("dbo.FilterValues");
        }
    }
}
