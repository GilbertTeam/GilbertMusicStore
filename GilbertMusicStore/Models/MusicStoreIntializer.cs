using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Data.Entity.Validation;
using System.Web.Helpers;

namespace GilbertMusicStore.Models
{
	public class MusicStoreInitializer : DropCreateDatabaseAlways<MusicStoreEntities>
	{
		#region Fields

		private readonly string _siteDirectory;
		private readonly string _contentDirectory;
		private readonly string _sampleDataDirectory;
		private readonly string _imagesDirectory;
		private readonly string _largeImagesDirectory;
		private readonly string _smallImagesDirectory;
		#endregion

		#region Constructors

		public MusicStoreInitializer()
		{
			_siteDirectory = HttpContext.Current.Server.MapPath("~");
			_contentDirectory = Path.Combine(_siteDirectory, "Content");
			_sampleDataDirectory = Path.Combine(_contentDirectory, "Sample Data");
			_imagesDirectory = Path.Combine(_contentDirectory, "Images");
			_largeImagesDirectory = Path.Combine(_imagesDirectory, "Large");
			_smallImagesDirectory = Path.Combine(_imagesDirectory, "Small");
		}
		#endregion

		#region Methods

		private string GetGuitarDescription(string guitarDirectory)
		{
			string descriptionFile = Path.Combine(guitarDirectory, "Description.txt");
			string description = string.Empty;

			if (File.Exists(descriptionFile))
			{
				using (StreamReader reader = new StreamReader(descriptionFile))
				{
					description = reader.ReadToEnd();
				}
			}

			return description;
		}

		private void PrepareGuitarImages(int currentId, string guitarDirectory, out string virtualLargeMainImageFile, out string virtualSmallMainImageFile)
		{
			string currentLargeImagesDirectory = Path.Combine(_largeImagesDirectory, currentId.ToString());
			string currentSmallImagesDirectory = Path.Combine(_smallImagesDirectory, currentId.ToString());
			string imageFileExtension = ".jpg";
			string mainImageFile = Path.Combine(guitarDirectory, "MainPhoto.jpg");

			if (!File.Exists(mainImageFile))
			{
				imageFileExtension = ".png";
				mainImageFile = Path.ChangeExtension(mainImageFile, imageFileExtension);
			}
			
			virtualLargeMainImageFile = Path.Combine(currentLargeImagesDirectory, "MainImage" + imageFileExtension);
			virtualSmallMainImageFile = Path.Combine(currentSmallImagesDirectory, "MainImage" + imageFileExtension);

			if (!Directory.Exists(currentLargeImagesDirectory))
			{
				Directory.CreateDirectory(currentLargeImagesDirectory);
			}

			if (!Directory.Exists(currentSmallImagesDirectory))
			{
				Directory.CreateDirectory(currentSmallImagesDirectory);
			}

			if (File.Exists(mainImageFile))
			{
				if (!File.Exists(virtualLargeMainImageFile))
				{
					File.Copy(mainImageFile, Path.Combine(virtualLargeMainImageFile));
				}
				if (!File.Exists(virtualSmallMainImageFile))
				{
					WebImage mainImage = new WebImage(mainImageFile);
					mainImage.Resize(50, 120);
					mainImage.RotateRight();
					mainImage.Save(virtualSmallMainImageFile);
				}
			}

			virtualLargeMainImageFile = "/" + virtualLargeMainImageFile.Replace(_siteDirectory, string.Empty).Replace("\\", "/");
			virtualSmallMainImageFile = "/" + virtualSmallMainImageFile.Replace(_siteDirectory, string.Empty).Replace("\\", "/");

			//string[] imageFiles = new string[4]
			//{
			//    Path.Combine(guitarDirectory, "UserPhoto1.jpg"),
			//    Path.Combine(guitarDirectory, "UserPhoto2.jpg"),
			//    Path.Combine(guitarDirectory, "UserPhoto3.jpg"),
			//    Path.Combine(guitarDirectory, "UserPhoto4.jpg")
			//};

			//for (int i = 0; i < 4; i++)
			//{
			//    string virtualImageFile = Path.Combine(currentSmallImagesDirectory, "UserImage" + (i + 1).ToString() + ".jpg");

			//    if (File.Exists(imageFiles[i]) &&
			//        !File.Exists(virtualImageFile))
			//    {
			//        WebImage webImage = new WebImage(imageFiles[i]);
			//        webImage.Resize(120, 50, false);
			//        //webImage.RotateRight();
			//        webImage.Save(virtualImageFile);
			//    }

			//    imageFiles[i] = "/" + virtualImageFile.Replace(siteDirectory, string.Empty).Replace("\\", "/");
			//}
		}

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
			Color vintageSunburstColor = context.Colors.Add(new Color { Name = "Vintage Sunburst" });
			Color antiqueNaturalColor = context.Colors.Add(new Color { Name = "Antique Natural" });

