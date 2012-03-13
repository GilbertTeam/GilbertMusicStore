using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GilbertMusicStore.Models
{
	public class News
	{
		public int Id { get; set; }

		public DateTime Date { get; set; }

		public string Title { get; set; }

		public string Content { get; set; }
	}
}