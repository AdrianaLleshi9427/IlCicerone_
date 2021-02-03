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
using System.Data.Entity.Infrastructure;

namespace IlCicerone.Controllers
{
    public class TourCollectionController : Controller
    {
        // GET: Tour
        private ApplicationDbContext _db = new ApplicationDbContext();

        public int pageSize = 6;
        public ActionResult Index(string txtSearch, int? page, int? SelectedCategory)
        {
            //var categories = _db.Categories.OrderBy(q => q.CategoryName).ToList();
            //ViewBag.SelectedCategory = new SelectList(categories, "CategoryId", "CategoryName", SelectedCategory);
            //int categoryId = SelectedCategory.GetValueOrDefault();
            
            var data = (from s in _db.TourCollections select s);
            //if (SelectedCategory != 0)
            //{
            //    data = data.Where(c => !SelectedCategory.HasValue || c.CategoryId == categoryId).OrderBy(d => d.TourId)
            //    .Include(d => d.Category);
            //}
            if (!String.IsNullOrEmpty(txtSearch))
            {
                ViewBag.txtSearch = txtSearch;
                data = data.Where(s => s.TourCollectionTitle.Contains(txtSearch));
            }
            if (page > 0)
            {
                page = page;
            }
            else
            {
                page = 1;
            }
            int start = (int)(page - 1) * pageSize;
            ViewBag.pageCurrent = page;
            int totalPage = data.Count();
            float totalNumsize = (totalPage / (float)pageSize);
            int numSize = (int)Math.Ceiling(totalNumsize);
            ViewBag.numSize = numSize;
            ViewBag.tours = data.OrderByDescending(x => x.TourCollectionID).Skip(start).Take(pageSize);
            return View();

        }
        //Tour
        public ActionResult Create()
        {          
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TourCollectionId,UserId,TourCollectionTitle,TourCollectionDetails,CreatedCol_at,UpdatedCol_at,TourCollectionDate,TourColOwner")] TourCollection _tourCol, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string pic = null;
                    if (file != null)
                    {
                        pic = System.IO.Path.GetFileName(file.FileName);
                        _tourCol.CollectionImage = pic;
                        string path = System.IO.Path.Combine(Server.MapPath("~/Images/Tour/"), _tourCol.CollectionImage);
                        // file is uploaded
                        file.SaveAs(path);
                    }
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
                var title = _tourCol.TourCollectionTitle;
                var user = User.Identity.GetUserId();

                var count = _db.TourCollections.Where(s => s.TourCollectionTitle.Contains(title)).Count();
                if (count > 0)
                {
                    ViewBag.message = "Title already exists";
                    return View();
                }

                _tourCol.CreatedCol_at = DateTime.Now;
                _tourCol.UserId = user;

                _db.TourCollections.Add(_tourCol);
                _db.SaveChanges();
                return RedirectToAction("Create", "Tour", new { id = _tourCol.TourCollectionID });

                //return Json(_tour);
            }
            ViewBag.message = "Insert failed!";
            return View();
        }

        //Tour
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = _db.TourCollections.Where(s => s.TourCollectionID == id).FirstOrDefault();
            TourCollection tours = _db.TourCollections.Find(id);
            if (tours == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        //Tour
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TourCollection _tourCol, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string pic = null;
                    if (file != null)
                    {
                        pic = System.IO.Path.GetFileName(file.FileName);
                        _tourCol.CollectionImage = pic;
                        string path = System.IO.Path.Combine(Server.MapPath("~/Images/Tour/"), _tourCol.CollectionImage);
                        ViewBag.Message = "File uploaded successfully.";
                        // file is uploaded
                        file.SaveAs(path);
                    }
                    _tourCol.CollectionImage = file != null ? pic : _tourCol.CollectionImage;
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }


                var data = _db.TourCollections.Find(_tourCol.TourCollectionID);

                data.TourCollectionTitle = _tourCol.TourCollectionTitle;
                data.TourCollectionDetails = _tourCol.TourCollectionDetails;
                data.TourCollectionDate = _tourCol.TourCollectionDate;
                data.EndCol_date = _tourCol.EndCol_date;
                data.UpdatedCol_at = DateTime.Now;
                data.UserId = User.Identity.GetUserId();
                data.CollectionImage = _tourCol.CollectionImage;
                data.TourColOwner = _tourCol.TourColOwner;
                _db.Entry(data).State = EntityState.Modified;
                _db.SaveChanges();
                // return Json(_tourCol);
                return RedirectToAction("Index");
            }
            var dataEdit = _db.TourCollections.Where(s => s.TourCollectionID == _tourCol.TourCollectionID).FirstOrDefault();
            return View(dataEdit);
        }
        // GET: Tour/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourCollection tours = _db.TourCollections.Find(id);
            if (tours == null)
            {
                return HttpNotFound();
            }
            return View(tours);
        }

        //Tour
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _db.TourCollections.Where(s => s.TourCollectionID == id).First();
            if (product == null)
            {
                return HttpNotFound();
            }
            _db.TourCollections.Remove(product);
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