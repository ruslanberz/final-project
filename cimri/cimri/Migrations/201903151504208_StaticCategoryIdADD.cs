namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StaticCategoryIdADD : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Statics", "CategoryIndex1ID", c => c.Int(nullable: false));
            AddColumn("dbo.Statics", "CategoryIndex2ID", c => c.Int(nullable: false));
            AddColumn("dbo.Statics", "CategoryIndex3ID", c => c.Int(nullable: false));
            AddColumn("dbo.Statics", "CategoryIndex4ID", c => c.Int(nullable: false));
            AddColumn("dbo.Statics", "CategoryIndex5ID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Statics", "CategoryIndex5ID");
            DropColumn("dbo.Statics", "CategoryIndex4ID");
            DropColumn("dbo.Statics", "CategoryIndex3ID");
            DropColumn("dbo.Statics", "CategoryIndex2ID");
            DropColumn("dbo.Statics", "CategoryIndex1ID");
        }
    }
}
