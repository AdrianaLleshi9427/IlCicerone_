
using IlCicerone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApplicationDashboardMVC.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext dc = new ApplicationDbContext();
        // GET: Dashboard  

        public ActionResult Index()
        {

            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                ViewBag.CountUsers = _context.Users.Count();
                ViewBag.CountArticles = _context.Articles.Count();
                ViewBag.CountArticleCollections = _context.ArticleCollections.Count();
                ViewBag.CountTours = _context.Tours.Count();
                ViewBag.CountTourCollections = _context.TourCollections.Count();
                ViewBag.CountEvents = _context.Events.Count();

                var tour = (from s in dc.Tours select s);
                ViewBag.tours = tour.Take(5);

                var article = (from s in dc.Articles select s);
                ViewBag.articles = article.Take(5);

                var user = (from s in dc.Users select s);
                ViewBag.users = user.Take(8);
            }

            return View();
        }
    }
}