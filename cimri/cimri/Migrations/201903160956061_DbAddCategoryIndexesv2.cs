namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbAddCategoryIndexesv2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryIDIndexes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CatIndex = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CategoryIDIndexes");
        }
    }
}
