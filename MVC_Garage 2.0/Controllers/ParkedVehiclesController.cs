﻿using System;
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
        // GET: ListAllVehicles
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
            if (ModelState.IsValid)
            {
                parkedVehicle.InDate = DateTime.Now;
                db.ParkedVehicles.Add(parkedVehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(parkedVehicle);
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
        public ActionResult EditPost(int ? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var parkedVehicleToUpdate = db.ParkedVehicles.Find(id);
            if (TryUpdateModel(parkedVehicleToUpdate, "", new string[] {"RegistrationNumber", "NumberOfWheels", "VehicleBrand", "VehicleModel", "VehicleTYpe", "Color" }))
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
            db.ParkedVehicles.Remove(parkedVehicle);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
       
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
