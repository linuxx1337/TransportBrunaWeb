using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TransportBrunaWeb.DAL;
using TransportBrunaWeb.Models;

namespace TransportBrunaWeb.Controllers
{
    public class TransportationLogController : Controller
    {
        private BrunaContext db = new BrunaContext();

        // GET: TransportationLog
        public ActionResult Index()
        {
            var transportationLog = db.TransportationLog.Include(t => t.CargoTypes).Include(t => t.Containers).Include(t => t.Costs).Include(t => t.Customers).Include(t => t.Vehicles);
            return View(transportationLog.ToList());
        }

        // GET: TransportationLog/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportationLog transportationLog = db.TransportationLog.Find(id);
            if (transportationLog == null)
            {
                return HttpNotFound();
            }
            return View(transportationLog);
        }

        // GET: TransportationLog/Create
        public ActionResult Create()
        {
            ViewBag.CargoID = new SelectList(db.CargoTypes, "CargoID", "Name");
            ViewBag.ContainerID = new SelectList(db.Containers, "ContainerID", "Name");
            ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note");
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Description");
            ViewBag.VehicleID = new SelectList(db.Vehicles, "VehicleID", "Name");
            return View();
        }

        // POST: TransportationLog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContainerID,VehicleID,CargoID,CustomerID,CostID,Date,Location,Note,Description")] TransportationLog transportationLog, bool checkHousehold=false)
        {
            if (ModelState.IsValid)
            {
                transportationLog.TransportationLogID = Guid.NewGuid();

                transportationLog.DateCreated = DateTime.Now;
                transportationLog.DateModified = transportationLog.DateCreated;

                transportationLog.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                transportationLog.ModifiedBy = transportationLog.CreatedBy;

                db.TransportationLog.Add(transportationLog);
                db.SaveChanges();
                // CHECKBOX za HOUSEHOLD!
                if(checkHousehold == true)
                {
                    return RedirectToAction("Create", "HouseholdTransportation", new { LogID = transportationLog.TransportationLogID });
                }
                // tukaj dodaj za transporation status zapis
                //Guid latestTransporationLogID = transportationLog.TransportationLogID;
                TransportationStatus transportationStatus = new TransportationStatus();
                transportationStatus.TransportationStatusID = Guid.NewGuid();
                
                transportationStatus.DateCreated = transportationLog.DateCreated;
                transportationStatus.DateModified = transportationLog.DateModified;

                transportationStatus.CreatedBy = transportationLog.CreatedBy;
                transportationStatus.ModifiedBy = transportationLog.ModifiedBy;

                transportationStatus.Date = DateTime.Now;
               
                transportationStatus.TransportationTypeStatusID = Guid.Parse("c1f2cc4a-c96d-491f-9b7e-697f2d63645c");
                transportationStatus.TransportationLogID = transportationLog.TransportationLogID;

                db.TransportationStatus.Add(transportationStatus);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            ViewBag.CargoID = new SelectList(db.CargoTypes, "CargoID", "Name", transportationLog.CargoID);
            ViewBag.ContainerID = new SelectList(db.Containers, "ContainerID", "Name", transportationLog.ContainerID);
            ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note", transportationLog.CostID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Description", transportationLog.CustomerID);
            ViewBag.VehicleID = new SelectList(db.Vehicles, "VehicleID", "Name", transportationLog.VehicleID);
            return View(transportationLog);
        }

        // GET: TransportationLog/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportationLog transportationLog = db.TransportationLog.Find(id);

            TransportationLogViewModel view = new TransportationLogViewModel();
            view.TransportationLogID = transportationLog.TransportationLogID;
            view.ContainerID = transportationLog.ContainerID;
            view.VehicleID = transportationLog.VehicleID;
            view.CargoID = transportationLog.CargoID;
            view.CustomerID = transportationLog.CustomerID;
            view.CostID = transportationLog.CostID;
            view.Date = transportationLog.Date;
            view.Location = transportationLog.Location;
            view.Note = transportationLog.Note;
            view.Description = transportationLog.Description;

