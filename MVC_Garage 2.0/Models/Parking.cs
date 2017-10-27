using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Garage_2._0.Models
{
    public class Parking
    {
        public int Id { get; set; }
        public int WhatIsParked { get; set; } //0 empty, 1 one MC, 2 two MC, 3 full
    }
}