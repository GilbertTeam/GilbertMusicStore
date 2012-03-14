using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Objects;
using System.Data.Entity.Infrastructure;

namespace GilbertMusicStore.Models
{
	public class ShoppingCart
	{
		#region Fields

		private const string SessionKey = "CartId";

		private MusicStoreEntities _db = new MusicStoreEntities();
		#endregion

		#region Properties

		private string ShoppingCartId { get; set; }
		#endregion

		#region Constructors

		protected ShoppingCart()
		{

		}
		#endregion

		#region Methods

		public static ShoppingCart GetCart(HttpContextBase context)
		{
			string cartId = GetCartId(context);
			ShoppingCart shoppingCart = new ShoppingCart();
			shoppingCart.ShoppingCartId = cartId;

			return shoppingCart;
		}

		public static ShoppingCart GetCart(Controller controller)
		{
			return GetCart(controller.HttpContext);
		}

		public static string GetCartId(HttpContextBase context)
		{
			string cartId = null;

			if (context.Session[SessionKey] == null)
			{
				string userName = context.User.Identity.Name;

				if (!string.IsNullOrWhiteSpace(userName))
				{
					cartId = userName;
					context.Session[SessionKey] = cartId;
				}
				else
				{
					Guid tempCartId = Guid.NewGuid();

					cartId = tempCartId.ToString();
					context.Session[SessionKey] = cartId;
				}
			}

			return cartId;
		}

		public void AddToCart(Guitar guitar)
		{
			Cart cart =
				_db.Carts.SingleOrDefault(
					c => c.Tag == ShoppingCartId &&
					c.GuitarId == guitar.Id);

			if (cart == null)
			{
				cart = new Cart
				{
					Guitar = guitar,
					Tag = ShoppingCartId,
					Count = 1,
					DateCreated = DateTime.Now
				};

				_db.Carts.Add(cart);
			}
			else
			{
				cart.Count++;
			}

			_db.SaveChanges();
		}

		public int RemoveFromCart(int id)
		{
			Cart cart =
				_db.Carts.SingleOrDefault(
					c => c.Tag == ShoppingCartId &&
					c.Id == id);

			int itemCount = 0;

			if (cart != null)
			{
				if (cart.Count > 1)
				{
					itemCount = cart.Count--;
				}
				else
				{
					_db.Carts.Remove(cart);
				}

				_db.SaveChanges();
			}

			return itemCount;
		}

		public IList<Cart> GetCarts()
		{
			return _db.Carts.Where(c => c.Tag == ShoppingCartId).ToList();
		}

		public void EmptyCart()
		{
			var carts = GetCarts();

			foreach (var cart in carts)
			{
				_db.Carts.Remove(cart);
			}

			_db.SaveChanges();
		}

		public int GetCount()
		{
			int? count = (from cart in _db.Carts
						  where cart.Tag == ShoppingCartId
						  select (int?)cart.Count).Sum();

			return count ?? 0;
		}

		public decimal GetTotal()
		{
			decimal? total = (from cart in _db.Carts
							  where cart.Tag == ShoppingCartId
							  select (int?)cart.Count * cart.Guitar.Price).Sum();

			return total ?? decimal.Zero;
		}

		public int CreateOrder(Order order)
		{
			decimal orderTotal = 0;
			var carts = GetCarts();

			foreach (var cart in carts)
			{
				OrderDetail orderDetail = new OrderDetail
				{
					OrderId = order.Id,
					Guitar = cart.Guitar,
					UnitPrice = cart.Guitar.Price,
					Quantity = cart.Count
				};

				orderTotal += cart.Count * cart.Guitar.Price;

				_db.OrderDetails.Add(orderDetail);
			}

			order.Total = orderTotal;
			_db.SaveChanges();

			EmptyCart();

			return order.Id;
		}

		public void MigrateCart(string userName)
		{
			var carts = GetCarts();

			foreach (var cart in carts)
			{
				cart.Tag = userName;
			}

			_db.SaveChanges();
		}
		#endregion
	}
}