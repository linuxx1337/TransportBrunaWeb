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
    public class HouseholdTransportationController : Controller
    {
        private BrunaContext db = new BrunaContext();

        // GET: HouseholdTransportation
        public ActionResult Index(string searchString, string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.FirstnameSortParm = sortOrder == "Firstname" ? "Firstname_desc" : "Firstname";
            ViewBag.LastnameSortParm = sortOrder == "Lastname" ? "Lastname_desc" : "Lastname";
            ViewBag.AddressSortParm = sortOrder == "Address" ? "Address_desc" : "Address";
            ViewBag.PostCodeSortParm = sortOrder == "PostCode" ? "PostCode_desc" : "PostCode";
            ViewBag.CitySortParm = sortOrder == "City" ? "City_desc" : "City";

            var householdTransportation = db.HouseholdTransportation.Include(h => h.TransportationLog).OrderBy(x=>x.Date);

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
                householdTransportation = householdTransportation.Where(m => m.FirstName.Contains(searchString)
                || m.LastName.Contains(searchString)
                || m.Address.Contains(searchString)
                || m.PostCode.Contains(searchString)
                || m.City.Contains(searchString)
                || m.Note.Contains(searchString)).OrderBy(x=>x.Date);
            }

            // SORT funkcija
            switch (sortOrder)
            {
                case "Firstname":
                    householdTransportation = householdTransportation.OrderBy(s => s.FirstName);
                    break;
                case "Firstname_desc":
                    householdTransportation = householdTransportation.OrderByDescending(s => s.FirstName);
                    break;
                case "Date":
                    householdTransportation = householdTransportation.OrderBy(s => s.Date);
                    break;
                case "Date_desc":
                    householdTransportation = householdTransportation.OrderByDescending(s => s.Date);
                    break;
                case "Lastname":
                    householdTransportation = householdTransportation.OrderBy(s => s.LastName);
                    break;
                case "Lastname_desc":
                    householdTransportation = householdTransportation.OrderByDescending(s => s.LastName);
                    break;
                case "Address":
                    householdTransportation = householdTransportation.OrderBy(s => s.Address);
                    break;
                case "Address_desc":
                    householdTransportation = householdTransportation.OrderByDescending(s => s.Address);
                    break;
                case "PostCode":
                    householdTransportation = householdTransportation.OrderBy(s => s.PostCode);
                    break;
                case "PostCode_desc":
                    householdTransportation = householdTransportation.OrderByDescending(s => s.PostCode);
                    break;
                case "City":
                    householdTransportation = householdTransportation.OrderBy(s => s.City);
                    break;
                case "City_desc":
                    householdTransportation = householdTransportation.OrderByDescending(s => s.City);
                    break;
                default:
                    householdTransportation = householdTransportation.OrderByDescending(s => s.Date);
                    break;
            }

            //paging
            int pageSize = int.Parse(ConfigurationManager.AppSettings["pagesize"]);
            int pageNumber = (page ?? 1);
            return View(householdTransportation.ToPagedList(pageNumber, pageSize));

           // return View(householdTransportation.ToList());
        }

        // GET: HouseholdTransportation/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseholdTransportation householdTransportation = db.HouseholdTransportation.Find(id);

            // tole je dodano za izpis tabele voženj v view od household details
            var transportationLogHousehold = db.TransportationLog.Where(x => x.TransportationLogID == householdTransportation.TransportationLogID);
            ViewBag.tHousehold = transportationLogHousehold;


            if (householdTransportation == null)
            {
                return HttpNotFound();
            }
            return View(householdTransportation);
        }

        // GET: HouseholdTransportation/Create
        public ActionResult Create(string LogID)
        {
            TempData["TempModel"] = LogID;
            ViewBag.TransportationLogID = new SelectList(db.TransportationLog, "TransportationLogID", "Location");
            return View();
        }

        // POST: HouseholdTransportation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransportationLogID,FirstName,LastName,Address,PostCode,City,Date,Note,Attachment,Description")] HouseholdTransportation householdTransportation, string LogID)
        {
            if (ModelState.IsValid)
            {
                householdTransportation.HouseholdTransportationID = Guid.NewGuid();
                householdTransportation.TransportationLogID = Guid.Parse(LogID);

                householdTransportation.DateCreated = DateTime.Now;
                householdTransportation.DateModified = householdTransportation.DateCreated;

                householdTransportation.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                householdTransportation.ModifiedBy = householdTransportation.CreatedBy;

                db.HouseholdTransportation.Add(householdTransportation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

           // ViewBag.TransportationLogID = new SelectList(db.TransportationLog, "TransportationLogID", "Location", householdTransportation.TransportationLogID);
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

            HouseholdTransportationViewModel view = new HouseholdTransportationViewModel();
            view.HouseholdTransportationID = householdTransportation.HouseholdTransportationID;
            view.TransportationLogID = householdTransportation.TransportationLogID;
            view.FirstName = householdTransportation.FirstName;
            view.LastName = householdTransportation.LastName;
            view.Address = householdTransportation.Address;
            view.PostCode = householdTransportation.PostCode;
            view.City = householdTransportation.City;
            view.Date = householdTransportation.Date;
            view.Note = householdTransportation.Note;
            view.Attachment = householdTransportation.Attachment;
            view.Description = householdTransportation.Description;

            if (householdTransportation == null)
            {
                return HttpNotFound();
            }
            ViewBag.TransportationLogID = new SelectList(db.TransportationLog, "TransportationLogID", "Location", householdTransportation.TransportationLogID);
            return View(view);
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
                model.TransportationLogID = HouseholdTransportationViewModel.TransportationLogID;

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
