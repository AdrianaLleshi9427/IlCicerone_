using IlCicerone.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IlCicerone.Controllers
{
    public class ReviewsController : Controller
    {
        // GET: Reviews
        private ApplicationDbContext _db = new ApplicationDbContext();
        public ActionResult Index([Bind(Prefix = "id")] int tourId)
        {
            var tour = _db.Tours.Find(tourId);
            if (tour != null)
            {
                return View(tour);
            }
            return HttpNotFound();
        }


        [HttpGet]
        public ActionResult Create(int tourId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TourReview review)
        {
            if (ModelState.IsValid)
            {
                _db.Reviews.Add(review);
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = review.TourId });
            }
            return View(review);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _db.Reviews.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(TourReview review)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(review).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = review.TourId });
            }
            return View(review);
        }




        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }


        // GET: Tours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourReview review = _db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }


        // POST: Tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TourReview review = _db.Reviews.Find(id);
            _db.Reviews.Remove(review);
            _db.SaveChanges();
            return RedirectToAction("Index", "Tours", null); ;
        }
    }
}