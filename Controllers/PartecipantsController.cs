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
    public class PartecipantsController : Controller
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
        public ActionResult Create(Partecipant partecipant)
        {
            if (ModelState.IsValid)
            {
                _db.Partecipants.Add(partecipant);
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = partecipant.TourId });
            }
            return View(partecipant);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _db.Partecipants.Find(id);
            return View(model);

        }

        [HttpPost]
        public ActionResult Edit(Partecipant partecipant)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(partecipant).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = partecipant.TourId });
            }
            return View(partecipant);
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
            Partecipant partecipant = _db.Partecipants.Find(id);
            if (partecipant == null)
            {
                return HttpNotFound();
            }
            return View(partecipant);
        }


        // POST: Tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Partecipant partecipant = _db.Partecipants.Find(id);
            _db.Partecipants.Remove(partecipant);
            _db.SaveChanges();
            return RedirectToAction("Index", "Partecipant", null); ;
        }
    }
}