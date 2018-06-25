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
    public class CostTypesController : Controller
    {
        private BrunaContext db = new BrunaContext();

        // GET: CostTypes
        public ActionResult Index()
        {
            return View(db.CostTypes.ToList());
        }

        // GET: CostTypes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CostTypes costTypes = db.CostTypes.Find(id);
            if (costTypes == null)
            {
                return HttpNotFound();
            }
            return View(costTypes);
        }

        // GET: CostTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CostTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description")] CostTypes costTypes)
        {
            if (ModelState.IsValid)
            {
                costTypes.CostTypeID = Guid.NewGuid();

                costTypes.DateCreated = DateTime.Now;
                costTypes.DateModified = costTypes.DateCreated;

                costTypes.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                costTypes.ModifiedBy = costTypes.CreatedBy;

                db.CostTypes.Add(costTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(costTypes);
        }

        // GET: CostTypes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CostTypes costTypes = db.CostTypes.Find(id);
            if (costTypes == null)
            {
                return HttpNotFound();
            }
            return View(costTypes);
        }

        // POST: CostTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CostTypeID,Name,Description")] CostTypesViewModel CostTypesViewModel)
        {
            if (ModelState.IsValid)
            {
                CostTypes model = db.CostTypes.Find(CostTypesViewModel.CostTypeID);

                model.Name = CostTypesViewModel.Name;
                model.Description = CostTypesViewModel.Description;

                model.DateModified = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(CostTypesViewModel);
        }

        // GET: CostTypes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CostTypes costTypes = db.CostTypes.Find(id);
            if (costTypes == null)
            {
                return HttpNotFound();
            }
            return View(costTypes);
        }

        // POST: CostTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CostTypes costTypes = db.CostTypes.Find(id);
            db.CostTypes.Remove(costTypes);
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
