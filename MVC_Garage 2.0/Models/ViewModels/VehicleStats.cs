using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Garage_2._0.Models.ViewModels
{
    public class VehicleStats
    {
        public int Id { get; set; }
        public int ParkedTime { get; set; }
        public int TotalParkedCost { get; set; }
        public string RegNo { get; set; }
        public string VehType { get; set; }
        public int TotalNrOfWheels { get; set; }
    }
}