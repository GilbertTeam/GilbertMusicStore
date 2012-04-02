using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GilbertMusicStore.Models
{
	public class OrderDetail
	{
		[ScaffoldColumn(false)]
		public int Id { get; set; }

		[ScaffoldColumn(false)]
		public int OrderId { get; set; }

		[ScaffoldColumn(false)]
		public int GuitarId { get; set; }

		public int Quantity { get; set; }

		public decimal UnitPrice { get; set; }

		public virtual Guitar Guitar { get; set; }

		public virtual Order Order { get; set; }
	}
}