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
using PagedList;


namespace TransportBrunaWeb.Controllers
{
    public class TransportationLogController : Controller
    {
        private BrunaContext db = new BrunaContext();

        // GET: TransportationLog
        public ActionResult Index(string searchString, string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.LocationSortParm = sortOrder == "Location" ? "location_desc" : "Location";
            ViewBag.CustomerSortParm = sortOrder == "Customer" ? "customer_desc" : "Customer";
            ViewBag.CargoTypeSortParm = sortOrder == "CargoType" ? "cargotype_desc" : "CargoType";
            ViewBag.VehicleSortParm = sortOrder == "Vehicle" ? "vehicle_desc" : "Vehicle";
            ViewBag.ConstainerSortParm = sortOrder == "Container" ? "container_desc" : "Container";
            ViewBag.ActiveSortParm = sortOrder == "Active" ? "active_desc" : "Active";

            //paging
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var transportationLog = db.TransportationLog.Include(t => t.CargoTypes).Include(t => t.Containers).Include(t => t.Costs).Include(t => t.Customers).Include(t => t.Vehicles);
            
            // SEARCH funkcija
            if (!String.IsNullOrEmpty(searchString))
            {
                transportationLog = transportationLog.Where(m => m.CargoTypes.Name.Contains(searchString)
                || m.Containers.Label.Contains(searchString)
                //|| m.Customers.FullName.Contains(searchString)
                || m.Vehicles.Name.Contains(searchString)
                || m.Note.Contains(searchString)
                || m.Location.Contains(searchString));
            }

            // SORT funkcija
            switch (sortOrder)
            {
                case "Location":
                    transportationLog = transportationLog.OrderBy(s => s.Location);
                    break;
                case "location_desc":
                    transportationLog = transportationLog.OrderByDescending(s => s.Location);
                    break;
                case "Date":
                    transportationLog = transportationLog.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    transportationLog = transportationLog.OrderByDescending(s => s.Date);
                    break;
                case "Customer":
                    transportationLog = transportationLog.OrderBy(s => s.CustomerID);
                    break;
                case "customer_desc":
                    transportationLog = transportationLog.OrderByDescending(s => s.CustomerID);
                    break;
                case "CargoType":
                    transportationLog = transportationLog.OrderBy(s => s.CargoTypes.Name);
                    break;
                case "cargotype_desc":
                    transportationLog = transportationLog.OrderByDescending(s => s.CargoTypes.Name);
                    break;
                case "Vehicle":
                    transportationLog = transportationLog.OrderBy(s => s.Vehicles.Name);
                    break;
                case "vehicle_desc":
                    transportationLog = transportationLog.OrderByDescending(s => s.Vehicles.Name);
                    break;
                case "Container":
                    transportationLog = transportationLog.OrderBy(s => s.Containers.Label);
                    break;
                case "container_desc":
                    transportationLog = transportationLog.OrderByDescending(s => s.Containers.Label);
                    break;
                case "Active":
                    transportationLog = transportationLog.OrderByDescending(s => s.Active);
                    break;
                case "active_desc":
                    transportationLog = transportationLog.OrderBy(s => s.Active);
                    break;
                default:
                    transportationLog = transportationLog.OrderByDescending(s => s.Date);
                    break;
            }

            //paging
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(transportationLog.ToPagedList(pageNumber, pageSize));
            //return View(transportationLog.ToList());
        }

        // GET: TransportationLog/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportationLog transportationLog = db.TransportationLog.Find(id);
            if (transportationLog == null)
            {
                return HttpNotFound();
            }
            return View(transportationLog);
        }

        // GET: TransportationLog/Create
        public ActionResult Create()
        {
            ViewBag.CargoID = new SelectList(db.CargoTypes, "CargoID", "Name");
            ViewBag.ContainerID = new SelectList(db.Containers, "ContainerID", "Label");
            ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note");

            var test = (from customer in db.Customers
                       select new
                       {
                           CustomerID = customer.CustomerID,
                           Name = customer.CompanyID != null ? customer.Company.FullName:customer.PrivateCustomer.FullName
                       }).OrderBy(x=>x.Name);


            ViewBag.CustomerID = new SelectList(test, "CustomerID", "Name");
            ViewBag.VehicleID = new SelectList(db.Vehicles, "VehicleID", "Name");
            return View();
        }

