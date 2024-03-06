using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_Commerce.Models;
using PagedList;

namespace E_Commerce.Controllers
{
    public class HomeAdverts1Controller : Controller
    {
        private Entities3 db = new Entities3();

        // GET: HomeAdverts1
        public ActionResult Index(string search, int? pageNo)
        {
            IQueryable<Advert> adverts = db.Adverts
                .OrderByDescending(a => a.DateCreated);
            if (!String.IsNullOrEmpty(search))
            {
                adverts = adverts.Where(p => p.AdvertName.Contains(search) ||
                                          p.Brand.BrandName.Contains(search) ||
                                          p.Genre.GenreName.Contains(search));
            }
            return View(adverts.ToPagedList(pageNo ?? 1, 21));
        }



        // GET: HomeAdverts1/Details/5
        public ActionResult Contact(int? id)
        {
            if (id != null)
                ViewBag.Message = "Your contact information is sent successfully";
            return View();
        }
        [ChildActionOnly]
        public ActionResult SlideShow()
        {
            return PartialView(db.SlideShows.ToList());
        }
    }
}
