using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Garage_2._0.DataAccessLayer;
using MVC_Garage_2._0.Models;
using MVC_Garage_2._0.Models.ViewModels;

namespace MVC_Garage_2._0.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private RegisterContext db = new RegisterContext();
        private int MinuteCost = 10;
        public static int TotalGarageCost;
        public string[] vehInfo = new string[200];
        public string[] text = new string[200];
        // GET: ListAllVehicles
        //Started today
        public ActionResult ListAllVehicles(string sortOrder, string searchString, string currentFilter)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.RegSortParam = String.IsNullOrEmpty(sortOrder) ? "regNo_desc" : "";
            ViewBag.DateSortParam = sortOrder == "Date" ? "date_desc" : "Date";


            var allParkedVehicles = db.ParkedVehicles;
           

            var VehicleItems = allParkedVehicles.Select(v => new VehicleListItem
            {
                Id = v.Id,
                RegistrationNumber = v.RegistrationNumber,
                VehicleType = v.VehicleTYpe,
                Color = v.Color,
                InDate = v.InDate
            });

            if (searchString == null)
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                VehicleItems = VehicleItems.Where(v => v.RegistrationNumber.StartsWith(searchString));
            }

            switch (sortOrder)
            {
                case "regNo_desc":
                    {
                        VehicleItems = VehicleItems.OrderByDescending(v => v.RegistrationNumber);
                        break;
                    }
                case "Date":
                    {
                        VehicleItems = VehicleItems.OrderBy(v => v.InDate);
                        break;
                    }
                case "date_desc":
                    {
                        VehicleItems = VehicleItems.OrderByDescending(v => v.InDate);
                        break;
                    }
                default:
                    {
                        VehicleItems = VehicleItems.OrderBy(v => v.RegistrationNumber);
                        break;
                    }
            }


            return View(VehicleItems);
        }

        // GET: Index
        public ActionResult Index()
        {
            return View(db.ParkedVehicles.ToList());
        }
        public ActionResult VehStats(VehicleType? vehicleType)
        {
            var parkedVehicles = from m in db.ParkedVehicles
                                 select m;
            var GarageInfoLst = new List<string>();


            var airCount = parkedVehicles.Where(v => v.VehicleTYpe == VehicleType.Airplane).Count();
            var boatCount = parkedVehicles.Where(v => v.VehicleTYpe == VehicleType.Boat).Count();
            var busCount = parkedVehicles.Where(v => v.VehicleTYpe == VehicleType.Bus).Count();
            var carCount = parkedVehicles.Where(v => v.VehicleTYpe == VehicleType.Car).Count();
            var motCount = parkedVehicles.Where(v => v.VehicleTYpe == VehicleType.Motorcycle).Count();

            
           
            GarageInfoLst.Add($"Airplane count: {airCount}");
            GarageInfoLst.Add($"Boat count: {boatCount}");
            GarageInfoLst.Add($"Bus count: {busCount}");
            GarageInfoLst.Add($"Car count: {carCount}");
            GarageInfoLst.Add($"Motorcycle count: {motCount}");
            
           
            var WheelCount = (from d in db.ParkedVehicles
                              select d.NumberOfWheels).Sum();
            GarageInfoLst.Add($"Total Wheels count: {WheelCount}");
            ViewBag.Text = GarageInfoLst;
            var VehicleInfoLst = new List<VehicleStats>();

            foreach (var veh in parkedVehicles)
            {
                
                VehicleStats vehs = new VehicleStats();
                vehs.Id = veh.Id;
                vehs.RegNo = veh.RegistrationNumber;
                vehs.ParkedTime = (int)DateTime.Now.Subtract(veh.InDate).TotalMinutes;
                vehs.TotalParkedCost = vehs.ParkedTime * MinuteCost;
                
                VehicleInfoLst.Add(vehs);
            }


            return View(VehicleInfoLst);
        }



        // GET: ParkedVehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            if (parkedVehicle == null)
            {
                return HttpNotFound();
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RegistrationNumber,NumberOfWheels,VehicleBrand,VehicleModel,VehicleTYpe,Color")] ParkedVehicle parkedVehicle)
        {
            var currentParking = db.Parkings;

            int parkingSpotNumber = findFirstAvailableSpot(currentParking, parkedVehicle.VehicleTYpe);

            if (parkingSpotNumber == 0)
            {
                return RedirectToAction("ListAllVehicles");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var spotToUpdate = currentParking.Find(parkingSpotNumber);

                    switch (parkedVehicle.VehicleTYpe)
                    {
                        case VehicleType.Car:
                            spotToUpdate.WhatIsParked = 3;
                            break;
                        case VehicleType.Motorcycle:
                            spotToUpdate.WhatIsParked++;
                            break;
                        case VehicleType.Airplane:
                            for (int i = 0; i <= 2; i++)
                            {
                                spotToUpdate = currentParking.Find(parkingSpotNumber + i);
                                spotToUpdate.WhatIsParked = 3;
                            }
                            break;
                        default:
                            for (int i = 0; i <= 1 ; i++)
                            {
                                spotToUpdate = currentParking.Find(parkingSpotNumber + i);
                                spotToUpdate.WhatIsParked = 3;
                            }
                            break;
                    }

                    
                    

                    parkedVehicle.InDate = DateTime.Now;
                    parkedVehicle.ParkingSpot = parkingSpotNumber;
                    db.ParkedVehicles.Add(parkedVehicle);
                    db.SaveChanges();
                    return RedirectToAction("ListAllVehicles");
                }

                return View(parkedVehicle);
            }
        }

        private int findFirstAvailableSpot(DbSet<Parking> currentParking, VehicleType vehicleType)
        {
            List<Parking> freeSpots;

            if (vehicleType != VehicleType.Motorcycle)
            {
                freeSpots = currentParking.Where(s => s.WhatIsParked == 0).ToList();
            }
            else
            {
                freeSpots = currentParking.Where(s => s.WhatIsParked < 3).ToList();
            }

            if (freeSpots.Count() == 0)
            {
                return 0;
            }
            else
            {
                switch (vehicleType)
                {
                    case VehicleType.Car:
                        return freeSpots.FirstOrDefault().Id;
                        break;
                    case VehicleType.Motorcycle:
                        return freeSpots.FirstOrDefault().Id;
                        break;
                    case VehicleType.Airplane:
                        for (int i = 0; i < freeSpots.Count()-2; i++)
                        {
                            if ((freeSpots[i].Id == (freeSpots[i + 1].Id - 1)) &&
                               (freeSpots[i].Id == (freeSpots[i + 2].Id - 2)))
                            {
                                return freeSpots[i].Id;
                            }
                        }
                        return 0;
                        break;
                    default:
                        for (int i = 0; i < freeSpots.Count()-1; i++)
                        {
                            if (freeSpots[i].Id == (freeSpots[i+1].Id - 1))
                            {
                                return freeSpots[i].Id;
                            }
                        }
                        return 0;
                }
                return 0;
            }
        }

        // GET: ParkedVehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            if (parkedVehicle == null)
            {
                return HttpNotFound();
            }
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var parkedVehicleToUpdate = db.ParkedVehicles.Find(id);
            if (TryUpdateModel(parkedVehicleToUpdate, "", new string[] { "RegistrationNumber", "NumberOfWheels", "VehicleBrand", "VehicleModel", "VehicleTYpe", "Color" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("ListAllVehicles");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists !! Tuff Luck");
                }
            }

            return View(parkedVehicleToUpdate);

        }


        //public ActionResult Edit([Bind(Include = "RegistrationNumber,NumberOfWheels,VehicleBrand,VehicleModel,VehicleTYpe,Color")] ParkedVehicle parkedVehicle)
        //{
        //    var vehicleTime = db.ParkedVehicles.AsNoTracking().FirstOrDefault(v => v.Id == parkedVehicle.Id).InDate;

        //    if (ModelState.IsValid)
        //    {
        //        parkedVehicle.InDate = vehicleTime;
        //        db.Entry(parkedVehicle).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("ListAllVehicles");
        //    }
        //    return View(parkedVehicle);
        //}

        // GET: ParkedVehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            if (parkedVehicle == null)
            {
                return HttpNotFound();
            }
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            db.ParkedVehicles.Remove(parkedVehicle);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        //// GET: ParkedVehicles/Delete/5
        public ActionResult CheckOut(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            if (parkedVehicle == null)
            {
                return HttpNotFound();
            }
            var allParkedVehicles = db.ParkedVehicles;

            var RecieptVehicle = allParkedVehicles.Find(id);
            Receipt recVehicle = new Receipt();
            recVehicle.Id = RecieptVehicle.Id;
            recVehicle.RegistrationNumber = RecieptVehicle.RegistrationNumber;
            recVehicle.CheckInDate = RecieptVehicle.InDate;
            recVehicle.CheckOutDate = DateTime.Now;
            recVehicle.CostPerMinute = MinuteCost;
            recVehicle.TotalParkedTime = (int)DateTime.Now.Subtract(RecieptVehicle.InDate).TotalMinutes;
            recVehicle.TotalCost = recVehicle.TotalParkedTime * MinuteCost;

            return View(recVehicle);

        }
        // POST: ParkedVehicles/Delete/5
        [HttpPost, ActionName("CheckOut")]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOutConfirmed(int id)
        {
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);

            var parkingSpot = parkedVehicle.ParkingSpot;

            var currentParking = db.Parkings;
            var spotToUpdate = currentParking.Find(parkingSpot);

            switch (parkedVehicle.VehicleTYpe)
            {
                case VehicleType.Car:
                    spotToUpdate.WhatIsParked = 0;
                    break;
                case VehicleType.Motorcycle:
                    spotToUpdate.WhatIsParked--;
                    break;
                case VehicleType.Airplane:
                    for (int i = 0; i <= 2; i++)
                    {
                        spotToUpdate = currentParking.Find(parkingSpot + i);
                        spotToUpdate.WhatIsParked = 0;
                    }
                    break;
                default:
                    for (int i = 0; i <= 1; i++)
                    {
                        spotToUpdate = currentParking.Find(parkingSpot + i);
                        spotToUpdate.WhatIsParked = 0;
                    }
                    break;
            }

            db.ParkedVehicles.Remove(parkedVehicle);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult ListAllParking()
        {
            var currentParking = db.Parkings;
            int tempSpotNo;
            string tempSpotStatus = "";

            var currentParkings = currentParking.ToList();

            var parking = new List<ListAllParkingViewModel>();

            for (int i = 0; i < currentParking.Count(); i++)
            {
                tempSpotNo = currentParkings[i].Id;
                if (currentParkings[i].WhatIsParked == 0)
                {
                    tempSpotStatus = "Free";
                }
                else if (currentParkings[i].WhatIsParked == 1)
                {
                    tempSpotStatus = "1 Motorcycle";
                }
                else if (currentParkings[i].WhatIsParked == 2)
                {
                    tempSpotStatus = "2 Motorcycles";
                }
                else if (currentParkings[i].WhatIsParked == 3)
                {
                    tempSpotStatus = "Occupied";
                }
                var p = new ListAllParkingViewModel() { ParkingSpotNumber = tempSpotNo, ParkingSpotStatus = tempSpotStatus };
                parking.Add(p);
            }
         
            return View(parking);
        }
        
        //public PartialViewResult ParkingOverallStatus()
        //{
        //    var currentParking = db.Parkings.ToList();

        //    var emptySpots = currentParking.Where(s => s.WhatIsParked == 0);

        //    var returnString = $"@<progress value={emptySpots.Count()} max={currentParking.Count()}></progress>";

        //    return PartialView(returnString);   


        //}

       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
