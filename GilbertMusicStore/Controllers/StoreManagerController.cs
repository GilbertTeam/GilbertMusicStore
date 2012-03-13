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
	public class StoreManagerController : Controller
	{
		#region Fields

		private MusicStoreEntities _db = new MusicStoreEntities();
		#endregion

		//
		// GET: /StoreAdmin/

		public ViewResult Index()
		{
			var guitars = _db.Guitars.Include(g => g.Brand).Include(g => g.Color).Include(g => g.Manufacturer);
			return View(guitars.ToList());
		}

		//
		// GET: /StoreAdmin/Details/5

		public ViewResult Details(int id)
		{
			Guitar guitar = _db.Guitars.Find(id);
			return View(guitar);
		}

		//
		// GET: /StoreAdmin/Create

		public ActionResult Create()
		{
			ViewBag.BrandId = new SelectList(_db.Brands, "Id", "Name");
			ViewBag.ColorId = new SelectList(_db.Colors, "Id", "Name");
			ViewBag.ManufacturerId = new SelectList(_db.Manufacturers, "Id", "Name");
			ViewBag.BodyWoodId = new SelectList(_db.Woods, "WoodId", "Name");
			ViewBag.FretboardWoodId = new SelectList(_db.Woods, "WoodId", "Name");
			ViewBag.FingerboardWoodId = new SelectList(_db.Woods, "WoodId", "Name");

			return View();
		}

		//
		// POST: /StoreAdmin/Create

		[HttpPost]
		public ActionResult Create(Guitar guitar)
		{
			if (ModelState.IsValid)
			{
				_db.Guitars.Add(guitar);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.BrandId = new SelectList(_db.Brands, "Id", "Name", guitar.BrandId);
			ViewBag.ColorId = new SelectList(_db.Colors, "Id", "Name", guitar.ColorId);
			ViewBag.ManufacturerId = new SelectList(_db.Manufacturers, "ManufacturerId", "Name", guitar.ManufacturerId);
			return View(guitar);
		}

		//
		// GET: /StoreAdmin/Edit/5

		public ActionResult Edit(int id)
		{
			Guitar guitar = _db.Guitars.Find(id);
			ViewBag.BrandId = new SelectList(_db.Brands, "Id", "Name", guitar.BrandId);
			ViewBag.ColorId = new SelectList(_db.Colors, "Id", "Name", guitar.ColorId);
			ViewBag.ManufacturerId = new SelectList(_db.Manufacturers, "ManufacturerId", "Name", guitar.ManufacturerId);
			return View(guitar);
		}

		//
		// POST: /StoreAdmin/Edit/5

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

		//
		// GET: /StoreAdmin/Delete/5

		public ActionResult Delete(int id)
		{
			Guitar guitar = _db.Guitars.Find(id);
			return View(guitar);
		}

		//
		// POST: /StoreAdmin/Delete/5

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			Guitar guitar = _db.Guitars.Find(id);
			_db.Guitars.Remove(guitar);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			_db.Dispose();
			base.Dispose(disposing);
		}
	}
}