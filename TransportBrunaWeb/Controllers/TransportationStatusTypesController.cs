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
    [Authorize(Roles = "Superadmin")]

    public class TransportationStatusTypesController : Controller
    {
        private BrunaContext db = new BrunaContext();

        // GET: TransportationStatusTypes
        public ActionResult Index()
        {
            return View(db.TransportationStatusTypes.ToList());
        }

        // GET: TransportationStatusTypes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportationStatusTypes transportationStatusTypes = db.TransportationStatusTypes.Find(id);
            if (transportationStatusTypes == null)
            {
                return HttpNotFound();
            }
            return View(transportationStatusTypes);
        }

        // GET: TransportationStatusTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransportationStatusTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description")] TransportationStatusTypes transportationStatusTypes)
        {
            if (ModelState.IsValid)
            {
                transportationStatusTypes.TransportationTypeStatusID = Guid.NewGuid();

                transportationStatusTypes.DateCreated = DateTime.Now;
                transportationStatusTypes.DateModified = transportationStatusTypes.DateCreated;

                transportationStatusTypes.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                transportationStatusTypes.ModifiedBy = transportationStatusTypes.CreatedBy;

                db.TransportationStatusTypes.Add(transportationStatusTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transportationStatusTypes);
        }

        // GET: TransportationStatusTypes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportationStatusTypes transportationStatusTypes = db.TransportationStatusTypes.Find(id);
            if (transportationStatusTypes == null)
            {
                return HttpNotFound();
            }
            return View(transportationStatusTypes);
        }

        // POST: TransportationStatusTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransportationTypeStatusID,Name,Description")] TransportationStatusTypesViewModel TransportationStatusTypesViewModel)
        {
            if (ModelState.IsValid)
            {
                TransportationStatusTypes model = db.TransportationStatusTypes.Find(TransportationStatusTypesViewModel.TransportationTypeStatusID);

                model.Name = TransportationStatusTypesViewModel.Name;
                model.Description = TransportationStatusTypesViewModel.Description;

                model.DateModified = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(TransportationStatusTypesViewModel);
        }

        // GET: TransportationStatusTypes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportationStatusTypes transportationStatusTypes = db.TransportationStatusTypes.Find(id);
            if (transportationStatusTypes == null)
            {
                return HttpNotFound();
            }
            return View(transportationStatusTypes);
        }

        // POST: TransportationStatusTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TransportationStatusTypes transportationStatusTypes = db.TransportationStatusTypes.Find(id);
            db.TransportationStatusTypes.Remove(transportationStatusTypes);
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
