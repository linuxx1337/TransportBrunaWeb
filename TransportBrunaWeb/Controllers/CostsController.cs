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
    public class CostsController : Controller
    {
        private BrunaContext db = new BrunaContext();

        // GET: Costs
        public ActionResult Index()
        {
            var costs = db.Costs.Include(c => c.CostTypes);
            return View(costs.ToList());
        }

        // GET: Costs/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Costs costs = db.Costs.Find(id);
            if (costs == null)
            {
                return HttpNotFound();
            }
            return View(costs);
        }

        // GET: Costs/Create
        public ActionResult Create()
        {
            ViewBag.CostTypeID = new SelectList(db.CostTypes, "CostTypeID", "Name");
            return View();
        }

        // POST: Costs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CostTypeID,Amount,Date,Note,Description")] Costs costs)
        {
            if (ModelState.IsValid)
            {
                costs.CostID = Guid.NewGuid();

                costs.DateCreated = DateTime.Now;
                costs.DateModified = costs.DateCreated;

                costs.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                costs.ModifiedBy = costs.CreatedBy;

                db.Costs.Add(costs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CostTypeID = new SelectList(db.CostTypes, "CostTypeID", "Name", costs.CostTypeID);
            return View(costs);
        }

        // GET: Costs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Costs costs = db.Costs.Find(id);
            if (costs == null)
            {
                return HttpNotFound();
            }
            ViewBag.CostTypeID = new SelectList(db.CostTypes, "CostTypeID", "Name", costs.CostTypeID);
            return View(costs);
        }

        // POST: Costs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CostID,CostTypeID,Amount,Date,Note,Description")] CostsViewModel CostsViewModel)
        {
            if (ModelState.IsValid)
            {
                Costs model = db.Costs.Find(CostsViewModel.CostID);

                model.Amount = CostsViewModel.Amount;
                model.Date = CostsViewModel.Date;
                model.Note = CostsViewModel.Note;
                model.Description = CostsViewModel.Description;

                model.DateModified = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CostTypeID = new SelectList(db.CostTypes, "CostTypeID", "Name", CostsViewModel.CostTypeID);
            return View(CostsViewModel);
        }

        // GET: Costs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Costs costs = db.Costs.Find(id);
            if (costs == null)
            {
                return HttpNotFound();
            }
            return View(costs);
        }

        // POST: Costs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Costs costs = db.Costs.Find(id);
            db.Costs.Remove(costs);
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
