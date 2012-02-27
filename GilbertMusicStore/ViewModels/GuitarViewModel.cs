using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GilbertMusicStore.Models;

namespace GilbertMusicStore.ViewModels
{
	public class GuitarViewModel
	{
		#region Properties

		public Guitar Guitar { get; set; }

		public string Description { get; set; }

		public int Count { get; set; }

		public IList<GuitarReview> Reviews { get; set; }
		#endregion
	}
}