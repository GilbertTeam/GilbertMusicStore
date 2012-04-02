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

    public class ElectricGuitarController : Controller
    {
        private MusicStoreContext db = new MusicStoreContext();

        //
        // GET: /ElectricGuitar/

        public ViewResult Index()
        {
            var guitars = db.ElectricGuitars.Include(e => e.Brand).Include(e => e.Manufacturer).Include(e => e.Color);
            return View(guitars.ToList());
        }

        //
        // GET: /ElectricGuitar/Details/5

        public ViewResult Details(int id)
        {
            ElectricGuitar electricguitar = db.ElectricGuitars.Find(id);
            return View(electricguitar);
        }

        //
        // GET: /ElectricGuitar/Create

        public ActionResult Create()
        {
            FillElectricGuitarViewBag();
            return View();
        } 

        //
        // POST: /ElectricGuitar/Create

        [HttpPost]
        public ActionResult Create(ElectricGuitar electricguitar)
        {
            if (ModelState.IsValid)
            {
                db.Guitars.Add(electricguitar);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            FillElectricGuitarViewBag(electricguitar);
            return View(electricguitar);
        }
        
        //
        // GET: /ElectricGuitar/Edit/5
 
        public ActionResult Edit(int id)
        {
            ElectricGuitar electricguitar = db.ElectricGuitars.Find(id);
            FillElectricGuitarViewBag(electricguitar);
            return View(electricguitar);
        }

        //
        // POST: /ElectricGuitar/Edit/5

        [HttpPost]
        public ActionResult Edit(ElectricGuitar electricguitar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(electricguitar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            FillElectricGuitarViewBag(electricguitar);
            return View(electricguitar);
        }

        //
        // GET: /ElectricGuitar/Delete/5
 
        public ActionResult Delete(int id)
        {
            ElectricGuitar electricguitar = db.ElectricGuitars.Find(id);
            return View(electricguitar);
        }

        //
        // POST: /ElectricGuitar/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            ElectricGuitar electricguitar = db.ElectricGuitars.Find(id);
            db.Guitars.Remove(electricguitar);
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