using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GilbertMusicStore.Models;

namespace GilbertMusicStore.Controllers
{
	[Authorize(Roles = "Administrator")]

    public class NewsAdminController : Controller
    {
        private MusicStoreContext db = new MusicStoreContext();

        //
        // GET: /NewsAdmin/

        public ViewResult Index()
        {
            return View(db.News.OrderByDescending(n=>n.Date).ToList());
        }

        //
        // GET: /NewsAdmin/Details/5

        public ViewResult Details(int id)
        {
            News news = db.News.Find(id);
            return View(news);
        }

        //
        // GET: /NewsAdmin/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /NewsAdmin/Create

        [HttpPost]
        public ActionResult Create(News news)
        {
            if (ModelState.IsValid)
            {
				news.Date = DateTime.Now;
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(news);
        }
        
        //
        // GET: /NewsAdmin/Edit/5
 
        public ActionResult Edit(int id)
        {
            News news = db.News.Find(id);
            return View(news);
        }

        //
        // POST: /NewsAdmin/Edit/5

        [HttpPost]
        public ActionResult Edit(News news)
        {
            if (ModelState.IsValid)
            {
				news.Date = DateTime.Now;
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        //
        // GET: /NewsAdmin/Delete/5
 
        public ActionResult Delete(int id)
        {
            News news = db.News.Find(id);
            return View(news);
        }

        //
        // POST: /NewsAdmin/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            News news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}