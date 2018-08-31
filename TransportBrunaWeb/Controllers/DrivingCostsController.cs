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
    public class DrivingCostsController : Controller
    {
        private BrunaContext db = new BrunaContext();

        // GET: DrivingCosts
        public ActionResult Index()
        {
            var drivingCosts = db.DrivingCosts.Include(d => d.Costs).Include(d => d.TransportationLog);
            return View(drivingCosts.ToList());
        }

        // GET: DrivingCosts/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrivingCosts drivingCosts = db.DrivingCosts.Find(id);
            if (drivingCosts == null)
            {
                return HttpNotFound();
            }
            return View(drivingCosts);
        }

        // GET: DrivingCosts/Create
        public ActionResult Create()
        {
            ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note");
            ViewBag.TransportationLogID = new SelectList(db.TransportationLog, "TransportationLogID", "Location");
            return View();
        }

        // POST: DrivingCosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CostID,TransportationLogID,Description")] DrivingCosts drivingCosts)
        {
            if (ModelState.IsValid)
            {
                drivingCosts.DrivingCostID = Guid.NewGuid();

                drivingCosts.DateCreated = DateTime.Now;
                drivingCosts.DateModified = drivingCosts.DateCreated;

                drivingCosts.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                drivingCosts.ModifiedBy = drivingCosts.CreatedBy;

                db.DrivingCosts.Add(drivingCosts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note", drivingCosts.CostID);
            ViewBag.TransportationLogID = new SelectList(db.TransportationLog, "TransportationLogID", "Location", drivingCosts.TransportationLogID);
            return View(drivingCosts);
        }

        // GET: DrivingCosts/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrivingCosts drivingCosts = db.DrivingCosts.Find(id);

            DrivingCostsViewModel view = new DrivingCostsViewModel();
            view.DrivingCostID = drivingCosts.DrivingCostID;
            view.Description = drivingCosts.Description;
            view.CostID = drivingCosts.CostID;
            view.TransportationLogID = drivingCosts.TransportationLogID;

            if (drivingCosts == null)
            {
                return HttpNotFound();
            }
            ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note", drivingCosts.CostID);
            ViewBag.TransportationLogID = new SelectList(db.TransportationLog, "TransportationLogID", "Location", drivingCosts.TransportationLogID);
            return View(view);
        }

        // POST: DrivingCosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DrivingCostID,CostID,TransportationLogID,Description")] DrivingCostsViewModel DrivingCostsViewModel)
        {
            if (ModelState.IsValid)
            {
                DrivingCosts model = db.DrivingCosts.Find(DrivingCostsViewModel.DrivingCostID);

                model.Description = DrivingCostsViewModel.Description;
                model.CostID = DrivingCostsViewModel.CostID;
                model.TransportationLogID = DrivingCostsViewModel.TransportationLogID;

                model.DateModified = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note", DrivingCostsViewModel.CostID);
            ViewBag.TransportationLogID = new SelectList(db.TransportationLog, "TransportationLogID", "Location", DrivingCostsViewModel.TransportationLogID);
            return View(DrivingCostsViewModel);
        }

        // GET: DrivingCosts/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrivingCosts drivingCosts = db.DrivingCosts.Find(id);
            if (drivingCosts == null)
            {
                return HttpNotFound();
            }
            return View(drivingCosts);
        }

        // POST: DrivingCosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            DrivingCosts drivingCosts = db.DrivingCosts.Find(id);
            db.DrivingCosts.Remove(drivingCosts);
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
