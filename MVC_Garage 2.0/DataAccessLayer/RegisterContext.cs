using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MVC_Garage_2._0.DataAccessLayer
{
    public class RegisterContext : DbContext
    {
        
        public RegisterContext()
            : base ("ParkedVehicleContext")
        {
        }

        public DbSet<Models.ParkedVehicle> ParkedVehicles { get; set; }
        public DbSet<Models.Parking> Parkings { get; set; }

        public System.Data.Entity.DbSet<MVC_Garage_2._0.Models.ViewModels.Receipt> Receipts { get; set; }

        public System.Data.Entity.DbSet<MVC_Garage_2._0.Models.ViewModels.VehicleStats> VehicleStats { get; set; }
    }
}