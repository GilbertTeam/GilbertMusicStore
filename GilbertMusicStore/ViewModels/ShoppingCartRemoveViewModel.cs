using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GilbertMusicStore.ViewModels
{
	public class ShoppingCartRemoveViewModel
	{
		public string Message { get; set; }

		public decimal Total { get; set; }

		public int CartCount { get; set; }

		public int ItemCount { get; set; }

		public int DeleteId { get; set; }
	}
}