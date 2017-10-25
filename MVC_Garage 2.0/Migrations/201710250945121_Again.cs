namespace MVC_Garage_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Again : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Parkings");
            DropTable("dbo.VehicleListItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VehicleListItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegistrationNumber = c.String(),
                        VehicleType = c.Int(nullable: false),
                        Color = c.Int(nullable: false),
                        InDate = c.DateTime(nullable: false),
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
            
        }
    }
}
