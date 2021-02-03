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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IlCicerone.Controllers
{
    public class TourController : Controller
    {
        // GET: Tour
        private ApplicationDbContext _db = new ApplicationDbContext();

        //Tour

        public int pageSize = 6;
        public ActionResult Index(string txtSearch, int? page, int? SelectedCategory)
        {
            var categories = _db.Categories.OrderBy(q => q.CategoryName).ToList();
            ViewBag.SelectedCategory = new SelectList(categories, "CategoryId", "CategoryName", SelectedCategory);
            int categoryId = SelectedCategory.GetValueOrDefault();

            var data = (from s in _db.Tours select s);

            if (SelectedCategory != 0)
            {
                data = data.Where(c => !SelectedCategory.HasValue || c.CategoryId == categoryId).OrderBy(d => d.TourId)
                .Include(d => d.Category);
            }

            if (!String.IsNullOrEmpty(txtSearch))
            {
                ViewBag.txtSearch = txtSearch;
                data = data.Where(s => s.TourName.Contains(txtSearch));
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
            ViewBag.tours = data.OrderByDescending(x => x.TourId).Skip(start).Take(pageSize);
            return View();

        }
        //Tour
        public ActionResult Create()
        {
            PopulateCategoryDropDownList();
            PopulateCurrencyDropDownList();
            PopulateLanguageDropDownList();
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "TourId,UserId,TourName,TourImage,Price,ActivityLevel,Description,Publication_date,Create_date,End_date,Status,Rating,CategoryId,CityId,Language,Description,Story,ArtandCulture,EntertainmentandRecreation,Whattovisitandexcursions,Gastronomy,Howandwhentoarrive,TourOwner,LanguageId,CurrencyId")] Tour _tour, HttpPostedFileBase file)

        {
            if (ModelState.IsValid)
            {

                try
                {
                    if (file != null)
                    {
                        string path = Server.MapPath("~/Images/Tour/");
                        file.SaveAs(path + Path.GetFileName(file.FileName));
                        _tour.TourImage = Path.GetFileName(file.FileName);
                        ViewBag.Message = "File uploaded successfully.";
                        string loc = System.IO.Path.Combine(Server.MapPath("~/Images/Tour/"), _tour.TourImage);
                        // file is uploaded
                        file.SaveAs(loc);
                    }
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
                PopulateCategoryDropDownList(_tour.CategoryId);
                PopulateCurrencyDropDownList(_tour.CurrencyId);
                PopulateLanguageDropDownList(_tour.LanguageId);
                var title = _tour.TourName;
                var count = _db.Tours.Where(s => s.TourName.Contains(title)).Count();
                if (count > 0)
                {
                    ViewBag.message = "Title already exists";
                    return View();
                }

                var PublishDate = _tour.Publication_date;
                var TodayDate = DateTime.Now;

                if (_tour.IsActive == true && TodayDate < PublishDate)
                {
                    _tour.Status = "Active";
                }
                else
                {
                    _tour.Status = "Not Active";
                }
                _tour.Create_date = DateTime.Now;
                _tour.UserId = User.Identity.GetUserId();
                _db.Tours.Add(_tour);
                _db.SaveChanges();
                return RedirectToAction("Index");

                //return Json(_tour);
            }
            ViewBag.message = "Insert failed!";
            return View();
        }

        //Tour
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = _db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            var PublishDate = tour.Publication_date;
            var TodayDate = DateTime.Now;

            if (tour.IsActive == true && TodayDate < PublishDate)
            {
                tour.Status = "Active";
            }
            else
            {
                tour.Status = "Not Active";
            }
            var data = _db.Tours.Where(s => s.TourId == id).FirstOrDefault();
            PopulateCategoryDropDownList(tour.CategoryId);
            PopulateCurrencyDropDownList(tour.CurrencyId);
            PopulateLanguageDropDownList(tour.LanguageId);
            return View(data);
        }

        //Tour
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TourId,UserId,TourName,TourImage,Price,ActivityLevel,Description,Publication_date,Create_date,End_date,Status,Rating,CategoryId,CityId,Language,Description,Story,ArtandCulture,EntertainmentandRecreation,Whattovisitandexcursions,Gastronomy,Howandwhentoarrive,TourOwner,LanguageId,CurrencyId")] Tour _tour, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        string path = Server.MapPath("~/Images/Tour/");
                        file.SaveAs(path + Path.GetFileName(file.FileName));
                        _tour.TourImage = Path.GetFileName(file.FileName);
                        ViewBag.Message = "File uploaded successfully.";
                        string loc = System.IO.Path.Combine(Server.MapPath("~/Images/Tour/"), _tour.TourImage);
                        // file is uploaded
                        file.SaveAs(loc);
                    }
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
                var data = _db.Tours.Find(_tour.TourId);
                PopulateCategoryDropDownList(data.CategoryId);
                PopulateCurrencyDropDownList(data.CurrencyId);
                PopulateLanguageDropDownList(data.LanguageId);


                data.TourName = _tour.TourName;
                data.Description = _tour.Description;
                data.Story = _tour.Story;
                data.ArtandCulture = _tour.ArtandCulture;
                data.EntertainmentandRecreation = _tour.EntertainmentandRecreation;
                data.Whattovisitandexcursions = _tour.Whattovisitandexcursions;
                data.Gastronomy = _tour.Gastronomy;
                data.Howandwhentoarrive = _tour.Howandwhentoarrive;
                data.LanguageId = _tour.LanguageId;
                data.TourImage = _tour.TourImage;
                data.Status = _tour.Status;
                data.Rating = _tour.Rating;
                data.CategoryId = _tour.CategoryId;
                data.CurrencyId = _tour.CurrencyId;
                data.TourOwner = _tour.TourOwner;
                data.Price = _tour.Price;
                data.ActivityLevel = _tour.ActivityLevel;
                data.CityId = _tour.CityId;
                data.Publication_date = _tour.Publication_date;
                data.Updated_at = DateTime.Now;
                data.End_date = _tour.End_date;
                data.UserId = User.Identity.GetUserId();
                _db.Entry(data).State = EntityState.Modified;
                _db.SaveChanges();
                // return Json(_article);
                return RedirectToAction("Index");
            }
            var dataEdit = _db.Tours.Where(s => s.TourId == _tour.TourId).FirstOrDefault();
            return View(dataEdit);
        }

        private void PopulateCategoryDropDownList(object SelectedCategory = null)
        {
            var categoriesQuery = from d in _db.Categories
                                  orderby d.CategoryName
                                  select d;
            ViewBag.CategoryId = new SelectList(categoriesQuery, "CategoryId", "CategoryName", SelectedCategory);
        }

        private void PopulateCurrencyDropDownList(object SelectedCurrency = null)
        {
            var currenciesQuery = from d in _db.Currencies
                                  orderby d.CurrencyName
                                  select d;
            ViewBag.CurrencyId = new SelectList(currenciesQuery, "CurrencyId", "CurrencyName", SelectedCurrency);
        }
        private void PopulateLanguageDropDownList(object SelectedLanguage = null)
        {
            var languagesQuery = from d in _db.Languages
                                 orderby d.LanguageName
                                 select d;
            ViewBag.LanguageId = new SelectList(languagesQuery, "LanguageId", "LanguageName", SelectedLanguage);
        }


        // GET: Tour/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tour tour = _db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        //Tour
        [HttpGet]
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _db.Tours.Where(s => s.TourId == id).First();
            _db.Tours.Remove(product);
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