using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GilbertMusicStore.Models;

namespace GilbertMusicStore.Controllers
{
	public class NewsController : Controller
	{
		#region Fields

		private readonly MusicStoreContext _db = new MusicStoreContext();
		#endregion

		#region Methods

		public ActionResult Index()
		{
			return View(_db.News.ToList());
		}

		public ActionResult About()
		{
			return View();
		}
		#endregion
	}
}
