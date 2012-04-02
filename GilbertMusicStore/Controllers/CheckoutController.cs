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
				SendEmail(id);
				return View(id);
			}
			else
			{
				return View("Error");
			}
		}

		#region MailSend

		void SendEmail(int id)
		{
			Order order=_db.Orders.Find(id);
			System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage();
			mm.From = new System.Net.Mail.MailAddress("seralexandr@bk.ru");
			mm.To.Add(new System.Net.Mail.MailAddress(order.Email));
			mm.Subject = "Ваш заказ!";
			mm.IsBodyHtml = true;//письмо в html формате (если надо)
			string text = "Здравствуйте, " + order.FirstName + " " + order.LastName + "!<br>";
			text += "Вы сделали заказ на гитары в магазине Gilbert Music Store! <br>";
			text += "Ваш заказ:<br>";
			var details=_db.OrderDetails.Where(orderDetail => orderDetail.OrderId==id);
			int i = 1;
			foreach (OrderDetail d in details)
			{
				text += i.ToString() + ").   ";
				text += d.Guitar.Description+"<br><br>";
				text += "<img src=" + d.Guitar.LargeMainImageUrl + ">";
				i++;
			}
			text +="Общая стоимость заказа " +order.Total.ToString()+"p.";
			mm.Body = text;
			System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.mail.ru");
			client.Credentials = new System.Net.NetworkCredential("seralexandr@bk.ru", "MyNewPassword"); 
			client.Send(mm);//поехало
		}

		#endregion

		#endregion

	}
}
