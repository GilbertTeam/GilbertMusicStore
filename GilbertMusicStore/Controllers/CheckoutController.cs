using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GilbertMusicStore.Models;

namespace GilbertMusicStore.Controllers
{
	[Authorize]
	public class CheckoutController : Controller
	{
		#region Fields

		private const string PromoCode = "FREE";

		private readonly MusicStoreContext _db = new MusicStoreContext();
		#endregion

		#region Methods

		public ViewResult AddressAndPayment()
		{
			return View();
		}

		[HttpPost]
		public ActionResult AddressAndPayment(FormCollection values)
		{
			Order order = new Order();

			TryUpdateModel(order);

			try
			{
				if (!string.Equals(values["PromoCode"], PromoCode, StringComparison.OrdinalIgnoreCase))
				{
					return View(order);
				}
				else
				{
					order.UserName = User.Identity.Name;
					order.OrderDate = DateTime.Now;

					_db.Orders.Add(order);
					_db.SaveChanges();

					ShoppingCart shoppingCart = ShoppingCart.GetCart(HttpContext);
					shoppingCart.CreateOrder(order);

					return RedirectToAction("Complete", new { id = order.Id });
				}
			}
			catch (Exception)
			{
				return View(order);
			}
		}

		public ActionResult Complete(int id)
		{
			bool isValid = _db.Orders.Any(order =>
				order.Id == id && 
				order.UserName == User.Identity.Name);

			if (isValid)
			{
				return View(id);
			}
			else
			{
				return View("Error");
			}
		}
		#endregion
	}
}
