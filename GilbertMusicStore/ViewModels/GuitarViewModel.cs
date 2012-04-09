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

		public Guitar Guitar { get; private set; }

		public string Description { get; private set; }

		public int GuitarCount { get; private set; }

		public IEnumerable<Guitar> RelatedGuitars { get; private set; }

		public IEnumerable<GuitarReview> Reviews { get; set; }
		#endregion

		#region Constructors

		public GuitarViewModel(Guitar guitar, string description, int guitarCount, IEnumerable<Guitar> relatedGuitars)
		{
			Guitar = guitar;
			Description = description;
			GuitarCount = guitarCount;
			RelatedGuitars = relatedGuitars;
		}
		#endregion
	}
}