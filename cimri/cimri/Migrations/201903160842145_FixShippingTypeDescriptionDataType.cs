namespace cimri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixShippingTypeDescriptionDataType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ShippiingTypes", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ShippiingTypes", "Description", c => c.Int(nullable: false));
        }
    }
}