			Manufacturer indonesiaManufacturer = context.Manufacturers.Add(new Manufacturer { Name = "Индонезия" });
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
			Wood bubingaWood = context.Woods.Add(new Wood { Name = "Бубинга" });

			int currentId = 1;
			string guitarModel = "Yamaha C40 Natural";
			string guitarDirectory = Path.Combine(_sampleDataDirectory, guitarModel);
			string gutiarDescription = GetGuitarDescription(guitarDirectory);
			string virtualLargeMainImageFile;
			string virtualSmallMainImageFile;

			PrepareGuitarImages(currentId, guitarDirectory, out virtualLargeMainImageFile, out virtualSmallMainImageFile);

			Guitar guitar = context.Guitars.Add(
				new AcousticGuitar
				{
					Brand = yamahaBrand,
					Series = "C40",
					Model = guitarModel,
					BodyType = jumboBodyType,
					FretsCount = 19,
					Scale = 650,
					BodyWood = merantiWood,
					FretboardWood = natoWood,
					FingerboardWood = palisanderWood,
					Color = naturalColor,
					Description = gutiarDescription,
					Manufacturer = indonesiaManufacturer,
					LargeMainImageUrl = virtualLargeMainImageFile,
					SmallMainImageUrl = virtualSmallMainImageFile,
					Price = 3390,
					IsClassical = true
				});

			currentId = 2;
			guitarModel = "Fender CD-60 Black";
			guitarDirectory = Path.Combine(_sampleDataDirectory, guitarModel);
			gutiarDescription = GetGuitarDescription(guitarDirectory);

			PrepareGuitarImages(currentId, guitarDirectory, out virtualLargeMainImageFile, out virtualSmallMainImageFile);

			guitar = context.Guitars.Add(
				new AcousticGuitar
				{
					Brand = fenderBrand,
					Series = "CD-60",
					Model = guitarModel,
					BodyType = dreadnoughtBodyType,
					FretsCount = 20,
					Scale = 650,
					BodyWood = natoWood,
					FretboardWood = palisanderWood,
					FingerboardWood = palisanderWood,
					Color = blackColor,
					Description = gutiarDescription,
					Manufacturer = indonesiaManufacturer,
					LargeMainImageUrl = virtualLargeMainImageFile,
					SmallMainImageUrl = virtualSmallMainImageFile,
					Price = 6546,
					IsClassical = false
				});

			currentId = 3;
			guitarModel = "Yamaha F310 Natural";
			guitarDirectory = Path.Combine(_sampleDataDirectory, guitarModel);
			gutiarDescription = GetGuitarDescription(guitarDirectory);

			PrepareGuitarImages(currentId, guitarDirectory, out virtualLargeMainImageFile, out virtualSmallMainImageFile);

			guitar = context.Guitars.Add(
				new AcousticGuitar
				{
					Brand = yamahaBrand,
					Series = "F310",
					Model = guitarModel,
					BodyType = dreadnoughtBodyType,
					FretsCount = 20,
					Scale = 650,
					BodyWood = merantiWood,
					FretboardWood = palisanderWood,
					FingerboardWood = palisanderWood,
					Color = naturalColor,
					Description = gutiarDescription,
					Manufacturer = indonesiaManufacturer,
					LargeMainImageUrl = virtualLargeMainImageFile,
					SmallMainImageUrl = virtualSmallMainImageFile,
					Price = 4180,
					IsClassical = false
				});

			currentId = 4;
			guitarModel = "Gibson Blues King Vintage Sunburst";
			guitarDirectory = Path.Combine(_sampleDataDirectory, guitarModel);
			gutiarDescription = GetGuitarDescription(guitarDirectory);

			PrepareGuitarImages(currentId, guitarDirectory, out virtualLargeMainImageFile, out virtualSmallMainImageFile);

			guitar = context.Guitars.Add(
				new AcousticGuitar
				{
					Brand = gibsonBrand,
					Series = "Blues King",
					Model = guitarModel,
					BodyType = jumboBodyType,
					FretsCount = 19,
					Scale = 650,
					BodyWood = bubingaWood,
					FretboardWood = mahoganyWood,
					FingerboardWood = palisanderWood,
					Color = vintageSunburstColor,
					Description = gutiarDescription,
					Manufacturer = usaManufacturer,
					LargeMainImageUrl = virtualLargeMainImageFile,
					SmallMainImageUrl = virtualSmallMainImageFile,
					Price = 85900,
					IsClassical = false
				});

			currentId = 5;
			guitarModel = "Gibson J-185 Vintage Sunburst";
			guitarDirectory = Path.Combine(_sampleDataDirectory, guitarModel);
			gutiarDescription = GetGuitarDescription(guitarDirectory);

			PrepareGuitarImages(currentId, guitarDirectory, out virtualLargeMainImageFile, out virtualSmallMainImageFile);

