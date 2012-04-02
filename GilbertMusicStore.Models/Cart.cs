using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GilbertMusicStore.Models
{
	public class Cart
	{
		[ScaffoldColumn(false)]
		public int Id { get; set; }

		[ScaffoldColumn(false)]
		public string Tag { get; set; }

		[ScaffoldColumn(false)]
		public int GuitarId { get; set; }

		public int Count { get; set; }

		public DateTime DateCreated { get; set; }

		public virtual Guitar Guitar { get; set; }
	}
}