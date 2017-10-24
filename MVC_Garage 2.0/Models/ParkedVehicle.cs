using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Garage_2._0.Models
{
    public enum VehicleType
    {
        Car,
        Bus,
        Boat,
        Airplane,
        Motorcycle
    }

    public enum Color
    {
        Black,
        Blue,
        Red,
        White,
        Greeen
    }


    public class ParkedVehicle
    {
        public int Id { get; set; }

        [Display(Name ="Registratior Number")]
        public string RegistrationNumber { get; set; }

        [Display(Name ="Number of wheels")]
        [Range(0, 50, ErrorMessage ="The number of wheels must be between 0 and 50")]
        public int NumberOfWheels { get; set; }

        [Display(Name ="Vehicle brand")]
        public string VehicleBrand { get; set; }

        [Display(Name ="Vehicle model")]
        public string VehicleModel { get; set; }

        [Display(Name ="Parking Date and Time")]
        public DateTime InDate { get; set; }

        [Display(Name ="Vehicle type")]
        public VehicleType VehicleTYpe { get; set; }


        public Color Color { get; set; }
    }
}