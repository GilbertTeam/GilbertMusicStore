using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GilbertMusicStore.Models
{
	public abstract class Guitar
	{
		[ScaffoldColumn(false)]
		public int Id { get; set; }

		[ScaffoldColumn(false)]
		public int BrandId { get; set; }

		[Required]
		[MinLength(5)]
		[MaxLength(50)]
		public string Series { get; set; }

		[Required]
		[MaxLength(50)]
		public string Model { get; set; }

		[Range(15, 25)]
		public int FretsCount { get; set; }

		[Range(650.0f, 690.0f)]
		public float Scale { get; set; }

		[ScaffoldColumn(false)]
		public int BodyWoodId { get; set; }

		[ScaffoldColumn(false)]
		public int FretboardWoodId { get; set; }

		[ScaffoldColumn(false)]
		public int FingerboardWoodId { get; set; }

		[ScaffoldColumn(false)]
		public int ColorId { get; set; }

		[ScaffoldColumn(false)]
		public int ManufacturerId { get; set; }

		[MaxLength]
		public string Description { get; set; }

		public string AdditionalInfo { get; set; }

		public string LargeMainImageUrl { get; set; }

		public string SmallMainImageUrl { get; set; }

		//public string UserImage1Url { get; set; }

		//public string UserImage2Url { get; set; }

		//public string UserImage3Url { get; set; }

		//public string UserImage4Url { get; set; }

		[Required]
		[DataType(DataType.Currency)]
		public decimal Price { get; set; }

		#region Navigation Properties

		public virtual Brand Brand { get; set; }

		public Manufacturer Manufacturer { get; set; }

		public Wood BodyWood { get; set; }

		public Wood FretboardWood { get; set; }

		public Wood FingerboardWood { get; set; }

		public Color Color { get; set; }
		#endregion
	}
}