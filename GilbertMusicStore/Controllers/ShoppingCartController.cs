using System.Web.Mvc;
using System.Linq;
using GilbertMusicStore.Models;
using GilbertMusicStore.ViewModels;

namespace GilbertMusicStore.Controllers
{
	public class ShoppingCartController : Controller
	{
		#region Fields

		private readonly MusicStoreEntities _db = new MusicStoreEntities();
		#endregion

		#region Methods

		public ActionResult Index()
		{
			ShoppingCart shoppingCart = ShoppingCart.GetCart(HttpContext);
			ShoppingCartViewModel viewModel = new ShoppingCartViewModel
			{
				Carts = shoppingCart.GetCarts().ToList(),
				Total = shoppingCart.GetTotal()
			};

			return View(viewModel);
		}

		public ActionResult AddToCart(int id)
		{
			Guitar guitar = _db.Guitars.SingleOrDefault(g => g.Id == id);

			if (guitar != null)
			{
				ShoppingCart shoppingCart = ShoppingCart.GetCart(HttpContext);
				shoppingCart.AddToCart(guitar);

				return RedirectToAction("Index");
			}
			else
			{
				return HttpNotFound();
			}
		}

		[HttpPost]
		public ActionResult RemoveFromCart(int id)
		{
			Cart cart = _db.Carts.SingleOrDefault(c => c.Id == id);

			if (cart != null)
			{
				ShoppingCart shoppingCart = ShoppingCart.GetCart(HttpContext);
				Guitar guitar = cart.Guitar;
				string guitarName = string.Format("{0} {1} {2}", guitar.Brand.Name, guitar.Series, guitar.Model);
				int itemCount = shoppingCart.RemoveFromCart(id);
				ShoppingCartRemoveViewModel viewModel = new ShoppingCartRemoveViewModel
				{
					Message = Server.HtmlEncode(guitarName) + " удалена из БД.",
					Total = shoppingCart.GetTotal(),
					CartCount = shoppingCart.GetCount(),
					ItemCount = itemCount,
					DeleteId = id
				};

				return Json(viewModel);
			}
			else
			{
				return HttpNotFound();
			}
		}

		[ChildActionOnly]
		public ActionResult CartSummary()
		{
			ShoppingCart shoppingCart = ShoppingCart.GetCart(HttpContext);

			ViewBag.CartCount = shoppingCart.GetCount();

			return PartialView("CartSummary");
		}
		#endregion
	}
}
