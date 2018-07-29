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
    public class PrivateCustomersController : Controller
    {
        private BrunaContext db = new BrunaContext();

        // GET: PrivateCustomers
        public ActionResult Index()
        {
            return View(db.PrivateCustomer.ToList());
        }

        // GET: PrivateCustomers/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrivateCustomer privateCustomer = db.PrivateCustomer.Find(id);
            if (privateCustomer == null)
            {
                return HttpNotFound();
            }
            return View(privateCustomer);
        }

        // GET: PrivateCustomers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrivateCustomers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FullName,Address,Phone,Email,Vat,Note,Description")] PrivateCustomer privateCustomer)
        {
            if (ModelState.IsValid)
            {
                privateCustomer.PrivateCustomerID = Guid.NewGuid();

                privateCustomer.DateCreated = DateTime.Now;
                privateCustomer.DateModified = privateCustomer.DateCreated;

                privateCustomer.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                privateCustomer.ModifiedBy = privateCustomer.CreatedBy;

                db.PrivateCustomer.Add(privateCustomer);
                db.SaveChanges();

                Customers customers = new Customers();
                customers.CustomerID = Guid.NewGuid();

                customers.DateCreated = privateCustomer.DateCreated;
                customers.DateModified = privateCustomer.DateModified;

                customers.CreatedBy = privateCustomer.CreatedBy;
                customers.ModifiedBy = privateCustomer.ModifiedBy;

                customers.PrivateCustomerID = privateCustomer.PrivateCustomerID;

                db.Customers.Add(customers);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(privateCustomer);
        }

        // GET: PrivateCustomers/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrivateCustomer privateCustomer = db.PrivateCustomer.Find(id);
            if (privateCustomer == null)
            {
                return HttpNotFound();
            }
            return View(privateCustomer);
        }

        // POST: PrivateCustomers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PrivateCustomerID,FullName,Address,Phone,Email,Vat,Note,Description")] PrivateCustomerViewModel PrivateCustomerViewModel)
        {
            if (ModelState.IsValid)
            {
                PrivateCustomer model = db.PrivateCustomer.Find(PrivateCustomerViewModel.PrivateCustomerID);

                model.FullName = PrivateCustomerViewModel.FullName;
                model.Address = PrivateCustomerViewModel.Address;
                model.Phone = PrivateCustomerViewModel.Phone;
                model.Email = PrivateCustomerViewModel.Email;
                model.Vat = PrivateCustomerViewModel.Vat;
                model.Note = PrivateCustomerViewModel.Note;
                model.Description = PrivateCustomerViewModel.Description;

                model.DateModified = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(PrivateCustomerViewModel);
        }

        // GET: PrivateCustomers/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrivateCustomer privateCustomer = db.PrivateCustomer.Find(id);
            if (privateCustomer == null)
            {
                return HttpNotFound();
            }
            return View(privateCustomer);
        }

        // POST: PrivateCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            PrivateCustomer privateCustomer = db.PrivateCustomer.Find(id);
            db.PrivateCustomer.Remove(privateCustomer);
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
