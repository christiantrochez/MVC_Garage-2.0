namespace MVC_Garage_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Required : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ParkedVehicles", "RegistrationNumber", c => c.String(nullable: false));
            AlterColumn("dbo.ParkedVehicles", "VehicleBrand", c => c.String(nullable: false));
            AlterColumn("dbo.ParkedVehicles", "VehicleModel", c => c.String(nullable: false));
            DropTable("dbo.Receipts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Receipts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegistrationNumber = c.String(),
                        CheckOutDate = c.DateTime(nullable: false),
                        CheckInDate = c.DateTime(nullable: false),
                        CostPerMinute = c.Int(nullable: false),
                        TotalParkedTime = c.Int(nullable: false),
                        TotalCost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.ParkedVehicles", "VehicleModel", c => c.String());
            AlterColumn("dbo.ParkedVehicles", "VehicleBrand", c => c.String());
            AlterColumn("dbo.ParkedVehicles", "RegistrationNumber", c => c.String());
        }
    }
}
