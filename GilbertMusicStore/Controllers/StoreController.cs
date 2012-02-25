using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GilbertMusicStore.Models;

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
			return View(_entities.Guitars);
		}
		#endregion
	}
}
