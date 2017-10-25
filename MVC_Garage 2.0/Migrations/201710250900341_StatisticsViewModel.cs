namespace MVC_Garage_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatisticsViewModel : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VehicleListItems");
        }
    }
}
