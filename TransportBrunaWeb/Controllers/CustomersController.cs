using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public ActionResult Index(string searchString, string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.FullnameSortParm = sortOrder == "Fullname" ? "Fullname_desc" : "Fullname";
            ViewBag.AddressSortParm = sortOrder == "Address" ? "Address_desc" : "Address";
            ViewBag.VatSortParm = sortOrder == "Vat" ? "Vat_desc" : "Vat";

            var customers = db.Customers.Include(c => c.Company).Include(c => c.PrivateCustomer);

            /*
            List<Customers> test = (from customer in db.Customers
                        select new
                        {
                            CustomerID = customer.CustomerID,
                            Name = customer.CompanyID != null ? customer.Company.FullName : customer.PrivateCustomer.FullName,
                            Address = customer.CompanyID != null ? customer.Company.Address : customer.PrivateCustomer.Address,
                            Phone = customer.CompanyID != null ? customer.Company.Phone : customer.PrivateCustomer.Phone,
                            Epošta = customer.CompanyID != null ? customer.Company.Email : customer.PrivateCustomer.Email,
                            Vat = customer.CompanyID != null ? customer.Company.Vat : customer.PrivateCustomer.Vat,
                            Note = customer.CompanyID != null ? customer.Company.Note : customer.PrivateCustomer.Note
                        }).OrderBy(x => x.Name);*/

            
            // SEARCH filter
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            // SEARCH funkcija
            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(m => m.Company.FullName.Contains(searchString)
                || m.Company.Address.Contains(searchString)
                || m.PrivateCustomer.FullName.Contains(searchString)
                || m.PrivateCustomer.Address.Contains(searchString)).OrderBy(x => x.Company.FullName);
            }
            
            // SORT funkcija
            switch (sortOrder)
            {
                case "Fullname":
                    customers = customers.OrderBy(s => s.Company.FullName);
                    break;
                case "Fullname_desc":
                    customers = customers.OrderByDescending(s => s.Company.FullName);
                    break;
                case "Address":
                    customers = customers.OrderBy(s => s.Company.Address);
                    break;
                case "Address_desc":
                    customers = customers.OrderByDescending(s => s.Company.Address);
                    break;
                case "Vat":
                    customers = customers.OrderBy(s => s.Company.Vat);
                    break;
                case "Vat_desc":
                    customers = customers.OrderByDescending(s => s.Company.Vat);
                    break;
                default:
                    customers = customers.OrderBy(s => s.Company.FullName);
                    break;
            }

            //paging
            int pageSize = int.Parse(ConfigurationManager.AppSettings["pagesize"]);
            int pageNumber = (page ?? 1);
            return View(customers.ToPagedList(pageNumber, pageSize));
            //return View(test.ToPagedList(pageNumber, pageSize));
            //return View(customers.ToList());
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

            if(customers.CompanyID!=null)
            { 
            Company company = db.Company.Find(customers.CompanyID);
            db.Company.Remove(company);
            }

            if(customers.PrivateCustomerID!=null)
            {
                PrivateCustomer privateCustomer = db.PrivateCustomer.Find(customers.PrivateCustomerID);
                db.PrivateCustomer.Remove(privateCustomer);
            }

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
