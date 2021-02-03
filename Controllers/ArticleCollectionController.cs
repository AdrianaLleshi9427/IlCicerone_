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
    public class ArticleCollectionController : Controller
    {
        // GET: Article
        private ApplicationDbContext _db = new ApplicationDbContext();

        public int pageSize = 6;
        public ActionResult Index(string txtSearch, int? page, int? SelectedCategory)
        {
            //var categories = _db.Categories.OrderBy(q => q.CategoryName).ToList();
            //ViewBag.SelectedCategory = new SelectList(categories, "CategoryId", "CategoryName", SelectedCategory);
            //int categoryId = SelectedCategory.GetValueOrDefault();
            
            var data = (from s in _db.ArticleCollections select s);
            //if (SelectedCategory != 0)
            //{
            //    data = data.Where(c => !SelectedCategory.HasValue || c.CategoryId == categoryId).OrderBy(d => d.ArticleId)
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
        //Article
        public ActionResult Create()
        {          
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleCollectionId,UserId,ArticleCollectionTitle,ArticleCollectionDetails,CreatedCol_at,UpdatedCol_at,ArticleCollectionDate,ArticleColOwner")] ArticleCollection _articleCol, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string pic = null;
                    if (file != null)
                    {
                        pic = System.IO.Path.GetFileName(file.FileName);
                        _articleCol.CollectionImage = pic;
                        string path = System.IO.Path.Combine(Server.MapPath("~/Images/Article/"), _articleCol.CollectionImage);
                        // file is uploaded
                        file.SaveAs(path);
                    }
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
                var title = _articleCol.ArticleCollectionTitle;
                var user = User.Identity.GetUserId();

                var count = _db.ArticleCollections.Where(s => s.ArticleCollectionTitle.Contains(title)).Count();
                if (count > 0)
                {
                    ViewBag.message = "Title already exists";
                    return View();
                }
                
                _articleCol.CreatedCol_at = DateTime.Now;
                _articleCol.UserId = user;

                _db.ArticleCollections.Add(_articleCol);
                _db.SaveChanges();
                return RedirectToAction("Create", "Article", new { id = _articleCol.ArticleCollectionID });

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
            var data = _db.ArticleCollections.Where(s => s.ArticleCollectionID == id).FirstOrDefault();
            ArticleCollection articles = _db.ArticleCollections.Find(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        //Article
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticleCollection _articleCol, HttpPostedFileBase file)
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
                        _articleCol.CollectionImage = pic;
                        string path = System.IO.Path.Combine(Server.MapPath("~/Images/Article/"), _articleCol.CollectionImage);
                        ViewBag.Message = "File uploaded successfully.";
                        // file is uploaded
                        file.SaveAs(path);
                    }
                    _articleCol.CollectionImage = file != null ? pic : _articleCol.CollectionImage;
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }


                var data = _db.ArticleCollections.Find(_articleCol.ArticleCollectionID);

                data.ArticleCollectionTitle = _articleCol.ArticleCollectionTitle;
                data.ArticleCollectionDetails = _articleCol.ArticleCollectionDetails;
                data.ArticleCollectionDate = _articleCol.ArticleCollectionDate;
                data.UpdatedCol_at = DateTime.Now;
                data.UserId = User.Identity.GetUserId();
                data.CollectionImage = _articleCol.CollectionImage;
                data.ArticleColOwner = _articleCol.ArticleColOwner;
                _db.Entry(data).State = EntityState.Modified;
                _db.SaveChanges();
                // return Json(_article);
                return RedirectToAction("Index");
            }
            var dataEdit = _db.ArticleCollections.Where(s => s.ArticleCollectionID == _articleCol.ArticleCollectionID).FirstOrDefault();
            return View(dataEdit);
        }
        // GET: Article/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleCollection articles = _db.ArticleCollections.Find(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            return View(articles);
        }

        //Article
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _db.ArticleCollections.Where(s => s.ArticleCollectionID == id).First();
            if (product == null)
            {
                return HttpNotFound();
            }
            _db.ArticleCollections.Remove(product);
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