using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using E_Commerce.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace E_Commerce.Models
{
    [Authorize(Roles = "Admin,User")]
    public class AdvertsController : Controller
    {
        private Entities3 db = new Entities3();

        // GET: Adverts
        public ActionResult Index(string search, int? pageNo)
        {
           
           
            var adverts = db.Adverts.Include(a => a.Brand).Include(a => a.Genre);
            if (!String.IsNullOrEmpty(search))
            {
                adverts = adverts.Where(a => a.AdvertName.Contains(search) ||
                                          a.Brand.BrandName.Contains(search) ||
                                          a.Genre.GenreName.Contains(search));
            }
            adverts = adverts.OrderBy(a => a.AdvertId);

           
            //return View(products.ToPagedList(pageNo ?? 1, 5));
            return View(adverts.ToPagedList(pageNo ?? 1,21));
        }
       

        // GET: Adverts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advert advert = db.Adverts.Find(id);
            if (advert == null)
            {
                return HttpNotFound();
            }
            return View(advert);
        }

        // GET: Adverts/Create
        public ActionResult Create()
        {
          var userId= User.Identity.GetUserName();

            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName");
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName");
            return View();
        }

        // POST: Adverts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdvertId,AdvertName,Price,ImageUrl,Description,Quantity,TempQuantity,BrandId,GenreId,DateCreated,userId")] Advert advert, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (db.Adverts.Any(n => n.AdvertName == advert.AdvertName))
                {
                    ViewBag.Create = "This Product is already exist !";
                    ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName", advert.BrandId);
                    ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName", advert.GenreId);
                    return View(advert);
                }
                if (upload != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Content/Images/Products"), upload.FileName);
                    upload.SaveAs(path);
                    advert.ImageUrl = upload.FileName;
                }
                else
                {
                    advert.ImageUrl = "No Image";
                }
                advert.TempQuantity = advert.Quantity;
                advert.DateCreated = DateTime.Today.Date;
                advert.UserId = User.Identity.GetUserName();
                db.Adverts.Add(advert);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName", advert.BrandId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName", advert.GenreId);
            return View(advert); 

        }
        // GET: Adverts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advert advert = db.Adverts.Find(id);
            if (advert == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName", advert.BrandId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName", advert.GenreId);
            return View(advert);
        }

        // POST: Adverts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdvertId,AdvertName,Price,ImageUrl,Description,Quantity,TempQuantity,BrandId,GenreId,DateCreated")] Advert advert)
        {
            if (ModelState.IsValid)
            {
                db.Entry(advert).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName", advert.BrandId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName", advert.GenreId);
            return View(advert);
        }

        // GET: Adverts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advert advert = db.Adverts.Find(id);
            if (advert == null)
            {
                return HttpNotFound();
            }
            return View(advert);
        }

        // POST: Adverts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Advert advert = db.Adverts.Find(id);
            db.Adverts.Remove(advert);
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
