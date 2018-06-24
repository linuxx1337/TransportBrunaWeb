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
    public class ContainerTypesController : Controller
    {
        private BrunaContext db = new BrunaContext();

        // GET: ContainerTypes
        public ActionResult Index()
        {
            return View(db.ContainerTypes.ToList());
        }

        // GET: ContainerTypes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContainerTypes containerTypes = db.ContainerTypes.Find(id);
            if (containerTypes == null)
            {
                return HttpNotFound();
            }
            return View(containerTypes);
        }

        // GET: ContainerTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContainerTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description")] ContainerTypes containerTypes)
        {
            if (ModelState.IsValid)
            {
                containerTypes.ContainerTypeID = Guid.NewGuid();

                containerTypes.DateCreated = DateTime.Now;
                containerTypes.DateModified = containerTypes.DateCreated;

                containerTypes.CreatedBy = Guid.Parse(User.Identity.GetUserId()); // tole je za userID
                containerTypes.ModifiedBy = containerTypes.CreatedBy;

                db.ContainerTypes.Add(containerTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(containerTypes);
        }

        // GET: ContainerTypes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContainerTypes containerTypes = db.ContainerTypes.Find(id);
            if (containerTypes == null)
            {
                return HttpNotFound();
            }
            return View(containerTypes);
        }

        // POST: ContainerTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContainerTypeID,Name,Description")] ContainerTypes containerTypes)
        {
            if (ModelState.IsValid)
            {
                containerTypes.DateModified = DateTime.Now;

                containerTypes.ModifiedBy = Guid.Parse(User.Identity.GetUserId());

                db.Entry(containerTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(containerTypes);
        }

        // GET: ContainerTypes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContainerTypes containerTypes = db.ContainerTypes.Find(id);
            if (containerTypes == null)
            {
                return HttpNotFound();
            }
            return View(containerTypes);
        }

        // POST: ContainerTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ContainerTypes containerTypes = db.ContainerTypes.Find(id);
            db.ContainerTypes.Remove(containerTypes);
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
