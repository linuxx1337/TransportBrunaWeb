﻿using Microsoft.AspNet.Identity;
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

    public class ContainersController : Controller
    {
        private BrunaContext db = new BrunaContext();

        // GET: Containers
        public ActionResult Index()
        {
            var containers = db.Containers.Include(c => c.Company).Include(c => c.ContainerTypes);
            return View(containers.ToList());
        }

        // GET: Containers/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Containers containers = db.Containers.Find(id);
            if (containers == null)
            {
                return HttpNotFound();
            }
            return View(containers);
        }

        // GET: Containers/Create
        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "FullName");
            ViewBag.ContainerTypeID = new SelectList(db.ContainerTypes, "ContainerTypeID", "Name");
            return View();
        }

        // POST: Containers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContainerTypeID,CompanyID,Name,Label,Volume,Note,Description")] Containers containers)
        {
            if (ModelState.IsValid)
            {
                containers.ContainerID = Guid.NewGuid();

                containers.DateCreated = DateTime.Now;
                containers.DateModified = containers.DateCreated;

                containers.CreatedBy = Guid.Parse(User.Identity.GetUserId()); // tole je za userID
                containers.ModifiedBy = containers.CreatedBy;

                db.Containers.Add(containers);
                db.SaveChanges();
                return RedirectToAction("Index", "Vehicles");
            }

            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "FullName", containers.CompanyID);
            ViewBag.ContainerTypeID = new SelectList(db.ContainerTypes, "ContainerTypeID", "Name", containers.ContainerTypeID);
            return View(containers);
        }

        // GET: Containers/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Containers containers = db.Containers.Find(id);

            ContainersViewModel view = new ContainersViewModel();
            view.ContainerID = containers.ContainerID;
            view.CompanyID = containers.CompanyID;
            view.ContainerTypeID = containers.ContainerTypeID;
            view.Name = containers.Name;
            view.Label = containers.Label;
            view.Volume = containers.Volume;
            view.Note = containers.Note;
            view.Description = containers.Description;

            if (containers == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "FullName", containers.CompanyID);
            ViewBag.ContainerTypeID = new SelectList(db.ContainerTypes, "ContainerTypeID", "Name", containers.ContainerTypeID);
            return View(view);
        }

        // POST: Containers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContainerID,ContainerTypeID,CompanyID,Name,Label,Volume,Note,Description")] ContainersViewModel ContainersViewModel)
        {
            if (ModelState.IsValid)
            {
                Containers model = db.Containers.Find(ContainersViewModel.ContainerID);

                model.CompanyID = ContainersViewModel.CompanyID;
                model.ContainerTypeID = ContainersViewModel.ContainerTypeID;
                model.Name = ContainersViewModel.Name;
                model.Label = ContainersViewModel.Label;
                model.Volume = ContainersViewModel.Volume;
                model.Note = ContainersViewModel.Note;
                model.Description = ContainersViewModel.Description;

                model.DateModified = DateTime.Now;
                model.ModifiedBy = Guid.Parse(User.Identity.GetUserId());

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Vehicles");
            }
            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "FullName", ContainersViewModel.CompanyID);
            ViewBag.ContainerTypeID = new SelectList(db.ContainerTypes, "ContainerTypeID", "Name", ContainersViewModel.ContainerTypeID);
            return View(ContainersViewModel);
        }
        /*public ActionResult Edit([Bind(Include = "ContainerID,ContainerTypeID,CompanyID,Name,Label,Volume,Note,Description")] Containers containers)
        {
            if (ModelState.IsValid)
            {
                containers.DateModified = DateTime.Now;

                containers.ModifiedBy = Guid.Parse(User.Identity.GetUserId());


                db.Entry(containers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "FullName", containers.CompanyID);
            ViewBag.ContainerTypeID = new SelectList(db.ContainerTypes, "ContainerTypeID", "Name", containers.ContainerTypeID);
            return View(containers);
        }*/

        // GET: Containers/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Containers containers = db.Containers.Find(id);
            if (containers == null)
            {
                return HttpNotFound();
            }
            return View(containers);
        }

        // POST: Containers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Containers containers = db.Containers.Find(id);
            db.Containers.Remove(containers);
            db.SaveChanges();
            return RedirectToAction("Index", "Vehicles");
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
