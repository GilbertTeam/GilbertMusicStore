using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Data.Entity.Validation;

namespace GilbertMusicStore.Models
{
	public class MusicStoreInitializer : DropCreateDatabaseAlways<MusicStoreEntities>
	{
		protected override void Seed(MusicStoreEntities context)
		{
			BodyType dreadnoughtBodyType = context.BodyTypes.Add(new BodyType { Name = "Dreadnought" });
			BodyType jumboBodyType = context.BodyTypes.Add(new BodyType { Name = "Jumbo" });
			BodyType westernBodyType = context.BodyTypes.Add(new BodyType { Name = "Western" });

			Brand gibsonBrand = context.Brands.Add(new Brand { Name = "Gibson" });
			Brand ibanezBrand = context.Brands.Add(new Brand { Name = "Ibanez" });
			Brand fenderBrand = context.Brands.Add(new Brand { Name = "Fender" });
			Brand yamahaBrand = context.Brands.Add(new Brand { Name = "Yamaha" });

			Color naturalColor = context.Colors.Add(new Color { Name = "Натуральный" });
			Color blueColor = context.Colors.Add(new Color { Name = "Синий" });
			Color blackColor = context.Colors.Add(new Color { Name = "Черный" });

			Manufacturer usaManufacturer = context.Manufacturers.Add(new Manufacturer { Name = "США" });
			Manufacturer koreaManufacturer = context.Manufacturers.Add(new Manufacturer { Name = "Корея" });
			Manufacturer chinaManufacturer = context.Manufacturers.Add(new Manufacturer { Name = "Китай" });
			Manufacturer japanManufacturer = context.Manufacturers.Add(new Manufacturer { Name = "Япония" });

			Pickup pickup1 = context.Pickups.Add(new Pickup { Name = "L.R. Baggs Element 1" });
			Pickup pickup2 = context.Pickups.Add(new Pickup { Name = "L.R. Baggs Element 2" });
			Pickup pickup3 = context.Pickups.Add(new Pickup { Name = "L.R. Baggs Element 3" });
			Pickup pickup4 = context.Pickups.Add(new Pickup { Name = "L.R. Baggs Element 4" });

			Preamp preamp1 = context.Preamps.Add(new Preamp { Name = "Ibanez AEQ200T 1" });
			Preamp preamp2 = context.Preamps.Add(new Preamp { Name = "Ibanez AEQ200T 2" });
			Preamp preamp3 = context.Preamps.Add(new Preamp { Name = "Ibanez AEQ200T 3" });
			Preamp preamp4 = context.Preamps.Add(new Preamp { Name = "Ibanez AEQ200T 4" });

			Wood mapleWood = context.Woods.Add(new Wood { Name = "Клен" });
			Wood spruceWood = context.Woods.Add(new Wood { Name = "Ель" });
			Wood palisanderWood = context.Woods.Add(new Wood { Name = "Палисандр" });
			Wood mahoganyWood = context.Woods.Add(new Wood { Name = "Махагони" });
			Wood merantiWood = context.Woods.Add(new Wood { Name = "Меранти" });
			Wood natoWood = context.Woods.Add(new Wood { Name = "Нато" });

			string appDataPath = Path.Combine((string)AppDomain.CurrentDomain.GetData("DataDirectory"), "Sample Data");
			string resourcesPath = Path.Combine(appDataPath, "Yamaha C40 Classical Guitar");
			byte[] buffer;

			using (FileStream fileStream = File.Open(Path.Combine(resourcesPath, "MainPhoto.jpg"), FileMode.Open))
			{
				buffer = new byte[fileStream.Length];

				fileStream.Read(buffer, 0, (int)fileStream.Length);
			}

			Guitar guitar =context.Guitars.Add(new AcousticGuitar
			{
				BodyType = jumboBodyType,
				BodyWood = merantiWood,
				Color = blackColor,
				Manufacturer = japanManufacturer,
				FretboardWood = natoWood,
				FingerboardWood = palisanderWood,
				FretsCount = 19,
				IsClassical = true,
				Model = "Yamaha C40 Classical Guitar",
				Brand = yamahaBrand,
				Series = "C40",
				Scale = 650,
				MainImage = buffer
			});

			try
			{
				context.SaveChanges();
			}
			catch (DbEntityValidationException exception)
			{

			}
		}
	}
}