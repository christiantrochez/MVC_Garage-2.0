using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Garage_2._0.Models.ViewModels
{
    public class Receipt
    {
        public int Id { get; set; }

        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; }

        [Display(Name = "Check-Out Time")]
        public DateTime CheckOutDate { get; set; }

        [Display(Name = "Check-In Time")]
        public DateTime CheckInDate { get; set; }

        [Display(Name = "Parking Cost/Min")]
        public int CostPerMinute { get; set; }

        [Display(Name = "Total Parked Time")]
        public int TotalParkedTime { get; set; }

        [Display(Name = "Total Cost")]
        public int TotalCost { get; set; }
    }
}