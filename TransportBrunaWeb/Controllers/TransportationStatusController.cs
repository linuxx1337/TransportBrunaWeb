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
    public class TransportationStatusController : Controller
    {
        private BrunaContext db = new BrunaContext();

        // GET: TransportationStatus
        public ActionResult Index()
        {
            var transportationStatus = db.TransportationStatus.Include(t => t.TransportationLog).Include(t => t.TransportationStatusTypes);
            return View(transportationStatus.ToList());
        }

        // GET: TransportationStatus/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportationStatus transportationStatus = db.TransportationStatus.Find(id);
            if (transportationStatus == null)
            {
                return HttpNotFound();
            }
            return View(transportationStatus);
        }

        // GET: TransportationStatus/Create
        public ActionResult Create()
        {
            ViewBag.TransportationLogID = new SelectList(db.TransportationLog, "TransportationLogID", "Location");
            ViewBag.TransportationTypeStatusID = new SelectList(db.TransportationStatusTypes, "TransportationTypeStatusID", "Name");
            return View();
        }

        // POST: TransportationStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransportationTypeStatusID,TransportationLogID,Date,Note,Description")] TransportationStatus transportationStatus)
        {
            if (ModelState.IsValid)
            {
                transportationStatus.TransportationStatusID = Guid.NewGuid();

                transportationStatus.DateCreated = DateTime.Now;
                transportationStatus.DateModified = transportationStatus.DateCreated;

                transportationStatus.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                transportationStatus.ModifiedBy = transportationStatus.CreatedBy;

                db.TransportationStatus.Add(transportationStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TransportationLogID = new SelectList(db.TransportationLog, "TransportationLogID", "Location", transportationStatus.TransportationLogID);
            ViewBag.TransportationTypeStatusID = new SelectList(db.TransportationStatusTypes, "TransportationTypeStatusID", "Name", transportationStatus.TransportationTypeStatusID);
            return View(transportationStatus);
        }

        // GET: TransportationStatus/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportationStatus transportationStatus = db.TransportationStatus.Find(id);
            if (transportationStatus == null)
            {
                return HttpNotFound();
            }
            ViewBag.TransportationLogID = new SelectList(db.TransportationLog, "TransportationLogID", "Location", transportationStatus.TransportationLogID);
            ViewBag.TransportationTypeStatusID = new SelectList(db.TransportationStatusTypes, "TransportationTypeStatusID", "Name", transportationStatus.TransportationTypeStatusID);
            return View(transportationStatus);
        }

        // POST: TransportationStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransportationStatusID,TransportationTypeStatusID,TransportationLogID,Date,Note,Description")] TransportationStatusViewModel TransportationStatusViewModel)
        {
            if (ModelState.IsValid)
            {
                TransportationStatus model = db.TransportationStatus.Find(TransportationStatusViewModel.TransportationStatusID);

                model.Date = TransportationStatusViewModel.Date;
                model.Note = TransportationStatusViewModel.Note;
                model.Description = TransportationStatusViewModel.Description;

                model.DateModified = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TransportationLogID = new SelectList(db.TransportationLog, "TransportationLogID", "Location", TransportationStatusViewModel.TransportationLogID);
            ViewBag.TransportationTypeStatusID = new SelectList(db.TransportationStatusTypes, "TransportationTypeStatusID", "Name", TransportationStatusViewModel.TransportationTypeStatusID);
            return View(TransportationStatusViewModel);
        }

        // GET: TransportationStatus/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportationStatus transportationStatus = db.TransportationStatus.Find(id);
            if (transportationStatus == null)
            {
                return HttpNotFound();
            }
            return View(transportationStatus);
        }

        // POST: TransportationStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TransportationStatus transportationStatus = db.TransportationStatus.Find(id);
            db.TransportationStatus.Remove(transportationStatus);
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
