using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GilbertMusicStore.Models;
using GilbertMusicStore.ViewModels;

namespace GilbertMusicStore.Controllers
{
	public class StoreController : Controller
	{
		#region Fields

		private MusicStoreEntities _entities = new MusicStoreEntities();
		#endregion

		#region Methods

		private GuitarViewModel CreateGuitarViewModel(Guitar guitar)
		{
			int count = _entities.Guitars.Where(g => g.BrandId == guitar.BrandId && g.Series == guitar.Series).Count();
			string description = string.Format("цвет {0}, корпус {1}, гриф {2}, накладка {3}",
				guitar.Color.Name.ToLower(),
				guitar.BodyWood.Name.ToLower(),
				guitar.FretboardWood.Name.ToLower(),
				guitar.FingerboardWood.Name.ToLower());

			GuitarViewModel guitarViewModel = new GuitarViewModel { Guitar = guitar, Count = count, Description = description };

			return guitarViewModel;
		}

		public ActionResult Index()
		{
			List<GuitarViewModel> guitarViewModels = new List<GuitarViewModel>();

			foreach (Guitar guitar in _entities.Guitars
				.Include("Color")
				.Include("BodyWood")
				.Include("FretboardWood")
				.Include("FingerboardWood"))
			{
				guitarViewModels.Add(CreateGuitarViewModel(guitar));
			}

			//return View(_entities.Guitars);
			return View(guitarViewModels);
		}

		public ActionResult Details(int id = 0)
		{
			var guitar = _entities.Guitars
				.Include("Color")
				.Include("BodyWood")
				.Include("FretboardWood")
				.Include("FingerboardWood")
				.SingleOrDefault(g => g.Id == id);

			if (guitar != null)
			{
				return View(CreateGuitarViewModel(guitar));
			}
			else
			{
				return HttpNotFound();
			}
		}
		#endregion
	}
}
