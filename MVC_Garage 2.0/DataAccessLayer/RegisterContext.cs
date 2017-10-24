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

    }
}