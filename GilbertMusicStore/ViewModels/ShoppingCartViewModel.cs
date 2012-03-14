using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GilbertMusicStore.Models;

namespace GilbertMusicStore.ViewModels
{
	public class ShoppingCartViewModel
	{
		public List<Cart> Carts { get; set; }

		public decimal Total { get; set; }
	}
}