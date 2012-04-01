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
	public class StoreManagerController : Controller
	{
		#region Fields

		private MusicStoreContext _db = new MusicStoreContext();
		#endregion

		#region Methods

		private void FillGuitarViewBag()
		{
			ViewBag.BrandId = new SelectList(_db.Brands, "Id", "Name");
			ViewBag.ColorId = new SelectList(_db.Colors, "Id", "Name");
			ViewBag.ManufacturerId = new SelectList(_db.Manufacturers, "Id", "Name");
			ViewBag.BodyWoodId = new SelectList(_db.Woods, "WoodId", "Name");
			ViewBag.FretboardWoodId = new SelectList(_db.Woods, "WoodId", "Name");
			ViewBag.FingerboardWoodId = new SelectList(_db.Woods, "WoodId", "Name");
		}

		private void FillAcousticGuitarViewBag()
		{
			FillGuitarViewBag();

			ViewBag.BodyTypeId = new SelectList(_db.BodyTypes, "Id", "Name");
		}

		private void FillSemiAcousticGuitarViewBag()
		{
			FillAcousticGuitarViewBag();

			ViewBag.PreampId = new SelectList(_db.Preamps, "Id", "Name");
			ViewBag.PickupId = new SelectList(_db.Pickups, "Id", "Name");
		}

		private void FillElectricGuitarViewBag()
		{
			FillGuitarViewBag();
		}

		protected override void Dispose(bool disposing)
		{
			_db.Dispose();
			base.Dispose(disposing);
		}

		public ViewResult Index()
		{
			var guitars =
				_db.Guitars
				.Include(g => g.Brand)
				.Include(g => g.Color)
				.Include(g => g.Manufacturer);
            var list = guitars.ToList();
			return View(guitars.ToList());
		}

		public ViewResult Details(int id)
		{
			Guitar guitar = _db.Guitars.Find(id);
			return View(guitar);
		}

		public ActionResult CreateAcousticGuitar()
		{
			FillAcousticGuitarViewBag();

			return View();
		}

		[HttpPost]
		public ActionResult CreateAcousticGuitar(AcousticGuitar acousticGuitar)
		{
			if (ModelState.IsValid)
			{
				_db.Guitars.Add(acousticGuitar);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}

			FillAcousticGuitarViewBag();

			return View(acousticGuitar);
		}

		public ActionResult CreateSemiAcousticGuitar()
		{
			FillSemiAcousticGuitarViewBag();

			return View();
		}

		[HttpPost]
		public ActionResult CreateSemiAcousticGuitar(SemiAcousticGuitar semiAcousticGuitar)
		{
			if (ModelState.IsValid)
			{
				_db.Guitars.Add(semiAcousticGuitar);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}

			FillSemiAcousticGuitarViewBag();

			return View(semiAcousticGuitar);
		}

		public ActionResult CreateElectricGuitar()
		{
			FillElectricGuitarViewBag();

			return View();
		}

		[HttpPost]
		public ActionResult CreateElectricGuitar(ElectricGuitar electricGuitar)
		{
			if (ModelState.IsValid)
			{
				_db.Guitars.Add(electricGuitar);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}

			FillElectricGuitarViewBag();

			return View(electricGuitar);
		}

		public ActionResult Edit(int id)
		{
			Guitar guitar = _db.Guitars.Find(id);
			ViewBag.BrandId = new SelectList(_db.Brands, "Id", "Name", guitar.BrandId);
			ViewBag.ColorId = new SelectList(_db.Colors, "Id", "Name", guitar.ColorId);
			ViewBag.ManufacturerId = new SelectList(_db.Manufacturers, "ManufacturerId", "Name", guitar.ManufacturerId);
			return View(guitar);
		}

		[HttpPost]
		public ActionResult Edit(Guitar guitar)
		{
			if (ModelState.IsValid)
			{
				_db.Entry(guitar).State = EntityState.Modified;
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.BrandId = new SelectList(_db.Brands, "Id", "Name", guitar.BrandId);
			ViewBag.ColorId = new SelectList(_db.Colors, "Id", "Name", guitar.ColorId);
			ViewBag.ManufacturerId = new SelectList(_db.Manufacturers, "ManufacturerId", "Name", guitar.ManufacturerId);
			return View(guitar);
		}

		public ActionResult Delete(int id)
		{
			Guitar guitar = _db.Guitars.Find(id);
			return View(guitar);
		}

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			Guitar guitar = _db.Guitars.Find(id);
			_db.Guitars.Remove(guitar);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
		#endregion
	}
}