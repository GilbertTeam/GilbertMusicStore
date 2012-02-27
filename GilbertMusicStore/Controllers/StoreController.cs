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

		//
		// GET: /Store/

		public ActionResult Index()
		{
			//List<GuitarViewModel> guitarViewModels = new List<GuitarViewModel>();

			//foreach (Guitar guitar in _entities.Guitars)
			//{
			//    int count = _entities.Guitars.Where(g => g.Brand == guitar.Brand && g.Series == guitar.Series).Count();
			//    string Description = string.Empty;
			//    //guitarViewModels.Add(new GuitarViewModel { Guitar = guitar, Count = _entities.Guitars.Where(g => g.Brand == guitar.Brand && g.Series == guitar.Series).Count(), 
			//}

			return View(_entities.Guitars);
		}

		public ActionResult Details(int id)
		{
			var guitar = _entities.Guitars.Find(id);

			if (guitar != null)
			{
				return View(guitar);
			}
			else
			{
				return HttpNotFound();
			}
		}
		#endregion
	}
}
