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

		public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
		{
			IEnumerable<Guitar> guitars =
				_db.Guitars
				.Include(g => g.Color)
				.Include(g => g.BodyWood)
				.Include(g => g.FretboardWood)
				.Include(g => g.FretboardWood)
				.Include(g => g.FingerboardWood);

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
				_currentSortColumn = "none";
			}
			else
			{
				string temp = sortOrder.ToLower();
				if (_currentSortColumn != temp)
				{
					//По умолчанию используется сортировка по возрастанию.
					_currentCortDirection = SortDirection.Ascending;
				}
				else
				{
					//Если сортировка уже была включена, то переключим ее.
					_currentCortDirection = _currentCortDirection != SortDirection.Ascending ? SortDirection.Ascending : SortDirection.Descending;
				}

				_currentSortColumn = temp;
				ViewBag.CurrentSortColumn = _currentSortColumn;

				if (string.Compare(_currentSortColumn, "name") == 0)
				{
					switch (_currentCortDirection)
					{
						case SortDirection.Ascending:
							guitars = guitars.OrderBy(guitar => guitar.Model);
							break;
						case SortDirection.Descending:
							guitars = guitars.OrderByDescending(guitar => guitar.Model);
							break;
					}
				}
				else if (string.Compare(_currentSortColumn, "count") == 0)
				{
				}
				else if (string.Compare(_currentSortColumn, "price") == 0)
				{
					switch (_currentCortDirection)
					{
						case SortDirection.Ascending:
							guitars = guitars.OrderBy(guitar => guitar.Price);
							break;
						case SortDirection.Descending:
							guitars = guitars.OrderByDescending(guitar => guitar.Price);
							break;
					}
				}
			}

			if (!string.IsNullOrEmpty(searchString))
			{
				string temp = searchString.ToLower();

				guitars = guitars.Where(guitar => guitar.Model.ToLower().Contains(temp));
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
