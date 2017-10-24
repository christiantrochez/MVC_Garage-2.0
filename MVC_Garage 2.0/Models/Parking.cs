using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Garage_2._0.Models
{
    public class Parking
    {
        public int AvailableParkingSpots { get; set; }
        public int[] ParkingSpots { get; set; }
    }
}