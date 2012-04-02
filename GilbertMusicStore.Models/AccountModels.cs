using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GilbertMusicStore.Models.Attributes;

namespace GilbertMusicStore.Models
{
	public class ChangePasswordModel
	{
		[RequiredEx]
		[DataType(DataType.Password)]
		[Display(Name = "Текущий пароль")]
		public string OldPassword { get; set; }

		[RequiredEx]
		[StringLengthEx(100, MinimumLength = 6)]
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
		[RequiredEx]
		[Display(Name = "Имя пользователя")]
		public string UserName { get; set; }

		[RequiredEx]
		[DataType(DataType.Password)]
		[Display(Name = "Пароль")]
		public string Password { get; set; }

		[Display(Name = "Запомнить меня?")]
		public bool RememberMe { get; set; }
	}

	public class RegisterModel
	{
		[RequiredEx]
		[Display(Name = "Имя пользователя")]
		public string UserName { get; set; }

		[RequiredEx]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Email адрес")]
		public string Email { get; set; }

		[RequiredEx]
		[StringLengthEx(100, MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Пароль")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Подтвердите пароль")]
		[Compare("Password", ErrorMessage = "Пароли не совпадают.")]
		public string ConfirmPassword { get; set; }
	}
}
