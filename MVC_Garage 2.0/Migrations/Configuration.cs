namespace MVC_Garage_2._0.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MVC_Garage_2._0.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MVC_Garage_2._0.DataAccessLayer.RegisterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MVC_Garage_2._0.DataAccessLayer.RegisterContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.ParkedVehicles.AddOrUpdate(
                v => v.RegistrationNumber,
                new Models.ParkedVehicle { RegistrationNumber = "ABC 123", Color = Color.Red, NumberOfWheels = 4, VehicleTYpe = VehicleType.Car, VehicleBrand = "Ferrari", VehicleModel = "F40", InDate = DateTime.Parse("2017-10-21 12:00:00"), ParkingSpot=1 },
                new Models.ParkedVehicle { RegistrationNumber = "BBB2345", Color = Color.White, NumberOfWheels = 0, VehicleTYpe = VehicleType.Boat, VehicleBrand = "FerryBrand", VehicleModel = "V234", InDate = DateTime.Parse("2017-10-17 06:30:00"), ParkingSpot=2 },
                new Models.ParkedVehicle { RegistrationNumber = "EFG 478", Color = Color.Greeen, NumberOfWheels = 6, VehicleTYpe = VehicleType.Bus, VehicleBrand = "NeoPlan", VehicleModel = "Super", InDate = DateTime.Parse("2017-10-20 18:00:00"), ParkingSpot=4 },
                new Models.ParkedVehicle { RegistrationNumber = "LFF 34-6678", Color = Color.Blue, NumberOfWheels = 32, VehicleTYpe = VehicleType.Airplane, VehicleBrand = "AirBus", VehicleModel = "A380", InDate = DateTime.Parse("2017-10-15 04:15:20"), ParkingSpot=13 },
                new Models.ParkedVehicle { RegistrationNumber = "Racer", Color = Color.Red, NumberOfWheels = 2, VehicleTYpe = VehicleType.Motorcycle, VehicleBrand = "Yamaha", VehicleModel = "Hogata", InDate = DateTime.Parse("2017-10-22 18:45:00"), ParkingSpot=9 },
                new Models.ParkedVehicle { RegistrationNumber = "HH 25-45-ERF", Color = Color.White, NumberOfWheels = 4, VehicleTYpe = VehicleType.Car, VehicleBrand = "Porche", VehicleModel = "958", InDate = DateTime.Parse("2017-10-20 00:00:00"), ParkingSpot=10 },
                new Models.ParkedVehicle { RegistrationNumber = "134-FRG-X", Color = Color.Black, NumberOfWheels = 4, VehicleTYpe = VehicleType.Airplane, VehicleBrand = "Buggati", VehicleModel = "Veron", InDate = DateTime.Parse("2017-10-19 23:59:59"), ParkingSpot=11 }
                );

            context.Parkings.AddOrUpdate(
                p => p.Id,
                new Models.Parking { Id = 1, WhatIsParked = 3 }, //Car
                new Models.Parking { Id = 2, WhatIsParked = 3 }, //Boat
                new Models.Parking { Id = 3, WhatIsParked = 3 },//Boat
                new Models.Parking { Id = 4, WhatIsParked = 3 },//Bus
                new Models.Parking { Id = 5, WhatIsParked = 3 },//Bus
                new Models.Parking { Id = 6, WhatIsParked = 0 },//Empty
                new Models.Parking { Id = 7, WhatIsParked = 0 },//Empty
                new Models.Parking { Id = 8, WhatIsParked = 0 },//Empty
                new Models.Parking { Id = 9, WhatIsParked = 1 }, //MC
                new Models.Parking { Id = 10, WhatIsParked = 3 }, //Car
                new Models.Parking { Id = 11, WhatIsParked = 3 }, //Car
                new Models.Parking { Id = 12, WhatIsParked = 0 }, //Empty
                new Models.Parking { Id = 13, WhatIsParked = 3 },//AirPlane
                new Models.Parking { Id = 14, WhatIsParked = 3 },//AirPlane
                new Models.Parking { Id = 15, WhatIsParked = 3 },//AirPlane
                new Models.Parking { Id = 16, WhatIsParked = 0 },//Empty
                new Models.Parking { Id = 17, WhatIsParked = 0 },//Empty
                new Models.Parking { Id = 18, WhatIsParked = 0 },//Empty
                new Models.Parking { Id = 19, WhatIsParked = 0 },//Empty
                new Models.Parking { Id = 20, WhatIsParked = 0 });//Empty


            for (int i = 21; i <= 300; i++)
            {
                context.Parkings.AddOrUpdate(
                     p => p.Id,
                     new Models.Parking { Id = i, WhatIsParked = 0 }

             );
            };
            
             


        }
    }
}
