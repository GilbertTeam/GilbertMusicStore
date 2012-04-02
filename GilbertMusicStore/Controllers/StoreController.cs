using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GilbertMusicStore.Models;
using GilbertMusicStore.ViewModels;
using System.Web.Helpers;
using System.Linq.Expressions;
using System.Data.Entity;
using PagedList;

namespace GilbertMusicStore.Controllers
{
	public class StoreController : Controller
	{
		#region Fields

		private readonly MusicStoreContext _db = new MusicStoreContext();

		private string _currentSortColumn;

		private SortDirection _currentCortDirection;
		#endregion

		#region Methods

		private GuitarViewModel CreateGuitarViewModel(Guitar guitar)
		{
			int count = _db.Guitars.Where(g => g.BrandId == guitar.BrandId && g.Series == guitar.Series).Count();
			string description = string.Format("цвет {0}, корпус {1}, гриф {2}, накладка {3}",
				guitar.Color.Name.ToLower(),
				guitar.BodyWood.Name.ToLower(),
				guitar.FretboardWood.Name.ToLower(),
				guitar.FingerboardWood.Name.ToLower());

			GuitarViewModel guitarViewModel = new GuitarViewModel { Guitar = guitar, Count = count, Description = description };

			return guitarViewModel;
		}

		protected override void Dispose(bool disposing)
		{
			_db.Dispose();
			base.Dispose(disposing);
		}

		public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page,
			string guitarType, int? brandId)
		{
			ViewBag.CurrentSort = sortOrder;
			ViewBag.CurrentFilter = currentFilter;
			ViewBag.CurrentType = guitarType;

			IEnumerable<Guitar> allGuitars =
				_db.Guitars
				.Include(g => g.Color)
				.Include(g => g.BodyWood)
				.Include(g => g.FretboardWood)
				.Include(g => g.FretboardWood)
				.Include(g => g.FingerboardWood);


			List<Guitar> tmpGuitars = new List<Guitar>();
			if (string.IsNullOrEmpty(guitarType))
			{
				guitarType = "";
			}

			string tmp = guitarType.ToLower();

			foreach (Guitar guitar in allGuitars)
			{
				switch (tmp)
				{
					case "акустическая":
						ElectricGuitar tmpElGuitar = guitar as ElectricGuitar;
						if (tmpElGuitar != null)
							break;
						SemiAcousticGuitar tmpSemiGuitar = guitar as SemiAcousticGuitar;
						if (tmpSemiGuitar != null)
							break;
						AcousticGuitar acGuitar = guitar as AcousticGuitar;
						if (acGuitar != null)
							tmpGuitars.Add(guitar);
						break;
					case "электрическая":
						ElectricGuitar elGuitar = guitar as ElectricGuitar;
						if (elGuitar != null)
							tmpGuitars.Add(guitar);
						break;
					case "полуакустическая":
						SemiAcousticGuitar semiGuitar = guitar as SemiAcousticGuitar;
						if (semiGuitar != null)
							tmpGuitars.Add(guitar);
						break;
					default:
						tmpGuitars.Add(guitar);
						break;
				}
			}
			

			IEnumerable<Guitar> guitars = tmpGuitars;

			if (Request.HttpMethod == "GET")
			{
				searchString = currentFilter;
			}
			else
			{
				page = 1;
			}
			ViewBag.CurrentFilter = searchString;

			if (string.IsNullOrEmpty(sortOrder))
			{
				ViewBag.CurrentSortColumn = "none";
			}
			else
			{
				string temp = sortOrder.ToLower();
				_currentSortColumn = temp;
				ViewBag.CurrentSortColumn = _currentSortColumn;

				if (string.Compare(_currentSortColumn, "name") == 0)
				{
					guitars = guitars.OrderBy(guitar => guitar.Model);
					
				}
				else if (string.Compare(_currentSortColumn, "name desc") == 0)
				{
					guitars = guitars.OrderByDescending(guitar => guitar.Model);
				}
				else if (string.Compare(_currentSortColumn, "count") == 0)
				{
				}
				else if (string.Compare(_currentSortColumn, "count desc") == 0)
				{
				}
				else if (string.Compare(_currentSortColumn, "price") == 0)
				{
					guitars = guitars.OrderBy(guitar => guitar.Price);
				}
				else if (string.Compare(_currentSortColumn, "price desc") == 0)
				{
					guitars = guitars.OrderByDescending(guitar => guitar.Price);
				}
			}

			if (!string.IsNullOrEmpty(searchString))
			{
				string temp = searchString.ToLower();

				guitars = guitars.Where(guitar => guitar.Model.ToLower().Contains(temp));
			}

			if (brandId != null)
			{
				guitars = guitars.Where(guitar => guitar.BrandId == brandId);
			}

			List<Guitar> list = guitars.ToList();
			List<GuitarViewModel> guitarViewModels = new List<GuitarViewModel>();

			foreach (Guitar guitar in list)
			{
				guitarViewModels.Add(CreateGuitarViewModel(guitar));
			}

			int pageSize = 3;
			int pageNumber = (page ?? 1);
			return View(guitarViewModels.ToPagedList(pageNumber, pageSize));
		}

		public ActionResult Details(int id = 0)
		{
			var guitar = _db.Guitars
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

		[ChildActionOnly]
		public ActionResult NavigationMenu()
		{
			List<Guitar> guitars = _db.Guitars.ToList();
			List<Brand> brands = _db.Brands.ToList();
			List<BrandInfo> brandsInfo = new List<BrandInfo>();

			brands.ForEach(brand => brandsInfo.Add(
				new BrandInfo(brand, guitars.Where(guitar => guitar.BrandId == brand.Id).Count())));

			return PartialView(brandsInfo);
		}
		#endregion
	}
}
