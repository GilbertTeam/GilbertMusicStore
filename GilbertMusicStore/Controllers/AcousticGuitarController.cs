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
    public class AcousticGuitarController : Controller
    {
        private MusicStoreEntities db = new MusicStoreEntities();

        //
        // GET: /AcousticGuitar/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated && User.IsInRole("admin"))
            {
                IEnumerable<AcousticGuitar> guitars = db.AcousticGuitars.Include(a => a.Brand).Include(a => a.Manufacturer).Include(a => a.Color).Include(a => a.BodyType);
                return View(guitars.ToList());
            }
            else
            {
                return new HttpStatusCodeResult(403);
            }
        }

        //
        // GET: /AcousticGuitar/Details/5

        public ViewResult Details(int id)
        {
            AcousticGuitar acousticguitar = db.AcousticGuitars.Find(id);
            return View(acousticguitar);
        }

        //
        // GET: /AcousticGuitar/Create

        public ActionResult Create()
        {
            FillAcousticGuitarViewBag();
            return View();
        } 

        //
        // POST: /AcousticGuitar/Create

        [HttpPost]
        public ActionResult Create(AcousticGuitar acousticguitar)
        {
            if (ModelState.IsValid)
            {
                acousticguitar.FingerboardWood = db.Woods.Where(w => w.WoodId == acousticguitar.FingerboardWoodId).First();
                acousticguitar.BodyWood = db.Woods.Where(w => w.WoodId == acousticguitar.BodyWoodId).First();
                acousticguitar.FretboardWood = db.Woods.Where(w => w.WoodId == acousticguitar.FretboardWoodId).First();
                db.Guitars.Add(acousticguitar);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            FillAcousticGuitarViewBag(acousticguitar);
            return View(acousticguitar);
        }
        
        //
        // GET: /AcousticGuitar/Edit/5
 
        public ActionResult Edit(int id)
        {
            AcousticGuitar acousticguitar = db.AcousticGuitars.Find(id);
            FillAcousticGuitarViewBag(acousticguitar);
            return View(acousticguitar);
        }

        //
        // POST: /AcousticGuitar/Edit/5

        [HttpPost]
        public ActionResult Edit(AcousticGuitar acousticguitar)
        {
            if (ModelState.IsValid)
            {
                acousticguitar.FingerboardWood = db.Woods.Where(w => w.WoodId == acousticguitar.FingerboardWoodId).First();
                acousticguitar.BodyWood = db.Woods.Where(w => w.WoodId == acousticguitar.BodyWoodId).First();
                acousticguitar.FretboardWood = db.Woods.Where(w => w.WoodId == acousticguitar.FretboardWoodId).First();
                db.Entry(acousticguitar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            FillAcousticGuitarViewBag(acousticguitar);
            return View(acousticguitar);
        }

        //
        // GET: /AcousticGuitar/Delete/5
 
        public ActionResult Delete(int id)
        {
            AcousticGuitar acousticguitar = db.AcousticGuitars.Find(id);
            return View(acousticguitar);
        }

        //
        // POST: /AcousticGuitar/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Guitar acousticguitar = db.Guitars.Find(id);
            db.Guitars.Remove(acousticguitar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        #region Fill ViewBag

        private void FillGuitarViewBag()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name");
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "Name");
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "Id", "Name");
            ViewBag.BodyWoodId = new SelectList(db.Woods, "WoodId", "Name");
            ViewBag.FretboardWoodId = new SelectList(db.Woods, "WoodId", "Name");
            ViewBag.FingerboardWoodId = new SelectList(db.Woods, "WoodId", "Name");
        }

        private void FillGuitarViewBag(Guitar g)
        {
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", g.BrandId);
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "Name", g.ColorId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "Id", "Name", g.ManufacturerId);
            ViewBag.BodyWoodId = new SelectList(db.Woods, "WoodId", "Name", g.BodyWoodId);
            ViewBag.FretboardWoodId = new SelectList(db.Woods, "WoodId", "Name", g.FretboardWoodId);
            ViewBag.FingerboardWoodId = new SelectList(db.Woods, "WoodId", "Name", g.FingerboardWoodId);
        }

        private void FillAcousticGuitarViewBag()
        {
            FillGuitarViewBag();

            ViewBag.BodyTypeId = new SelectList(db.BodyTypes, "Id", "Name");
        }

        private void FillAcousticGuitarViewBag(AcousticGuitar g)
        {
            FillGuitarViewBag(g);

            ViewBag.BodyTypeId = new SelectList(db.BodyTypes, "Id", "Name", g.BodyTypeId);
        }

        private void FillSemiAcousticGuitarViewBag()
        {
            FillAcousticGuitarViewBag();

            ViewBag.PreampId = new SelectList(db.Preamps, "Id", "Name");
            ViewBag.PickupId = new SelectList(db.Pickups, "Id", "Name");
        }

        private void FillSemiAcousticGuitarViewBag(SemiAcousticGuitar g)
        {
            FillAcousticGuitarViewBag(g);

            ViewBag.PreampId = new SelectList(db.Preamps, "Id", "Name", g.PreampId);
            ViewBag.PickupId = new SelectList(db.Pickups, "Id", "Name", g.PickupId);
        }

        private void FillElectricGuitarViewBag()
        {
            FillGuitarViewBag();
        }

        private void FillElectricGuitarViewBag(ElectricGuitar g)
        {
            FillGuitarViewBag(g);
        }

        #endregion
    }
}