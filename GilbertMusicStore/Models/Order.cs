using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GilbertMusicStore.Models
{
	public class StringLengthExAttribute : StringLengthAttribute
	{
		public StringLengthExAttribute(int maximumLenght)
			: base(maximumLenght)
		{
			DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(StringLengthExAttribute), typeof(StringLengthAttribute));
		}

		public override string FormatErrorMessage(string name)
		{
			return base.FormatErrorMessage(name);
		}
	}

	[Bind(Exclude = "OrderId")]
	public class Order
	{
		[ScaffoldColumn(false)]
		public int Id { get; set; }

		[ScaffoldColumn(false)]
		public DateTime OrderDate { get; set; }

		[ScaffoldColumn(false)]
		public string UserName { get; set; }

		[Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(Properties.Resources))]
		[StringLengthEx(150)]
		public string FirstName { get; set; }

		[Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(Properties.Resources))]
		[StringLength(150)]
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

		

		public ICollection<OrderDetail> OrderDetails { get; set; }
	}
}