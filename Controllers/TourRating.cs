using IlCicerone.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Net;

namespace IlCicerone.Controllers
{
    public class TourRatingController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(_db.TourRatings.ToList());
        }

        // GET: ToursComment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourRating ToursComment = _db.TourRatings.Find(id);
            if (ToursComment == null)
            {
                return HttpNotFound();
            }
            return View(ToursComment);
        }
        // GET: ArticlesComments/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FormCollection form)
        {
            var comment = form["Comment"].ToString();
            var tourId = int.Parse(form["TourId"]);
            var rating = int.Parse(form["Rating"]);

            TourRating guideComment = new TourRating()
            {
                TourId = tourId,
                Comments = comment,
                Rating = rating,
                ThisDateTime = DateTime.Now
            };

            _db.TourRatings.Add(guideComment);
            _db.SaveChanges();

            return RedirectToAction("TourDetails", "Home", new { id = tourId });
        }

        // POST: ArticlesComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,Comments,ThisDateTime,TourId,Rating")] TourRating toursComment)
        {
            if (ModelState.IsValid)
            {
                _db.TourRatings.Add(toursComment);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(toursComment);
        }

        // GET: ArticlesComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourRating toursComment = _db.TourRatings.Find(id);
            if (toursComment == null)
            {
                return HttpNotFound();
            }
            return View(toursComment);
        }

        // POST: ArticlesComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentId,Comments,ThisDateTime,ArticleId,Rating")] TourRating toursComment)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(toursComment).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toursComment);
        }

        // GET: ArticlesComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourRating toursComment = _db.TourRatings.Find(id);
            if (toursComment == null)
            {
                return HttpNotFound();
            }
            return View(toursComment);
        }

        // POST: ArticlesComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TourRating toursComment = _db.TourRatings.Find(id);
            _db.TourRatings.Remove(toursComment);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
