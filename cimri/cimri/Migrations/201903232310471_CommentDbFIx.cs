namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentDbFIx : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Rating", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "Rating");
        }
    }
}
