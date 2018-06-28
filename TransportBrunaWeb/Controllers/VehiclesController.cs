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
    public class VehiclesController : Controller
    {
        private BrunaContext db = new BrunaContext();

        // GET: Vehicles
        public ActionResult Index()
        {
            var vehicles = db.Vehicles.Include(v => v.Company).Include(v => v.Costs);
            return View(vehicles.ToList());
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicles vehicles = db.Vehicles.Find(id);
            if (vehicles == null)
            {
                return HttpNotFound();
            }
            return View(vehicles);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "FullName");
            ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyID,CostID,Name,RegPlate,Brand,Vin,Gvw,MassCargo,Type,DateReg,DateMot,Note,Description")] Vehicles vehicles)
        {
            if (ModelState.IsValid)
            {
                vehicles.VehicleID = Guid.NewGuid();

                vehicles.DateCreated = DateTime.Now;
                vehicles.DateModified = vehicles.DateCreated;

                vehicles.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                vehicles.ModifiedBy = vehicles.CreatedBy;

                db.Vehicles.Add(vehicles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "FullName", vehicles.CompanyID);
            ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note", vehicles.CostID);
            return View(vehicles);
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicles vehicles = db.Vehicles.Find(id);
            if (vehicles == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "FullName", vehicles.CompanyID);
            ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note", vehicles.CostID);
            return View(vehicles);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VehicleID,CompanyID,CostID,Name,RegPlate,Brand,Vin,Gvw,MassCargo,Type,DateReg,DateMot,Note,Description")] VehiclesViewModel VehiclesViewModel)
        {
            if (ModelState.IsValid)
            {
                Vehicles model = db.Vehicles.Find(VehiclesViewModel.VehicleID);

                model.Name = VehiclesViewModel.Name;
                model.RegPlate = VehiclesViewModel.RegPlate;
                model.Brand = VehiclesViewModel.Brand;
                model.Vin = VehiclesViewModel.Vin;
                model.Gvw = VehiclesViewModel.Gvw;
                model.MassCargo = VehiclesViewModel.MassCargo;
                model.Type = VehiclesViewModel.Type;
                model.DateReg = VehiclesViewModel.DateReg;
                model.DateMot = VehiclesViewModel.DateMot;
                model.Note = VehiclesViewModel.Note;
                model.Description = VehiclesViewModel.Description;

                model.DateModified = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "FullName", VehiclesViewModel.CompanyID);
            ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note", VehiclesViewModel.CostID);
            return View(VehiclesViewModel);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicles vehicles = db.Vehicles.Find(id);
            if (vehicles == null)
            {
                return HttpNotFound();
            }
            return View(vehicles);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Vehicles vehicles = db.Vehicles.Find(id);
            db.Vehicles.Remove(vehicles);
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