			guitar = context.Guitars.Add(
				new AcousticGuitar
				{
					Brand = gibsonBrand,
					Series = "J-185",
					Model = guitarModel,
					BodyType = jumboBodyType,
					FretsCount = 19,
					Scale = 650,
					BodyWood = mapleWood,
					FretboardWood = mahoganyWood,
					FingerboardWood = palisanderWood,
					Color = vintageSunburstColor,
					Description = gutiarDescription,
					Manufacturer = usaManufacturer,
					LargeMainImageUrl = virtualLargeMainImageFile,
					SmallMainImageUrl = virtualSmallMainImageFile,
					Price = 95900,
					IsClassical = false
				});

			currentId = 6;
			guitarModel = "Gibson J-185 Antique Natural";
			guitarDirectory = Path.Combine(_sampleDataDirectory, guitarModel);
			gutiarDescription = GetGuitarDescription(guitarDirectory);

			PrepareGuitarImages(currentId, guitarDirectory, out virtualLargeMainImageFile, out virtualSmallMainImageFile);

			guitar = context.Guitars.Add(
				new AcousticGuitar
				{
					Brand = gibsonBrand,
					Series = "J-185",
					Model = guitarModel,
					BodyType = jumboBodyType,
					FretsCount = 19,
					Scale = 650,
					BodyWood = mapleWood,
					FretboardWood = mahoganyWood,
					FingerboardWood = palisanderWood,
					Color = antiqueNaturalColor,
					Description = gutiarDescription,
					Manufacturer = usaManufacturer,
					LargeMainImageUrl = virtualLargeMainImageFile,
					SmallMainImageUrl = virtualSmallMainImageFile,
					Price = 95900,
					IsClassical = false
				});

			currentId = 7;
			guitarModel = "Ibanez V72ECE Black";
			guitarDirectory = Path.Combine(_sampleDataDirectory, guitarModel);
			gutiarDescription = GetGuitarDescription(guitarDirectory);

			PrepareGuitarImages(currentId, guitarDirectory, out virtualLargeMainImageFile, out virtualSmallMainImageFile);

			guitar = context.Guitars.Add(
				new SemiAcousticGuitar
				{
					Brand = ibanezBrand,
					Series = "V72ECE",
					Model = guitarModel,
					BodyType = jumboBodyType,
					FretsCount = 19,
					Scale = 650,
					BodyWood = mahoganyWood,
					FretboardWood = palisanderWood,
					FingerboardWood = palisanderWood,
					Color = blackColor,
					Description = gutiarDescription,
					Manufacturer = usaManufacturer,
					LargeMainImageUrl = virtualLargeMainImageFile,
					SmallMainImageUrl = virtualSmallMainImageFile,
					Price = 13100,
					IsClassical = false,
					Preamp = preamp1,
					Pickup = pickup1
				});

			currentId = 8;
			guitarModel = "Ibanez V72ECE Natural";
			guitarDirectory = Path.Combine(_sampleDataDirectory, guitarModel);
			gutiarDescription = GetGuitarDescription(guitarDirectory);

			PrepareGuitarImages(currentId, guitarDirectory, out virtualLargeMainImageFile, out virtualSmallMainImageFile);

			guitar = context.Guitars.Add(
				new SemiAcousticGuitar
				{
					Brand = ibanezBrand,
					Series = "V72ECE",
					Model = guitarModel,
					BodyType = jumboBodyType,
					FretsCount = 19,
					Scale = 650,
					BodyWood = mahoganyWood,
					FretboardWood = palisanderWood,
					FingerboardWood = palisanderWood,
					Color = naturalColor,
					Description = gutiarDescription,
					Manufacturer = usaManufacturer,
					LargeMainImageUrl = virtualLargeMainImageFile,
					SmallMainImageUrl = virtualSmallMainImageFile,
					Price = 13100,
					IsClassical = false,
					Preamp = preamp1,
					Pickup = pickup1
				});

			currentId = 9;
			guitarModel = "Ibanez GART60 Black Night";
			guitarDirectory = Path.Combine(_sampleDataDirectory, guitarModel);
			gutiarDescription = GetGuitarDescription(guitarDirectory);

			PrepareGuitarImages(currentId, guitarDirectory, out virtualLargeMainImageFile, out virtualSmallMainImageFile);

			guitar = context.Guitars.Add(
				new ElectricGuitar
				{
					Brand = ibanezBrand,
					Series = "GART60",
					Model = guitarModel,
					FretsCount = 19,
					Scale = 650,
					BodyWood = mahoganyWood,
					FretboardWood = palisanderWood,
					FingerboardWood = palisanderWood,
					Color = blackColor,
					Description = gutiarDescription,
					Manufacturer = usaManufacturer,
					LargeMainImageUrl = virtualLargeMainImageFile,
					SmallMainImageUrl = virtualSmallMainImageFile,
					Price = 7965,
				});
		}
		#endregion
	}
}