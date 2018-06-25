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
    public class CargoTypesController : Controller
    {
        private BrunaContext db = new BrunaContext();

        // GET: CargoTypes
        public ActionResult Index()
        {
            return View(db.CargoTypes.ToList());
        }

        // GET: CargoTypes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoTypes cargoTypes = db.CargoTypes.Find(id);
            if (cargoTypes == null)
            {
                return HttpNotFound();
            }
            return View(cargoTypes);
        }

        // GET: CargoTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CargoTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description")] CargoTypes cargoTypes)
        {
            if (ModelState.IsValid)
            {
                cargoTypes.CargoID = Guid.NewGuid();

                cargoTypes.DateCreated = DateTime.Now;
                cargoTypes.DateModified = cargoTypes.DateCreated;

                cargoTypes.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                cargoTypes.ModifiedBy = cargoTypes.CreatedBy;

                db.CargoTypes.Add(cargoTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cargoTypes);
        }

        // GET: CargoTypes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoTypes cargoTypes = db.CargoTypes.Find(id);
            if (cargoTypes == null)
            {
                return HttpNotFound();
            }
            return View(cargoTypes);
        }

        // POST: CargoTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CargoID,Name,Description")] CargoTypesViewModel CargoTypesViewModel)
        {
            if (ModelState.IsValid)
            {
                CargoTypes model = db.CargoTypes.Find(CargoTypesViewModel.CargoID);

                model.Name = CargoTypesViewModel.Name;
                model.Description = CargoTypesViewModel.Description;

                model.DateModified = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(CargoTypesViewModel);
        }

        // GET: CargoTypes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoTypes cargoTypes = db.CargoTypes.Find(id);
            if (cargoTypes == null)
            {
                return HttpNotFound();
            }
            return View(cargoTypes);
        }

        // POST: CargoTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CargoTypes cargoTypes = db.CargoTypes.Find(id);
            db.CargoTypes.Remove(cargoTypes);
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