        // POST: TransportationLog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContainerID,VehicleID,CargoID,CustomerID,CostID,Date,Location,Note,Description")] TransportationLog transportationLog, bool checkHousehold=false, bool checkCosts = false)
        {
            if (ModelState.IsValid)
            {
                transportationLog.TransportationLogID = Guid.NewGuid();

                transportationLog.DateCreated = DateTime.Now;
                transportationLog.DateModified = transportationLog.DateCreated;

                transportationLog.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                transportationLog.ModifiedBy = transportationLog.CreatedBy;

                transportationLog.Active = true;

                db.TransportationLog.Add(transportationLog);
                db.SaveChanges();
                // CHECKBOX za HOUSEHOLD!
                if(checkHousehold == true)
                {
                    return RedirectToAction("Create", "HouseholdTransportation", new { LogID = transportationLog.TransportationLogID });
                }

                // CHECKBOX za COSTS!
                /*if (checkCosts == true)
                {
                    Costs costs = new Costs();
                    costs.CostID = Guid.NewGuid();

                    costs.DateCreated = transportationLog.DateCreated;
                    costs.DateModified = transportationLog.DateModified;

                    costs.CreatedBy = transportationLog.CreatedBy;
                    costs.ModifiedBy = transportationLog.ModifiedBy;

                    costs.Amount = amount;
                    costs.Date = transportationLog.Date;
                    costs.Note = note;
                    costs.CostTypeID 



                    db.Costs.Add(costs);
                    db.SaveChanges();
                }*/

                // tukaj dodaj za transporation status zapis
                //Guid latestTransporationLogID = transportationLog.TransportationLogID;
                TransportationStatus transportationStatus = new TransportationStatus();
                transportationStatus.TransportationStatusID = Guid.NewGuid();
                
                transportationStatus.DateCreated = transportationLog.DateCreated;
                transportationStatus.DateModified = transportationLog.DateModified;

                transportationStatus.CreatedBy = transportationLog.CreatedBy;
                transportationStatus.ModifiedBy = transportationLog.ModifiedBy;

                transportationStatus.Date = DateTime.Now;
               
                transportationStatus.TransportationTypeStatusID = Guid.Parse("c1f2cc4a-c96d-491f-9b7e-697f2d63645c");
                transportationStatus.TransportationLogID = transportationLog.TransportationLogID;

                db.TransportationStatus.Add(transportationStatus);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            ViewBag.CargoID = new SelectList(db.CargoTypes, "CargoID", "Name", transportationLog.CargoID);
            ViewBag.ContainerID = new SelectList(db.Containers, "ContainerID", "Name", transportationLog.ContainerID);
            ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note", transportationLog.CostID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Description", transportationLog.CustomerID);
            ViewBag.VehicleID = new SelectList(db.Vehicles, "VehicleID", "Name", transportationLog.VehicleID);
            return View(transportationLog);
        }

        // GET: TransportationLog/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportationLog transportationLog = db.TransportationLog.Find(id);

            TransportationLogViewModel view = new TransportationLogViewModel();
            view.TransportationLogID = transportationLog.TransportationLogID;
            view.ContainerID = transportationLog.ContainerID;
            view.VehicleID = transportationLog.VehicleID;
            view.CargoID = transportationLog.CargoID;
            view.CustomerID = transportationLog.CustomerID;
            view.CostID = transportationLog.CostID;
            view.Date = transportationLog.Date;
            view.Location = transportationLog.Location;
            view.Note = transportationLog.Note;
            view.Description = transportationLog.Description;

