using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GilbertMusicStore.Models
{
	public class GuitarReview
	{
		public int Id { get; set; }

		public virtual Guitar Guitar { get; set; }

		public string Content { get; set; }

		public DateTime DateCreated { get; set; }
	}
}