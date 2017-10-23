namespace MVC_Garage_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RegNoToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ParkedVehicles", "RegistrationNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ParkedVehicles", "RegistrationNumber", c => c.Int(nullable: false));
        }
    }
}
