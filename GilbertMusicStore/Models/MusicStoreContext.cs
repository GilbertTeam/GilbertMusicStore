using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace GilbertMusicStore.Models
{
	public class MusicStoreContext : DbContext
	{
		public DbSet<Brand> Brands { get; set; }
		public DbSet<Guitar> Guitars { get; set; }
		public DbSet<BodyType> BodyTypes { get; set; }
		public DbSet<Preamp> Preamps { get; set; }
		public DbSet<Wood> Woods { get; set; }
		public DbSet<Pickup> Pickups { get; set; }
		public DbSet<Color> Colors { get; set; }
		public DbSet<Manufacturer> Manufacturers { get; set; }
		public DbSet<GuitarReview> GuitarReviews { get; set; }
		public DbSet<News> News { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }

		public MusicStoreContext()
		{
			Configuration.ValidateOnSaveEnabled = false;
			//Configuration.LazyLoadingEnabled = false;
			//Configuration.ProxyCreationEnabled = false;
		}
	}
}