            if (transportationLog == null)
            {
                return HttpNotFound();
            }
            ViewBag.CargoID = new SelectList(db.CargoTypes, "CargoID", "Name", transportationLog.CargoID);
            ViewBag.ContainerID = new SelectList(db.Containers, "ContainerID", "Label", transportationLog.ContainerID);
            ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note", transportationLog.CostID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Description", transportationLog.CustomerID);
            ViewBag.VehicleID = new SelectList(db.Vehicles, "VehicleID", "Name", transportationLog.VehicleID);
            return View(view);
        }

        // POST: TransportationLog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransportationLogID,ContainerID,VehicleID,CargoID,CustomerID,CostID,Date,Location,Note,Description")] TransportationLogViewModel TransportationLogViewModel)
        {
            if (ModelState.IsValid)
            {
                TransportationLog model = db.TransportationLog.Find(TransportationLogViewModel.TransportationLogID);

                model.Date = TransportationLogViewModel.Date;
                model.Location = TransportationLogViewModel.Location;
                model.Note = TransportationLogViewModel.Note;
                model.Description = TransportationLogViewModel.Description;

                model.CargoID=TransportationLogViewModel.CargoID;
                model.ContainerID = TransportationLogViewModel.ContainerID;
                model.CustomerID = TransportationLogViewModel.CustomerID;
                model.VehicleID = TransportationLogViewModel.VehicleID;
                model.CostID = TransportationLogViewModel.CostID;

                model.DateModified = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CargoID = new SelectList(db.CargoTypes, "CargoID", "Name", TransportationLogViewModel.CargoID);
            ViewBag.ContainerID = new SelectList(db.Containers, "ContainerID", "Label", TransportationLogViewModel.ContainerID);
            ViewBag.CostID = new SelectList(db.Costs, "CostID", "Note", TransportationLogViewModel.CostID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Description", TransportationLogViewModel.CustomerID);
            ViewBag.VehicleID = new SelectList(db.Vehicles, "VehicleID", "Name", TransportationLogViewModel.VehicleID);
            return View(TransportationLogViewModel);
        }

        // GET: TransportationLog/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportationLog transportationLog = db.TransportationLog.Find(id);
            if (transportationLog == null)
            {
                return HttpNotFound();
            }
            return View(transportationLog);
        }

        // POST: TransportationLog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TransportationLog transportationLog = db.TransportationLog.Find(id);
            db.TransportationLog.Remove(transportationLog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Funkcija "zapri voznjo"
        public ActionResult CloseLog(string idLog)
        {
            //TransportationStatus transportationStatus = db.TransportationStatus.Where(t => t.TransportationLogID == Guid.Parse(idLog));
            //TransportationStatus transportationStatus = new TransportationStatus();
            //var x = db.TransportationStatus.Where(t => t.TransportationLogID == Guid.Parse(idLog));
            //TransportationStatus transportationStatus = db.TransportationStatus.Find(Guid.Parse(idLog));
            //var transportationStatus = db.TransportationStatus.Include(t => t.TransportationLog).Include(t => t.TransportationStatusTypes).Where(x => x.TransportationLogID == Guid.Parse(idLog));

            //TransportationStatus transportationStatus = db.TransportationStatus.Where(t => t.TransportationLogID == Guid.Parse("62416e0c - 2332 - 4836 - bac7 - 3889aaa32491"));
            //var x = db.TransportationStatus.Where(t => t.TransportationLogID == Guid.Parse(idLog));

            //TransportationStatus transportationStatus = new TransportationStatus();

            Guid tempID = Guid.Parse(idLog);
            TransportationStatus transportationStatus = db.TransportationStatus.Where(x => x.TransportationLogID == tempID).Single();
            
            Guid idOpen = Guid.Parse("c1f2cc4a-c96d-491f-9b7e-697f2d63645c");

            if (transportationStatus.TransportationTypeStatusID == idOpen)
            {
                TransportationStatus transportationStatus1 = new TransportationStatus();
                transportationStatus1.TransportationStatusID = Guid.NewGuid();

                transportationStatus1.DateCreated = DateTime.Now;
                transportationStatus1.DateModified = transportationStatus1.DateCreated;

                transportationStatus1.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                transportationStatus1.ModifiedBy = transportationStatus1.CreatedBy;

                transportationStatus1.Date = DateTime.Now;

                transportationStatus1.TransportationTypeStatusID = Guid.Parse("fd69e724-361e-486e-ae03-05273c135c90");
                transportationStatus1.TransportationLogID = Guid.Parse(idLog);

                db.TransportationStatus.Add(transportationStatus1);
                db.SaveChanges();

                // transportation log = neaktiven
                TransportationLog model = db.TransportationLog.Find(tempID);

                model.Active = false;

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
