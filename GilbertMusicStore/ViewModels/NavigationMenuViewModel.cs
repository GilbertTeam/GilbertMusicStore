using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GilbertMusicStore.Models;

namespace GilbertMusicStore.ViewModels
{
	public class BrandInfo
	{
		public Brand Brand { get; private set; }

		public int Count { get; private set; }

		public BrandInfo(Brand brand, int count)
		{
			Brand = brand;
			Count = count;
		}
	}

	public class NavigationMenuViewModel
	{
		public Guitar CurrentGuitar { get; private set; }

		public IEnumerable<BrandInfo> BrandsInfo { get; private set; }

		public NavigationMenuViewModel(Guitar currentGuitar, IEnumerable<BrandInfo> brandsInfo)
		{
			CurrentGuitar = currentGuitar;
			BrandsInfo = brandsInfo;
		}
	}
}