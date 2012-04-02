using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace GilbertMusicStore.Models
{

	public class ChangePasswordModel
	{
		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Текущий пароль")]
		public string OldPassword { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "{0} должен быть минимум {2} символов в длину.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Новый пароль")]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Подтвердите новый пароль")]
		[Compare("NewPassword", ErrorMessage = "Пароли не совпадают.")]
		public string ConfirmPassword { get; set; }
	}

	public class LogOnModel
	{
		[Required]
		[Display(Name = "Имя пользователя")]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Пароль")]
		public string Password { get; set; }

		[Display(Name = "Запомнить меня?")]
		public bool RememberMe { get; set; }
	}

	public class RegisterModel
	{
		[Required]
		[Display(Name = "Имя пользователя")]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Email адрес")]
		public string Email { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "{0} должен быть минимум {2} символов в длину.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Пароль")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Подтвердите пароль")]
		[Compare("Password", ErrorMessage = "Пароли не совпадают.")]
		public string ConfirmPassword { get; set; }
	}
}
