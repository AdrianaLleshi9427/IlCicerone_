using System.Data;
using IlCicerone.Models;
using IlCicerone.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data.Entity;
using System.IO;
using PagedList;
using IlCicerone.Models.Home;
using System.Net;

namespace IlCicerone.Controllers
{
    [System.Runtime.InteropServices.Guid("9FD43E7F-47B5-4C2B-BC22-62A52ABE10DD")]
    public class HomeController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        public ActionResult Index(string txtSearch, int? id)
        {
            var carousel = (from d in _db.Carousels select d);
            ViewBag.carousels = carousel;

            var article = (from a in _db.Articles select a);
            ViewBag.articles = article;

            var data = (from s in _db.Tours select s);
            ViewBag.tours = data;

            var categorie = (from c in _db.Categories select c);
            ViewBag.categories = categorie;

            var categoryId = id.GetValueOrDefault();

            if (id != 0)
            {
                data = data.Where(c => !id.HasValue || c.CategoryId == categoryId).OrderBy(d => d.TourId)
                .Include(d => d.Category);
                ViewBag.tours = data.OrderByDescending(x => x.TourId);
            }

            if (!String.IsNullOrEmpty(txtSearch))
            {
                ViewBag.txtSearch = txtSearch;
                data = data.Where(s => s.TourName.Contains(txtSearch));
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Services()
        {
            return View();
        }
        public ActionResult FAQ()
        {
            return View();
        }
        public ActionResult Event()
        {
            return View();
        }
        public JsonResult GetEvents()
        {
            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                var events = dc.Events.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public ActionResult Maps(string selectedOpt)
        {
            var src = from s in _db.Tours
                      select s;
            ViewBag.selectedOpt = selectedOpt;
            return View();
        }
        public ActionResult Guide()
        {
            return View();
        }

        public int pageSize = 6;
        public ActionResult Tour(string txtSearch, int? page, string country, int? collectionId)
        {
            var data = (from s in _db.Tours select s);

            if (country != null)
            {
                data = data.Where(c => c.Country.CountryName == country).OrderBy(d => d.TourName)
                .Include(d => d.Country);
            }
            if (collectionId != null)
            {
                data = data.Where(c => c.TourColId == collectionId).OrderBy(d => d.TourName)
                .Include(d => d.TourCollection);
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


        public ActionResult Article(string txtSearch, int? page, int? id, int? collectionId)
        {
            var categories = (from s in _db.Categories select s).ToList();
            ViewBag.categories = categories;
            ViewBag.category = id.GetValueOrDefault(); ;
            var data = (from s in _db.Articles select s);

            if (id != 0)
            {
                data = data.Where(c => !id.HasValue || c.CategoryId == id).OrderBy(d => d.ArticleId)
                .Include(d => d.Category);
            }
            if (collectionId != null)
            {
                data = data.Where(c => c.ArticleColId == collectionId).OrderBy(d => d.Title)
                .Include(d => d.ArticleCollection);
            }

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
            ViewBag.articles = data.OrderByDescending(x => x.ArticleId).Skip(start).Take(pageSize);
            return View();

        }
        public ActionResult TourCollection(string txtSearch, int? page)
        {
            var data = (from s in _db.TourCollections select s);

            //if (country != null)
            //{
            //    data = data.Where(c => c.Country.CountryName == country).OrderBy(d => d.TourName)
            //    .Include(d => d.Country);
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

        public ActionResult ArticleCollection(string txtSearch, int? page, int? id)
        {
            //var categories = (from s in _db.Categories select s).ToList();
            //ViewBag.categories = categories;
            //ViewBag.category = id.GetValueOrDefault(); ;
            var data = (from s in _db.ArticleCollections select s);

            //if (id != 0)
            //{
            //    data = data.Where(c => !id.HasValue || c.CategoryId == id).OrderBy(d => d.ArticleId)
            //    .Include(d => d.Category);
            //}

            if (!String.IsNullOrEmpty(txtSearch))
            {
                ViewBag.txtSearch = txtSearch;
                data = data.Where(s => s.ArticleCollectionTitle.Contains(txtSearch));
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
            ViewBag.articles = data.OrderByDescending(x => x.ArticleCollectionID).Skip(start).Take(pageSize);
            return View();

        }

        public ActionResult ArticleDetails(int articleId)
        {
            return View(_unitOfWork.GetRepositoryInstance<Article>().GetFirstorDefault(articleId));
        }
        public ActionResult ArticleCollectionDetails(int articleId)
        {
            return View(_unitOfWork.GetRepositoryInstance<ArticleCollection>().GetFirstorDefault(articleId));
        }

        public ActionResult TourDetails(int tourId)
        {
            if (tourId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = _db.Tours.Find(tourId);
            if (tour == null)
            {
                return HttpNotFound();
            }
            ViewBag.TourId = tourId;

            var comments = _db.TourRatings.Where(d => d.TourId.Equals(tourId)).ToList();
            ViewBag.Comments = comments;

            var ratings = _db.TourRatings.Where(d => d.TourId.Equals(tourId)).ToList();
            if (ratings.Count() > 0)
            {
                var ratingSum = ratings.Sum(d => d.Rating);
                ViewBag.RatingSum = ratingSum;
                var ratingCount = ratings.Count();
                ViewBag.RatingCount = ratingCount;
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
            }

            return View(_unitOfWork.GetRepositoryInstance<Tour>().GetFirstorDefault(tourId));
        }
        public ActionResult TourCollectionDetails(int tourId)
        {
            if (tourId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourCollection tour = _db.TourCollections.Find(tourId);
            if (tour == null)
            {
                return HttpNotFound();
            }
            ViewBag.TourId = tourId;

            return View(_unitOfWork.GetRepositoryInstance<TourCollection>().GetFirstorDefault(tourId));
        }
        public ActionResult Checkout(int nrPartecipanti, int tourId)
        {
            ViewBag.Count = nrPartecipanti;
            if (nrPartecipanti > 0)
            {
                var tourList = _db.Tours.Where(s => s.TourId == tourId).FirstOrDefault();
                ViewBag.countPartecipant = _db.Partecipants.Where(s => s.TourId == tourId).Count();
                int lineTotal = Convert.ToInt32(nrPartecipanti * tourList.Price);
                ViewBag.tourName = tourList.TourName;
                ViewBag.tourPrice = tourList.Price;
                ViewBag.total = lineTotal;
                return View();
            }
            else
            {
                return RedirectToAction("EmptyCart");
            }
        }
        public ActionResult EmptyCart()
        {
            return View();
        }

        public JsonResult GetCountry(string term)
        {
            List<string> countryList;
            countryList = _db.Countries.Where(x => x.CountryName.StartsWith(term)).Select(y => y.CountryName).ToList();
            ViewBag.Message = term;
            return Json(countryList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCity(string term)
        {
            List<string> cityList;
            cityList = _db.Cities.Where(x => x.CityName.StartsWith(term)).Select(y => y.CityName).ToList();
            ViewBag.Message = term;
            return Json(cityList, JsonRequestBehavior.AllowGet);
        }
    }
}