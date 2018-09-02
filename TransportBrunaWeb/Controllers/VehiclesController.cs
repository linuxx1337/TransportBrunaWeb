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
    public class VehiclesController : Controller
    {
        private BrunaContext db = new BrunaContext();

        // GET: Vehicles
        public ActionResult Index(string searchString, string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewBag.RegPlateSortParm = sortOrder == "RegPlate" ? "RegPlate_desc" : "RegPlate";
            ViewBag.BrandSortParm = sortOrder == "Brand" ? "Brand_desc" : "Brand";
            ViewBag.DateRegSortParm = sortOrder == "DateReg" ? "DateReg_desc" : "DateReg";
            ViewBag.DateMotSortParm = sortOrder == "DateMot" ? "DateMot_desc" : "DateMot";
            //ViewBag.LabelSortParm = sortOrder == "Label" ? "Label_desc" : "Label";

            //var vehicles = db.Vehicles.Include(v => v.Company).Include(v => v.Costs);
            var vehicles = db.Vehicles.Include(v => v.Company);
            // tole je dodano za izpis tabele containers
            var allContainers = db.Containers.Include(c => c.Company).Include(c => c.ContainerTypes);
            ViewBag.allContainers = allContainers;
            //****

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
                vehicles = vehicles.Where(m => m.Name.Contains(searchString)
                || m.RegPlate.Contains(searchString)
                || m.Brand.Contains(searchString)
                || m.Note.Contains(searchString)).OrderBy(x => x.DateReg);
                /*
                allContainers = allContainers.Where(m => m.Name.Contains(searchString)
                || m.Label.Contains(searchString)
                || m.ContainerTypes.Name.Contains(searchString)
                || m.Note.Contains(searchString)).OrderBy(x => x.Label);*/
            }

            // SORT funkcija
            switch (sortOrder)
            {
                case "Name":
                    vehicles = vehicles.OrderBy(s => s.Name);
                    break;
                case "Name_desc":
                    vehicles = vehicles.OrderByDescending(s => s.Name);
                    break;
                case "RegPlate":
                    vehicles = vehicles.OrderBy(s => s.RegPlate);
                    break;
                case "RegPlate_desc":
                    vehicles = vehicles.OrderByDescending(s => s.RegPlate);
                    break;
                case "Brand":
                    vehicles = vehicles.OrderBy(s => s.Brand);
                    break;
                case "Brand_desc":
                    vehicles = vehicles.OrderByDescending(s => s.Brand);
                    break;
                case "DateReg":
                    vehicles = vehicles.OrderBy(s => s.DateReg);
                    break;
                case "DateReg_desc":
                    vehicles = vehicles.OrderByDescending(s => s.DateReg);
                    break;
                case "DateMot":
                    vehicles = vehicles.OrderBy(s => s.DateMot);
                    break;
                case "DateMot_desc":
                    vehicles = vehicles.OrderByDescending(s => s.DateMot);
                    break;
                /*case "Label":
                    allContainers = allContainers.OrderBy(s => s.Label);
                    break;
                case "Label_desc":
                    allContainers = allContainers.OrderByDescending(s => s.Label);
                    break;*/
                default:
                    vehicles = vehicles.OrderByDescending(s => s.DateReg);
                    //allContainers = allContainers.OrderBy(s => s.Label);
                    break;
            }

            //paging
            int pageSize = int.Parse(ConfigurationManager.AppSettings["pagesize"]);
            int pageNumber = (page ?? 1);
            return View(vehicles.ToPagedList(pageNumber, pageSize));
            //return View(vehicles.ToList());
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicles vehicle = db.Vehicles.Find(id);

            // tole je dodano za izpis tabele stroskov v view od vehicle details
            var vehicleCosts = db.VehicleCosts.Include(v => v.Costs).Include(v => v.Vehicles).Where(x => x.VehicleID==vehicle.VehicleID);
            ViewBag.VCosts = vehicleCosts;
            //////////
            if (vehicle == null)
            {
                return HttpNotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "FullName");
            //ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyID,Name,RegPlate,Brand,Vin,Gvw,MassCargo,Type,DateReg,DateMot,Note,Description")] Vehicles vehicles)
        {
            if (ModelState.IsValid)
            {
                vehicles.VehicleID = Guid.NewGuid();

                vehicles.DateCreated = DateTime.Now;
                vehicles.DateModified = vehicles.DateCreated;

                vehicles.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                vehicles.ModifiedBy = vehicles.CreatedBy;

                db.Vehicles.Add(vehicles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "FullName", vehicles.CompanyID);
            //ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note", vehicles.CostID);
            return View(vehicles);
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicles vehicles = db.Vehicles.Find(id);

            VehiclesViewModel view = new VehiclesViewModel();
            view.VehicleID = vehicles.VehicleID;
            view.CompanyID = vehicles.CompanyID;
            view.Name = vehicles.Name;
            view.RegPlate = vehicles.RegPlate;
            view.Brand = vehicles.Brand;
            view.Vin = vehicles.Vin;
            view.Gvw = vehicles.Gvw;
            view.MassCargo = vehicles.MassCargo;
            view.Type = vehicles.Type;
            view.DateReg = vehicles.DateReg;
            view.DateMot = vehicles.DateMot;
            view.Note = vehicles.Note;
            view.Description = vehicles.Description;

            if (vehicles == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "FullName", vehicles.CompanyID);
            //ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note", vehicles.CostID);
            return View(view);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VehicleID,CompanyID,Name,RegPlate,Brand,Vin,Gvw,MassCargo,Type,DateReg,DateMot,Note,Description")] VehiclesViewModel VehiclesViewModel)
        {
            if (ModelState.IsValid)
            {
                Vehicles model = db.Vehicles.Find(VehiclesViewModel.VehicleID);

                model.Name = VehiclesViewModel.Name;
                model.RegPlate = VehiclesViewModel.RegPlate;
                model.Brand = VehiclesViewModel.Brand;
                model.Vin = VehiclesViewModel.Vin;
                model.Gvw = VehiclesViewModel.Gvw;
                model.MassCargo = VehiclesViewModel.MassCargo;
                model.Type = VehiclesViewModel.Type;
                model.DateReg = VehiclesViewModel.DateReg;
                model.DateMot = VehiclesViewModel.DateMot;
                model.Note = VehiclesViewModel.Note;
                model.Description = VehiclesViewModel.Description;
                model.CompanyID = VehiclesViewModel.CompanyID;

                model.DateModified = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "FullName", VehiclesViewModel.CompanyID);
            //ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note", VehiclesViewModel.CostID);
            return View(VehiclesViewModel);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicles vehicles = db.Vehicles.Find(id);
            if (vehicles == null)
            {
                return HttpNotFound();
            }
            return View(vehicles);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Vehicles vehicles = db.Vehicles.Find(id);
            db.Vehicles.Remove(vehicles);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // PROSTI KESONI
        public ActionResult nonActiveContainers()
        {
            // tole je dodano za izpis tabele containers
            //var allContainers = db.Containers.Include(c => c.Company).Include(c => c.ContainerTypes);
            var allContainers = db.Containers.Include(c => c.Company).Include(c => c.ContainerTypes);

            var allTransportationLogs = db.TransportationLog.Include(c => c.ContainerID);
            allTransportationLogs = allTransportationLogs.Where(c => c.Active == false);



            ViewBag.allContainers = allContainers;

            return View(allContainers.ToList());
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
