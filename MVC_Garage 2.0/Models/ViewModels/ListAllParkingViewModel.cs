using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Garage_2._0.Models.ViewModels
{
    public class ListAllParkingViewModel
    {
        [Display(Name = "Parking Spot No")]
        public int ParkingSpotNumber { get; set; }
        [Display(Name = "Parking Spot Status")]
        public string ParkingSpotStatus { get; set; }
    }
}