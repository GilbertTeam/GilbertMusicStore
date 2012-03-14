using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GilbertMusicStore.Models
{
	public class Order
	{
		[ScaffoldColumn(false)]
		public int Id { get; set; }

		public string UserName { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Address { get; set; }

		public string City { get; set; }

		public string PostalCode { get; set; }

		public string Country { get; set; }

		[DataType(DataType.PhoneNumber)]
		public string Phone { get; set; }

		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		public decimal Total { get; set; }

		public DateTime OrderDate { get; set; }

		public ICollection<OrderDetail> OrderDetails { get; set; }
	}
}