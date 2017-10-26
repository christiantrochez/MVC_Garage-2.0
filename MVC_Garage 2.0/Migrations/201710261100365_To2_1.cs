namespace MVC_Garage_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class To2_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParkedVehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegistrationNumber = c.String(),
                        NumberOfWheels = c.Int(nullable: false),
                        VehicleBrand = c.String(),
                        VehicleModel = c.String(),
                        InDate = c.DateTime(nullable: false),
                        VehicleTYpe = c.Int(nullable: false),
                        Color = c.Int(nullable: false),
                        ParkingSpot = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Parkings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WhatIsParked = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.VehicleStats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParkedTime = c.Int(nullable: false),
                        TotalParkedCost = c.Int(nullable: false),
                        RegNo = c.String(),
                        VehType = c.String(),
                        TotalNrOfWheels = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VehicleStats");
            DropTable("dbo.Receipts");
            DropTable("dbo.Parkings");
            DropTable("dbo.ParkedVehicles");
        }
    }
}
