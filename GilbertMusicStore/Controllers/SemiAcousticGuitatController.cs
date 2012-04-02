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

    public class SemiAcousticGuitarController : Controller
    {
        private MusicStoreContext db = new MusicStoreContext();

        //
        // GET: /SemiAcousticGuitar/

        public ViewResult Index()
        {
            var guitars = db.SemiAcousticGuitars.Include(s => s.Brand).Include(s => s.Manufacturer).Include(s => s.BodyType).Include(s => s.Color).Include(s => s.Preamp).Include(s => s.Pickup);
            return View(guitars.ToList());
        }

        //
        // GET: /SemiAcousticGuitat/Details/5

        public ViewResult Details(int id)
        {
            SemiAcousticGuitar semiacousticguitar = db.SemiAcousticGuitars.Find(id);
            return View(semiacousticguitar);
        }

        //
        // GET: /SemiAcousticGuitat/Create

        public ActionResult Create()
        {
            FillSemiAcousticGuitarViewBag();
            return View();
        } 

        //
        // POST: /SemiAcousticGuitat/Create

        [HttpPost]
        public ActionResult Create(SemiAcousticGuitar semiacousticguitar)
        {
            if (ModelState.IsValid)
            {
                db.Guitars.Add(semiacousticguitar);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            FillSemiAcousticGuitarViewBag(semiacousticguitar);
            return View(semiacousticguitar);
        }
        
        //
        // GET: /SemiAcousticGuitat/Edit/5
 
        public ActionResult Edit(int id)
        {
            SemiAcousticGuitar semiacousticguitar = db.SemiAcousticGuitars.Find(id);
            FillSemiAcousticGuitarViewBag(semiacousticguitar);
            return View(semiacousticguitar);
        }

        //
        // POST: /SemiAcousticGuitat/Edit/5

        [HttpPost]
        public ActionResult Edit(SemiAcousticGuitar semiacousticguitar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(semiacousticguitar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            FillSemiAcousticGuitarViewBag(semiacousticguitar);
            return View(semiacousticguitar);
        }

        //
        // GET: /SemiAcousticGuitat/Delete/5
 
        public ActionResult Delete(int id)
        {
            SemiAcousticGuitar semiacousticguitar = db.SemiAcousticGuitars.Find(id);
            return View(semiacousticguitar);
        }

        //
        // POST: /SemiAcousticGuitat/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            SemiAcousticGuitar semiacousticguitar = db.SemiAcousticGuitars.Find(id);
            db.Guitars.Remove(semiacousticguitar);
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