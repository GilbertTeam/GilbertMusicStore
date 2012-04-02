using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using GilbertMusicStore.Models;

namespace GilbertMusicStore.Controllers
{
	public class AccountController : Controller
	{
		#region Methods

		#region Status Codes

		private static string ErrorCodeToString(MembershipCreateStatus createStatus)
		{
			// See http://go.microsoft.com/fwlink/?LinkID=177550 for
			// a full list of status codes.
			switch (createStatus)
			{
			case MembershipCreateStatus.DuplicateUserName:
					return "Такое имя пользователя уже существует. Пожалуйста введите другое имя пользователя.";

			case MembershipCreateStatus.DuplicateEmail:
					return "Пользователь с таким e-mail'ом уже существует. Пожалуйста введите другой e-mail.";

			case MembershipCreateStatus.InvalidPassword:
					return "Введёный пароль является неверным. Пожалуйста введите верный пароль.";

			case MembershipCreateStatus.InvalidEmail:
					return "Введёный e-mail является неверным. Пожалуйста введите другой и повторите.";

			case MembershipCreateStatus.InvalidAnswer:
			return "Ответ на вопрос неверен. Проверьте значение и повторите снова.";

			case MembershipCreateStatus.InvalidQuestion:
			return "Вопрос неверен. Пожалуйста проверьте значение и повторите снова.";

			case MembershipCreateStatus.InvalidUserName:
			return "Неверное имя пользователя. Пожалуйста проверьте значение и повторите снова.";

			case MembershipCreateStatus.ProviderError:
			return "Ошибка в сервисе аутентификации. Пожалуйста проверьте значение и попробуйте снова. Если проблема повторится, свяжитесь с администрацией.";

			case MembershipCreateStatus.UserRejected:
			return "Запрос на создание пользователя был отвергнут. Пожалуйста проверьте введёный данные и попробуйте снова. Если проблема повторится, свяжитесь с администрацией.";

			default:
			return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
			}
		}
		#endregion

		private void CheckoutShoppingCart(string userName)
		{
			ShoppingCart shoppingCart = ShoppingCart.GetCart(HttpContext);

			shoppingCart.Checkout(userName);
			Session[ShoppingCart.SessionKey] = userName;
		}

		public ActionResult LogOn()
		{
			return View();
		}

		[HttpPost]
		public ActionResult LogOn(LogOnModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				if (Membership.ValidateUser(model.UserName, model.Password))
				{
					CheckoutShoppingCart(model.UserName);

					FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
					if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
						&& !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
					{
						return Redirect(returnUrl);
					}
					else
					{
						return RedirectToAction("Index", "News");
					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, Properties.Resources.AuthorizationError);
				}
			}

			return View(model);
		}

		public ActionResult LogOff()
		{
			FormsAuthentication.SignOut();

			return RedirectToAction("Index", "News");
		}

		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Register(RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				MembershipCreateStatus createStatus;
				Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

				if (createStatus == MembershipCreateStatus.Success)
				{
					CheckoutShoppingCart(model.UserName);

					FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
					return RedirectToAction("Index", "News");
				}
				else
				{
					ModelState.AddModelError("", ErrorCodeToString(createStatus));
				}
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		[Authorize]
		public ActionResult ChangePassword()
		{
			return View();
		}

		[Authorize]
		[HttpPost]
		public ActionResult ChangePassword(ChangePasswordModel model)
		{
			if (ModelState.IsValid)
			{
				// ChangePassword will throw an exception rather
				// than return false in certain failure scenarios.
				bool changePasswordSucceeded;
				try
				{
					MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
					changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
				}
				catch (Exception)
				{
					changePasswordSucceeded = false;
				}

				if (changePasswordSucceeded)
				{
					return RedirectToAction("ChangePasswordSuccess");
				}
				else
				{
					ModelState.AddModelError("", "Текущий пароль неправильный.");
				}
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		public ActionResult ChangePasswordSuccess()
		{
			return View();
		}
		#endregion
	}
}