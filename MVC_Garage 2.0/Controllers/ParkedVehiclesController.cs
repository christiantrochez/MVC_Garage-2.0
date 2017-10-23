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

namespace MVC_Garage_2._0.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private RegisterContext db = new RegisterContext();

        // GET: ParkedVehicles
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
        public ActionResult Create([Bind(Include = "Id,RegistrationNumber,NumberOfWheels,VehicleBrand,VehicleModel,InDate,VehicleTYpe,Color")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RegistrationNumber,NumberOfWheels,VehicleBrand,VehicleModel,InDate,VehicleTYpe,Color")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parkedVehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parkedVehicle);
        }

        //// GET: ParkedVehicles/Delete/5
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
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
        //    db.ParkedVehicles.Remove(parkedVehicle);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");

        //}
        public ActionResult DeleteConfirmed(int id)
        {
            int price = 2;
            int totalCost;
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            VehicleDeleteViewModel vehVM = new VehicleDeleteViewModel();
            vehVM.Id = parkedVehicle.Id;
            
            //ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            //db.ParkedVehicles.Remove(parkedVehicle);
            //db.SaveChanges();
            //return RedirectToAction("Index");

            var vehicle = new ParkedVehicle
            {
                Id = vehVM.Id,
                InDate = vehVM.CheckInTime


            };

            var CheckOutDate = DateTime.Now;
            TimeSpan totalTime = CheckOutDate.Subtract(vehicle.InDate);
            totalCost = totalTime.Hours * price;
            vehVM.kvittoString = $"Your Vehicle with Registration Number: {vehicle.RegistrationNumber} parked since {vehicle.InDate} until {CheckOutDate} will cost: {totalCost:C}";
            //irmDelete.kvittoString
            //return RedirectToAction("ConfirmDelete",kvittoString);
            ViewBag.Text = vehVM.kvittoString;
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
        public class VehicleDeleteViewModel
        {
            public int Id { get; set; }
            public int Price { get; set; }
            public DateTime CheckInTime { get; set; }
            // public DateTime CheckOutTime { get; set; }
            public string kvittoString;
            // public int Count { get; set; }
            //public string Description { get; set; }
            //TimeSpan totalTime = DateTime.Now.Subtract(InDate);
        }
    }
}
