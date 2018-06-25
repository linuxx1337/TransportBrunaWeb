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
    public class CustomersController : Controller
    {
        private BrunaContext db = new BrunaContext();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.Company).Include(c => c.PrivateCustomer);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "FullName");
            ViewBag.PrivateCustomerID = new SelectList(db.PrivateCustomer, "PrivateCustomerID", "FullName");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyID,PrivateCustomerID,Description")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                customers.CustomerID = Guid.NewGuid();

                customers.DateCreated = DateTime.Now;
                customers.DateModified = customers.DateCreated;

                customers.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                customers.ModifiedBy = customers.CreatedBy;

                db.Customers.Add(customers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "FullName", customers.CompanyID);
            ViewBag.PrivateCustomerID = new SelectList(db.PrivateCustomer, "PrivateCustomerID", "FullName", customers.PrivateCustomerID);
            return View(customers);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "FullName", customers.CompanyID);
            ViewBag.PrivateCustomerID = new SelectList(db.PrivateCustomer, "PrivateCustomerID", "FullName", customers.PrivateCustomerID);
            return View(customers);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,CompanyID,PrivateCustomerID,Description")] CutomersViewModel CutomersViewModel)
        {
            if (ModelState.IsValid)
            {
                Customers model = db.Customers.Find(CutomersViewModel.CustomerID);

                model.Description = CutomersViewModel.Description;

                model.DateModified = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "FullName", CutomersViewModel.CompanyID);
            ViewBag.PrivateCustomerID = new SelectList(db.PrivateCustomer, "PrivateCustomerID", "FullName", CutomersViewModel.PrivateCustomerID);
            return View(CutomersViewModel);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Customers customers = db.Customers.Find(id);
            db.Customers.Remove(customers);
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
