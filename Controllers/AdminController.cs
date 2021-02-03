using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using IlCicerone.Models;
using System.Data.Entity;
using System.IO;
using Microsoft.AspNet.Identity;

namespace IlCicerone.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        //[Authorize (Roles= "Client")]
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Calendar()
        {
            return View();
        }
        private ApplicationDbContext _db = new ApplicationDbContext();
        public int pageSize = 6;
        public ActionResult Categories(string txtSearch, int? page)
        {
            var data = (from s in _db.Categories select s);
            if (!String.IsNullOrEmpty(txtSearch))
            {
                ViewBag.txtSearch = txtSearch;
                data = data.Where(s => s.CategoryName.Contains(txtSearch));
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
            ViewBag.categories = data.OrderByDescending(x => x.CategoryId).Skip(start).Take(pageSize);
            return View();

        }

        //Category
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category _category)
        {
            if (ModelState.IsValid)
            {
                var title = _category.CategoryName;
                var count = _db.Categories.Where(s => s.CategoryName.Contains(title)).Count();
                if (count > 0)
                {
                    ViewBag.message = "Title already exists";
                    return View();
                }
                _category.UserId = User.Identity.GetUserId();
                _db.Categories.Add(_category);
                _db.SaveChanges();
                return RedirectToAction("Categories");

                //return Json(_carousel);
            }
            ViewBag.message = "Insert failed!";
            return View();
        }

        //Category
        public ActionResult EditCategory(int id)
        {
            var data = _db.Categories.Where(s => s.CategoryId == id).FirstOrDefault();
            return View(data);
        }

        //Category
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory([Bind(Include = "CategoryId,CategoryName,UserId,ArticleId,TourId")] Category _category)
        {

            if (ModelState.IsValid)
            {
                var data = _db.Categories.Find(_category.CategoryId);
                data.CategoryName = _category.CategoryName;
                data.UserId = User.Identity.GetUserId();
                _db.Entry(data).State = EntityState.Modified;
                _db.SaveChanges();
                // return Json(v);
                return RedirectToAction("Categories");
            }
            var dataEdit = _db.Categories.Where(s => s.CategoryId == _category.CategoryId).FirstOrDefault();
            return View(dataEdit);
        }
        //Category
        [HttpGet]
        public ActionResult DeleteCategory(int? id)
        {
            var product = _db.Categories.Where(s => s.CategoryId == id).First();
            _db.Categories.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("Categories");
        }

        public ActionResult CarouselList(string txtSearch, int? page)
        {
            var data = (from s in _db.Carousels select s);
            if (!String.IsNullOrEmpty(txtSearch))
            {
                ViewBag.txtSearch = txtSearch;
                data = data.Where(s => s.Title.Contains(txtSearch));
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
            ViewBag.carousels = data.OrderByDescending(x => x.Id).Skip(start).Take(pageSize);
            return View();

        }

        //Carousel
        public ActionResult Carousel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Carousel(Carousel _carousel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    if (file != null)
                    {
                        string path = Server.MapPath("~/CarouselImg/");
                        file.SaveAs(path + Path.GetFileName(file.FileName));
                        _carousel.URL = Path.GetFileName(file.FileName);
                        ViewBag.Message = "File uploaded successfully.";
                        string loc = System.IO.Path.Combine(Server.MapPath("~/CarouselImg/"), _carousel.URL);
                        // file is uploaded
                        file.SaveAs(loc);
                    }
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
                var title = _carousel.Title;
                var count = _db.Carousels.Where(s => s.Title.Contains(title)).Count();
                if (count > 0)
                {
                    ViewBag.message = "Title already exists";
                    return View();
                }
                _db.Carousels.Add(_carousel);
                _db.SaveChanges();
                return RedirectToAction("CarouselList");

                //return Json(_carousel);
            }
            ViewBag.message = "Insert failed!";
            return View();
        }

        //Carousel
        public ActionResult EditCarousel (int id)
        {
            var data = _db.Carousels.Where(s => s.Id == id).FirstOrDefault();
            return View(data);
        }

        //Carousel
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCarousel([Bind(Include = "Id,Title,Description,URL,Link_destination")] Carousel _carousel, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        string path = Server.MapPath("~/CarouselImg/");
                        file.SaveAs(path + Path.GetFileName(file.FileName));
                        _carousel.URL = Path.GetFileName(file.FileName);
                        ViewBag.Message = "File uploaded successfully.";
                        string loc = System.IO.Path.Combine(Server.MapPath("~/CarouselImg/"), _carousel.URL);
                        // file is uploaded
                        file.SaveAs(loc);
                    }
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
                var data = _db.Carousels.Find(_carousel.Id);
                data.Title = _carousel.Title;
                data.Description = _carousel.Description;
                data.URL = _carousel.URL;
                data.Link_destination = _carousel.Link_destination;
                _db.Entry(data).State = EntityState.Modified;
                _db.SaveChanges();
                // return Json(v);
                return RedirectToAction("CarouselList");
            }
            var dataEdit = _db.Carousels.Where(s => s.Id == _carousel.Id).FirstOrDefault();
            return View(dataEdit);
        }
        //Carousel
        [HttpGet]
        public ActionResult DeleteCarousel(int? id)
        {
            var product = _db.Carousels.Where(s => s.Id == id).First();
            _db.Carousels.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("CarouselList");
        }
    }
}