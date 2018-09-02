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
    public class VehicleCostsController : Controller
    {
        private BrunaContext db = new BrunaContext();

        // GET: VehicleCosts
        public ActionResult Index(string searchString, string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.CostTypesSortParm = sortOrder == "CostTypes" ? "CostTypes_desc" : "CostTypes";
            ViewBag.AmountSortParm = sortOrder == "Amount" ? "Amount_desc" : "Amount";

            var vehicleCosts = db.VehicleCosts.Include(v => v.Costs).Include(v => v.Vehicles);

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
                vehicleCosts = vehicleCosts.Where(m => m.Costs.CostTypes.Name.Contains(searchString));
            }

            // SORT funkcija
            switch (sortOrder)
            {
                case "CostTypes":
                    vehicleCosts = vehicleCosts.OrderBy(s => s.Costs.CostTypes.Name);
                    break;
                case "CostTypes_desc":
                    vehicleCosts = vehicleCosts.OrderByDescending(s => s.Costs.CostTypes.Name);
                    break;
                case "Date":
                    vehicleCosts = vehicleCosts.OrderBy(s => s.Costs.Date);
                    break;
                case "Date_desc":
                    vehicleCosts = vehicleCosts.OrderByDescending(s => s.Costs.Date);
                    break;
                case "Amount":
                    vehicleCosts = vehicleCosts.OrderBy(s => s.Costs.Amount);
                    break;
                case "Amount_desc":
                    vehicleCosts = vehicleCosts.OrderByDescending(s => s.Costs.Amount);
                    break;
                default:
                    vehicleCosts = vehicleCosts.OrderByDescending(s => s.Costs.Date);
                    break;
            }

            //paging
            int pageSize = int.Parse(ConfigurationManager.AppSettings["pagesize"]);
            int pageNumber = (page ?? 1);
            return View(vehicleCosts.ToPagedList(pageNumber, pageSize));

            //return View(vehicleCosts.ToList());
        }

        // GET: VehicleCosts/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleCosts vehicleCosts = db.VehicleCosts.Find(id);
            if (vehicleCosts == null)
            {
                return HttpNotFound();
            }
            return View(vehicleCosts);
        }

        // GET: VehicleCosts/Create
        public ActionResult Create()
        {
            ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note");
            ViewBag.VehicleID = new SelectList(db.Vehicles, "VehicleID", "Name");
            return View();
        }

        // POST: VehicleCosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VehicleID,CostID,Description")] VehicleCosts vehicleCosts)
        {
            if (ModelState.IsValid)
            {
                vehicleCosts.VehicleCostID = Guid.NewGuid();

                vehicleCosts.DateCreated = DateTime.Now;
                vehicleCosts.DateModified = vehicleCosts.DateCreated;

                vehicleCosts.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                vehicleCosts.ModifiedBy = vehicleCosts.CreatedBy;

                db.VehicleCosts.Add(vehicleCosts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note", vehicleCosts.CostID);
            ViewBag.VehicleID = new SelectList(db.Vehicles, "VehicleID", "Name", vehicleCosts.VehicleID);
            return View(vehicleCosts);
        }

        // GET: VehicleCosts/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleCosts vehicleCosts = db.VehicleCosts.Find(id);

            VehicleCostsViewModel view = new VehicleCostsViewModel();
            view.VehicleCostID = vehicleCosts.VehicleCostID;
            view.VehicleID = vehicleCosts.VehicleID;
            view.CostID = vehicleCosts.CostID;
            view.Description = vehicleCosts.Description;

            if (vehicleCosts == null)
            {
                return HttpNotFound();
            }
            ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note", vehicleCosts.CostID);
            ViewBag.VehicleID = new SelectList(db.Vehicles, "VehicleID", "Name", vehicleCosts.VehicleID);
            return View(view);
        }

        // POST: VehicleCosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VehicleCostID,VehicleID,CostID,Description")] VehicleCostsViewModel VehicleCostsViewModel)
        {
            if (ModelState.IsValid)
            {
                VehicleCosts model = db.VehicleCosts.Find(VehicleCostsViewModel.VehicleCostID);

                model.Description = VehicleCostsViewModel.Description;
                model.CostID = VehicleCostsViewModel.CostID;
                model.VehicleID = VehicleCostsViewModel.VehicleID;

                model.DateModified = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note", VehicleCostsViewModel.CostID);
            ViewBag.VehicleID = new SelectList(db.Vehicles, "VehicleID", "Name", VehicleCostsViewModel.VehicleID);
            return View(VehicleCostsViewModel);
        }

        // GET: VehicleCosts/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleCosts vehicleCosts = db.VehicleCosts.Find(id);
            if (vehicleCosts == null)
            {
                return HttpNotFound();
            }
            return View(vehicleCosts);
        }

        // POST: VehicleCosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            VehicleCosts vehicleCosts = db.VehicleCosts.Find(id);
            db.VehicleCosts.Remove(vehicleCosts);
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
