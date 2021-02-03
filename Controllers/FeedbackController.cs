using IlCicerone.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace IlCicerone.Controllers
{
    public class FeedbackController : Controller
    {
        ApplicationDbContext context;
        public FeedbackController()
        {
            context = new ApplicationDbContext();
        }

        // GET: Feedback
        public ActionResult Index()
        {
            return View(context.Feedbacks.ToList());
        }

        public ActionResult Create()
        {
            FeedbackViewModel model = new FeedbackViewModel();
            Common item = new Common();
            model.Answers = item.GetAnswers();
            //model.Answers = Common.GetAnswers();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(FeedbackViewModel model)
        {
            if (ModelState.IsValid)
            {
                context.Feedbacks.Add(new Feedback() { Answer = model.Select, Comment = model.Comment, Email = model.Email, FullName = model.FullName });
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            Common item = new Common();
            model.Answers = item.GetAnswers();
            //model.Answers = Common.GetAnswers();
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = context.Feedbacks.Find(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                context.Entry(feedback).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index", new { id = feedback.ID });
            }
            return View(feedback);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = context.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }
    }
}