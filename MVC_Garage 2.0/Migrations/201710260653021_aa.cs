namespace MVC_Garage_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aa : DbMigration
    {
        public override void Up()
        {
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
        }
    }
}
