using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Garage_2._0.Models.ViewModels
{
    public class StatisticsViewModel
    {       
        public Models.VehicleType VehicelType { get; set; }
        public int NumberOfVechileType { get; set; }
        public int TotalNumberOfWheels { get; set; }
        public DateTime TotalParkingTime { get; set; }
    }
}