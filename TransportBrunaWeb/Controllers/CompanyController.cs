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
    [Authorize(Roles = "Superadmin, Superuser")]

    public class CompanyController : Controller
    {
        private BrunaContext db = new BrunaContext();

        // dodaj spodnjo za izpis userja - glej dropbox usernames.txt
        //private ApplicationDbContext dbApp = new ApplicationDbContext();

        // GET: Company
        public ActionResult Index()
        {
            return View(db.Company.ToList());
        }

        // GET: Company/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Company.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FullName,Address,Phone,Email,Vat,Note,Description")] Company company)
        {
            if (ModelState.IsValid)
            {
                company.CompanyID = Guid.NewGuid();

                company.DateCreated = DateTime.Now;
                company.DateModified = company.DateCreated;

                company.CreatedBy = Guid.Parse(User.Identity.GetUserId()); // tole je za userID
                company.ModifiedBy = company.CreatedBy;

                //company.UserName = User.Identity.GetUserName()

                db.Company.Add(company);
                db.SaveChanges();

                Customers customers = new Customers();
                customers.CustomerID = Guid.NewGuid();

                customers.DateCreated = company.DateCreated;
                customers.DateModified = company.DateModified;

                customers.CreatedBy = company.CreatedBy;
                customers.ModifiedBy = company.ModifiedBy;

                customers.CompanyID = company.CompanyID;

                db.Customers.Add(customers);
                db.SaveChanges();
                return RedirectToAction("Index", "Customers");
            }

            return View(company);
        }

        // GET: Company/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Company.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Company/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyID,FullName,Address,Phone,Email,Vat,Note,Description")] Company company)
        {
            if (ModelState.IsValid)
            {
                company.DateModified = DateTime.Now;

                company.ModifiedBy = Guid.Parse(User.Identity.GetUserId());

                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }*/
        // Uporaba ViewModel
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyID,FullName,Address,Phone,Email,Vat,Note,Description")] CompanyViewModel CompanyViewModel)
        {
            if (ModelState.IsValid)
            {
                Company model = db.Company.Find(CompanyViewModel.CompanyID);

                model.FullName = CompanyViewModel.FullName;
                model.Address = CompanyViewModel.Address;
                model.Phone = CompanyViewModel.Phone;
                model.Email = CompanyViewModel.Email;
                model.Vat = CompanyViewModel.Vat;
                model.Note = CompanyViewModel.Note;
                model.Description = CompanyViewModel.Description;

                model.DateModified = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Customers");
            }
            return View(CompanyViewModel);
        }

        // GET: Company/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Company.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Company company = db.Company.Find(id);
            db.Company.Remove(company);
            db.SaveChanges();
            return RedirectToAction("Index", "Customers");
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
