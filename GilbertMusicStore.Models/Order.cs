using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GilbertMusicStore.Models.Attributes;

namespace GilbertMusicStore.Models
{
	[Bind(Exclude = "OrderId")]
	public class Order
	{
		[ScaffoldColumn(false)]
		public int Id { get; set; }

		[ScaffoldColumn(false)]
		public DateTime OrderDate { get; set; }

		[ScaffoldColumn(false)]
		public string UserName { get; set; }

		[RequiredEx]
		[StringLengthEx(100)]
		[Display(Name = "Имя")]
		public string FirstName { get; set; }

		[RequiredEx]
		[StringLengthEx(100)]
		[Display(Name = "Фамилия")]
		public string LastName { get; set; }

		[RequiredEx]
		[StringLengthEx(50)]
		[Display(Name = "Адрес")]
		public string Address { get; set; }

		[RequiredEx]
		[StringLengthEx(50)]
		[Display(Name = "Город")]
		public string City { get; set; }

		[RequiredEx]
		[StringLengthEx(50)]
		[Display(Name = "Страна")]
		public string Country { get; set; }

		[RequiredEx]
		[StringLengthEx(50)]
		[Display(Name = "Почтовый индекс")]
		public string PostalCode { get; set; }

		[RequiredEx]
		[DataType(DataType.PhoneNumber)]
		[Display(Name = "Телефон")]
		public string Phone { get; set; }

		[RequiredEx]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Е-mail")]
		public string Email { get; set; }

		[ScaffoldColumn(false)]
		public decimal Total { get; set; }

		public ICollection<OrderDetail> OrderDetails { get; set; }
	}
}