using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_Garage_2._0.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC_Garage_2._0.Models.ViewModels
{
    public class VehicleListItem
    {
        public int Id { get; set; }

        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; }

        [Display(Name = "Vehicle type")]
        public VehicleType VehicleType { get; set; }

        public Color Color { get; set; }

        [Display(Name = "Parking Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime InDate { get; set; }
    }
}