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

		#region FillViewBag

		private void FillGuitarViewBag()
		{
			ViewBag.BrandId = new SelectList(_db.Brands, "Id", "Name");
			ViewBag.ColorId = new SelectList(_db.Colors, "Id", "Name");
			ViewBag.ManufacturerId = new SelectList(_db.Manufacturers, "Id", "Name");
			ViewBag.BodyWoodId = new SelectList(_db.Woods, "WoodId", "Name");
			ViewBag.FretboardWoodId = new SelectList(_db.Woods, "WoodId", "Name");
			ViewBag.FingerboardWoodId = new SelectList(_db.Woods, "WoodId", "Name");
		}

		private void FillGuitarViewBag(Guitar g)
		{
			ViewBag.BrandId = new SelectList(_db.Brands, "Id", "Name", g.BrandId);
			ViewBag.ColorId = new SelectList(_db.Colors, "Id", "Name", g.ColorId);
			ViewBag.ManufacturerId = new SelectList(_db.Manufacturers, "Id", "Name", g.ManufacturerId);
			ViewBag.BodyWoodId = new SelectList(_db.Woods, "WoodId", "Name", g.BodyWoodId);
			ViewBag.FretboardWoodId = new SelectList(_db.Woods, "WoodId", "Name", g.FretboardWoodId);
			ViewBag.FingerboardWoodId = new SelectList(_db.Woods, "WoodId", "Name", g.FingerboardWoodId);
		}

		private void FillAcousticGuitarViewBag()
		{
			FillGuitarViewBag();

			ViewBag.BodyTypeId = new SelectList(_db.BodyTypes, "Id", "Name");
		}

		private void FillAcousticGuitarViewBag(AcousticGuitar g)
		{
			FillGuitarViewBag(g);

			ViewBag.BodyTypeId = new SelectList(_db.BodyTypes, "Id", "Name", g.BodyTypeId);
		}

		private void FillSemiAcousticGuitarViewBag()
		{
			FillAcousticGuitarViewBag();

			ViewBag.PreampId = new SelectList(_db.Preamps, "Id", "Name");
			ViewBag.PickupId = new SelectList(_db.Pickups, "Id", "Name");
		}

		private void FillSemiAcousticGuitarViewBag(SemiAcousticGuitar g)
		{
			FillAcousticGuitarViewBag(g);

			ViewBag.PreampId = new SelectList(_db.Preamps, "Id", "Name", g.PreampId);
			ViewBag.PickupId = new SelectList(_db.Pickups, "Id", "Name", g.PickupId);
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
			if(guitar is AcousticGuitar)
			{
				FillAcousticGuitarViewBag(guitar as AcousticGuitar);
			}
			if (guitar is SemiAcousticGuitar)
			{
				FillSemiAcousticGuitarViewBag(guitar as SemiAcousticGuitar);
			}
			if (guitar is ElectricGuitar)
			{
				FillElectricGuitarViewBag(guitar as ElectricGuitar);
			}
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