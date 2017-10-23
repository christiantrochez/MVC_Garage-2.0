using System;
using System.Collections.Generic;
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
        public int RegistrationNumber { get; set; }
        public int NumberOfWheels { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleModel { get; set; }
        public DateTime InDate { get; set; }
        public VehicleType VehicleTYpe { get; set; }
        public Color Color { get; set; }
    }
}