            if (transportationLog == null)
            {
                return HttpNotFound();
            }
            ViewBag.CargoID = new SelectList(db.CargoTypes, "CargoID", "Name", transportationLog.CargoID);
            ViewBag.ContainerID = new SelectList(db.Containers, "ContainerID", "Name", transportationLog.ContainerID);
            ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note", transportationLog.CostID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Description", transportationLog.CustomerID);
            ViewBag.VehicleID = new SelectList(db.Vehicles, "VehicleID", "Name", transportationLog.VehicleID);
            return View(view);
        }

        // POST: TransportationLog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransportationLogID,ContainerID,VehicleID,CargoID,CustomerID,CostID,Date,Location,Note,Description")] TransportationLogViewModel TransportationLogViewModel)
        {
            if (ModelState.IsValid)
            {
                TransportationLog model = db.TransportationLog.Find(TransportationLogViewModel.TransportationLogID);

                model.Date = TransportationLogViewModel.Date;
                model.Location = TransportationLogViewModel.Location;
                model.Note = TransportationLogViewModel.Note;
                model.Description = TransportationLogViewModel.Description;

                model.DateModified = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CargoID = new SelectList(db.CargoTypes, "CargoID", "Name", TransportationLogViewModel.CargoID);
            ViewBag.ContainerID = new SelectList(db.Containers, "ContainerID", "Name", TransportationLogViewModel.ContainerID);
            ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note", TransportationLogViewModel.CostID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Description", TransportationLogViewModel.CustomerID);
            ViewBag.VehicleID = new SelectList(db.Vehicles, "VehicleID", "Name", TransportationLogViewModel.VehicleID);
            return View(TransportationLogViewModel);
        }

        // GET: TransportationLog/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportationLog transportationLog = db.TransportationLog.Find(id);
            if (transportationLog == null)
            {
                return HttpNotFound();
            }
            return View(transportationLog);
        }

        // POST: TransportationLog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TransportationLog transportationLog = db.TransportationLog.Find(id);
            db.TransportationLog.Remove(transportationLog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // zapri voznjo
        public ActionResult CloseLog(string idLog)
        {
            //TransportationStatus transportationStatus = db.TransportationStatus.Where(t => t.TransportationLogID == Guid.Parse(idLog));
            //TransportationStatus transportationStatus = new TransportationStatus();
            //var x = db.TransportationStatus.Where(t => t.TransportationLogID == Guid.Parse(idLog));
            //TransportationStatus transportationStatus = db.TransportationStatus.Find(Guid.Parse(idLog));
            //var transportationStatus = db.TransportationStatus.Include(t => t.TransportationLog).Include(t => t.TransportationStatusTypes).Where(x => x.TransportationLogID == Guid.Parse(idLog));

            //TransportationStatus transportationStatus = db.TransportationStatus.Where(t => t.TransportationLogID == Guid.Parse("62416e0c - 2332 - 4836 - bac7 - 3889aaa32491"));
            //var x = db.TransportationStatus.Where(t => t.TransportationLogID == Guid.Parse(idLog));

            //TransportationStatus transportationStatus = new TransportationStatus();

            Guid tempID = Guid.Parse(idLog);
            TransportationStatus transportationStatus = db.TransportationStatus.Where(x => x.TransportationLogID == tempID).Single();
            
            // DODAJ ZA IZPIS ERRORJA!

            Guid idOpen = Guid.Parse("c1f2cc4a-c96d-491f-9b7e-697f2d63645c");

            if (transportationStatus.TransportationTypeStatusID == idOpen)
            {
                TransportationStatus transportationStatus1 = new TransportationStatus();
                transportationStatus1.TransportationStatusID = Guid.NewGuid();

                transportationStatus1.DateCreated = DateTime.Now;
                transportationStatus1.DateModified = transportationStatus1.DateCreated;

                transportationStatus1.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                transportationStatus1.ModifiedBy = transportationStatus1.CreatedBy;

                transportationStatus1.Date = DateTime.Now;

                transportationStatus1.TransportationTypeStatusID = Guid.Parse("fd69e724-361e-486e-ae03-05273c135c90");
                transportationStatus1.TransportationLogID = Guid.Parse(idLog);

                db.TransportationStatus.Add(transportationStatus1);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            
            /*
            if (idLog != null)
            {
                
                // tukaj dodaj za transporation status zapis
                //Guid latestTransporationLogID = idLog;
                TransportationStatus transportationStatus = new TransportationStatus();
                transportationStatus.TransportationStatusID = Guid.NewGuid();

                transportationStatus.DateCreated = DateTime.Now;
                transportationStatus.DateModified = transportationStatus.DateCreated;

                transportationStatus.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                transportationStatus.ModifiedBy = transportationStatus.CreatedBy;

                transportationStatus.Date = DateTime.Now;

                transportationStatus.TransportationTypeStatusID = Guid.Parse("fd69e724-361e-486e-ae03-05273c135c90");
                transportationStatus.TransportationLogID = Guid.Parse(idLog);

                db.TransportationStatus.Add(transportationStatus);
                db.SaveChanges();

                return RedirectToAction("Index");
            }*/
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
