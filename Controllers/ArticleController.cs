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
    public class ArticleController : Controller
    {
        // GET: Article
        private ApplicationDbContext _db = new ApplicationDbContext();

        public int pageSize = 6;
        public ActionResult Index(string txtSearch, int? page, int? SelectedCategory)
        {
            var categories = _db.Categories.OrderBy(q => q.CategoryName).ToList();
            ViewBag.SelectedCategory = new SelectList(categories, "CategoryId", "CategoryName", SelectedCategory);
            int categoryId = SelectedCategory.GetValueOrDefault();

            var data = (from s in _db.Articles select s);
            if (SelectedCategory != 0)
            {
                data = data.Where(c => !SelectedCategory.HasValue || c.CategoryId == categoryId).OrderBy(d => d.ArticleId)
                .Include(d => d.Category);
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
        //Article
        public ActionResult Create()
        {
            PopulateCategoryDropDownList();
            PopulateLanguageDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleId,UserId,CategoryId,CityId,Title,Gallery,Description,ArticleImage,Created_at,Updated_at,Publication_date,LanguageId,ArticleBody,ArticleOwner")] Article _article, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    string pic = null;
                    if (file != null)
                    {
                        pic = System.IO.Path.GetFileName(file.FileName);
                        _article.ArticleImage = pic;
                        _article.Gallery = pic;
                        string path = System.IO.Path.Combine(Server.MapPath("~/Images/Article/"), _article.ArticleImage);
                        // file is uploaded
                        file.SaveAs(path);
                    }
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
                PopulateCategoryDropDownList(_article.CategoryId);
                PopulateLanguageDropDownList(_article.LanguageId);

                var title = _article.Title;
                var user = User.Identity.GetUserId();

                var count = _db.Articles.Where(s => s.Title.Contains(title)).Count();
                if (count > 0)
                {
                    ViewBag.message = "Title already exists";
                    return View();
                }



                _article.ParentArticleId = _article.ArticleId;
                _article.Created_at = DateTime.Now;
                _article.UserId = user;

                _db.Articles.Add(_article);
                _db.SaveChanges();
                return RedirectToAction("Index");

                //return Json(_article);
            }
            ViewBag.message = "Insert failed!";
            return View();
        }

        //Article
        public ActionResult Edit(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = _db.Articles.Where(s => s.ArticleId == id).FirstOrDefault();
            Article article = _db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            PopulateCategoryDropDownList(article.CategoryId);
            PopulateLanguageDropDownList(article.LanguageId);
            return View(data);
        }

        //Article
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Article _article, HttpPostedFileBase file)
        //public ActionResult Edit([Bind(Include = "ArticleId,UserId,Title,ArticleImage,Description,Publication_date,Createa_at,Updated_at,Gallery,CategoryId,CityId")] Article _article, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string pic = null;
                    if (file != null)
                    {
                        pic = System.IO.Path.GetFileName(file.FileName);
                        _article.ArticleImage = pic;
                        _article.Gallery = pic;
                        string path = System.IO.Path.Combine(Server.MapPath("~/Images/Article/"), _article.ArticleImage);
                        ViewBag.Message = "File uploaded successfully.";
                        // file is uploaded
                        file.SaveAs(path);
                    }
                    _article.ArticleImage = file != null ? pic : _article.ArticleImage;
                    _article.Gallery = file != null ? pic : _article.Gallery;
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }

                var data = _db.Articles.Find(_article.ArticleId);

                PopulateCategoryDropDownList(data.CategoryId);
                PopulateLanguageDropDownList(_article.LanguageId);

                data.Title = _article.Title;
                data.ArticleBody = _article.ArticleBody;
                data.ArticleOwner = _article.ArticleOwner;
                data.Description = _article.Description;
                data.ArticleImage = _article.ArticleImage;
                data.Publication_date = _article.Publication_date;
                data.Updated_at = DateTime.Now;
                data.UserId = User.Identity.GetUserId();
                data.Gallery = _article.Gallery;
                data.LanguageId = _article.LanguageId;
                data.CategoryId = _article.CategoryId;
                data.CityId = _article.CityId;
                _db.Entry(data).State = EntityState.Modified;
                _db.SaveChanges();
                // return Json(_article);
                return RedirectToAction("Index");
            }
            var dataEdit = _db.Articles.Where(s => s.ArticleId == _article.ArticleId).FirstOrDefault();
            return View(dataEdit);
        }

        private void PopulateCategoryDropDownList(object SelectedCategory = null)
        {
            var categoriesQuery = from d in _db.Categories
                                  orderby d.CategoryName
                                  select d;
            ViewBag.CategoryId = new SelectList(categoriesQuery, "CategoryId", "CategoryName", SelectedCategory);
        }
        private void PopulateLanguageDropDownList(object SelectedLanguage = null)
        {
            var languagesQuery = from d in _db.Languages
                                 orderby d.LanguageName
                                 select d;
            ViewBag.LanguageId = new SelectList(languagesQuery, "LanguageId", "LanguageName", SelectedLanguage);
        }


        // GET: Article/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = _db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        //Article
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _db.Articles.Where(s => s.ArticleId == id).First();
            if (product == null)
            {
                return HttpNotFound();
            }
            _db.Articles.Remove(product);
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