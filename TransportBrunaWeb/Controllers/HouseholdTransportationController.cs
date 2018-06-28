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
    public class HouseholdTransportationController : Controller
    {
        private BrunaContext db = new BrunaContext();

        // GET: HouseholdTransportation
        public ActionResult Index()
        {
            var householdTransportation = db.HouseholdTransportation.Include(h => h.TransportationLog);
            return View(householdTransportation.ToList());
        }

        // GET: HouseholdTransportation/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseholdTransportation householdTransportation = db.HouseholdTransportation.Find(id);
            if (householdTransportation == null)
            {
                return HttpNotFound();
            }
            return View(householdTransportation);
        }

        // GET: HouseholdTransportation/Create
        public ActionResult Create()
        {
            ViewBag.TransportationLogID = new SelectList(db.TransportationLog, "TransportationLogID", "Location");
            return View();
        }

        // POST: HouseholdTransportation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransportationLogID,FirstName,LastName,Address,PostCode,City,Date,Note,Attachment,Description")] HouseholdTransportation householdTransportation)
        {
            if (ModelState.IsValid)
            {
                householdTransportation.HouseholdTransportationID = Guid.NewGuid();

                householdTransportation.DateCreated = DateTime.Now;
                householdTransportation.DateModified = householdTransportation.DateCreated;

                householdTransportation.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                householdTransportation.ModifiedBy = householdTransportation.CreatedBy;

                db.HouseholdTransportation.Add(householdTransportation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TransportationLogID = new SelectList(db.TransportationLog, "TransportationLogID", "Location", householdTransportation.TransportationLogID);
            return View(householdTransportation);
        }

        // GET: HouseholdTransportation/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseholdTransportation householdTransportation = db.HouseholdTransportation.Find(id);
            if (householdTransportation == null)
            {
                return HttpNotFound();
            }
            ViewBag.TransportationLogID = new SelectList(db.TransportationLog, "TransportationLogID", "Location", householdTransportation.TransportationLogID);
            return View(householdTransportation);
        }

        // POST: HouseholdTransportation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HouseholdTransportationID,TransportationLogID,FirstName,LastName,Address,PostCode,City,Date,Note,Attachment,Description")] HouseholdTransportationViewModel HouseholdTransportationViewModel)
        {
            if (ModelState.IsValid)
            {
                HouseholdTransportation model = db.HouseholdTransportation.Find(HouseholdTransportationViewModel.HouseholdTransportationID);

                model.FirstName = HouseholdTransportationViewModel.FirstName;
                model.LastName = HouseholdTransportationViewModel.LastName;
                model.Address = HouseholdTransportationViewModel.Address;
                model.PostCode = HouseholdTransportationViewModel.PostCode;
                model.City = HouseholdTransportationViewModel.City;
                model.Date = HouseholdTransportationViewModel.Date;
                model.Note = HouseholdTransportationViewModel.Note;
                model.Attachment = HouseholdTransportationViewModel.Attachment;
                model.Description = HouseholdTransportationViewModel.Description;

                model.DateModified = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TransportationLogID = new SelectList(db.TransportationLog, "TransportationLogID", "Location", HouseholdTransportationViewModel.TransportationLogID);
            return View(HouseholdTransportationViewModel);
        }

        // GET: HouseholdTransportation/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseholdTransportation householdTransportation = db.HouseholdTransportation.Find(id);
            if (householdTransportation == null)
            {
                return HttpNotFound();
            }
            return View(householdTransportation);
        }

        // POST: HouseholdTransportation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            HouseholdTransportation householdTransportation = db.HouseholdTransportation.Find(id);
            db.HouseholdTransportation.Remove(householdTransportation);
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
