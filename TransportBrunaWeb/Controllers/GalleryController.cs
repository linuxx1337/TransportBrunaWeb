using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportBrunaWeb.Models.DAL;
using TransportBrunaWeb.DAL;

namespace TransportBrunaWeb.Controllers
{
    public class GalleryController : Controller
    {
        // GET: GalleryController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _List()
        {
            BrunaContext db = new BrunaContext();
            var list = db.Galleries.OrderBy(x => x.OrderNo)
                        .Select(x => new ImageList
                        {
                            Id = x.Id,
                            IsActive = x.IsActive,
                            OrderNo = x.OrderNo,
                            WebImageId = x.WebImageId,
                            Title = x.Title
                        }).ToList();

            return PartialView(list);
        }
    }
}