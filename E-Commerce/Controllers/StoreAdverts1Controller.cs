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
    public class StoreAdverts1Controller : Controller
    {
        private Entities3 db = new Entities3();

        // GET: StoreAdverts1
        public ActionResult Index(int? brandId, int? genreId, string search, int? pageNo)
        {
            var adverts = from p in db.Adverts select p;
            if (brandId != null)
            {
                adverts = adverts.Where(b => b.BrandId == brandId);
                ViewBag.BrandId = brandId;
                ViewBag.Name = db.Brands.SingleOrDefault(x => x.BrandId == brandId).BrandName;
            }
            if (genreId != null)
            {
                adverts = adverts.Where(g => g.GenreId == genreId);
                ViewBag.GenreId = genreId;
                ViewBag.Name = db.Genres.SingleOrDefault(x => x.GenreId == genreId).GenreName;
            }
            if (!String.IsNullOrEmpty(search))
            {
                adverts = adverts.Where(p => p.AdvertName.Contains(search) ||
                                          p.Brand.BrandName.Contains(search) ||
                                          p.Genre.GenreName.Contains(search));
            }
            adverts = adverts.OrderByDescending(p => p.AdvertId);
            return View(adverts.ToPagedList(pageNo ?? 1, 21));
        }
        public ActionResult ProductDetails(int? id)
        {
            var advert = db.Adverts.Find(id);
            return View(advert);
        }
        [ChildActionOnly]
        public ActionResult BrandsMenu()
        {
            return PartialView(db.Brands.ToList());
        }
        [ChildActionOnly]
        public ActionResult GenresMenu()
        {
            return PartialView(db.Genres.ToList());
        }
    }
}