namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentRatingFIx : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "Rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "Rating", c => c.Double(nullable: false));
        }
    }
}
