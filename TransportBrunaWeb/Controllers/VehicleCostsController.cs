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
    public class VehicleCostsController : Controller
    {
        private BrunaContext db = new BrunaContext();

        // GET: VehicleCosts
        public ActionResult Index()
        {
            var vehicleCosts = db.VehicleCosts.Include(v => v.Costs).Include(v => v.Vehicles); // tole rabiš za izpis tabele
            return View(vehicleCosts.ToList());
        }

        // GET: VehicleCosts/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleCosts vehicleCosts = db.VehicleCosts.Find(id);
            if (vehicleCosts == null)
            {
                return HttpNotFound();
            }
            return View(vehicleCosts);
        }

        // GET: VehicleCosts/Create
        public ActionResult Create()
        {
            ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note");
            ViewBag.VehicleID = new SelectList(db.Vehicles, "VehicleID", "Name");
            return View();
        }

        // POST: VehicleCosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VehicleID,CostID,Description")] VehicleCosts vehicleCosts)
        {
            if (ModelState.IsValid)
            {
                vehicleCosts.VehicleCostID = Guid.NewGuid();

                vehicleCosts.DateCreated = DateTime.Now;
                vehicleCosts.DateModified = vehicleCosts.DateCreated;

                vehicleCosts.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                vehicleCosts.ModifiedBy = vehicleCosts.CreatedBy;

                db.VehicleCosts.Add(vehicleCosts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note", vehicleCosts.CostID);
            ViewBag.VehicleID = new SelectList(db.Vehicles, "VehicleID", "Name", vehicleCosts.VehicleID);
            return View(vehicleCosts);
        }

        // GET: VehicleCosts/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleCosts vehicleCosts = db.VehicleCosts.Find(id);
            if (vehicleCosts == null)
            {
                return HttpNotFound();
            }
            ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note", vehicleCosts.CostID);
            ViewBag.VehicleID = new SelectList(db.Vehicles, "VehicleID", "Name", vehicleCosts.VehicleID);
            return View(vehicleCosts);
        }

        // POST: VehicleCosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VehicleCostID,VehicleID,CostID,Description")] VehicleCostsViewModel VehicleCostsViewModel)
        {
            if (ModelState.IsValid)
            {
                VehicleCosts model = db.VehicleCosts.Find(VehicleCostsViewModel.VehicleCostID);

                model.Description = VehicleCostsViewModel.Description;

                model.DateModified = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note", VehicleCostsViewModel.CostID);
            ViewBag.VehicleID = new SelectList(db.Vehicles, "VehicleID", "Name", VehicleCostsViewModel.VehicleID);
            return View(VehicleCostsViewModel);
        }

        // GET: VehicleCosts/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleCosts vehicleCosts = db.VehicleCosts.Find(id);
            if (vehicleCosts == null)
            {
                return HttpNotFound();
            }
            return View(vehicleCosts);
        }

        // POST: VehicleCosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            VehicleCosts vehicleCosts = db.VehicleCosts.Find(id);
            db.VehicleCosts.Remove(vehicleCosts);
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
