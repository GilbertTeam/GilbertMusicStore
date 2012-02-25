using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GilbertMusicStore.Models
{
	public abstract class Guitar
	{
		public int Id { get; set; }

		public int BrandId { get; set; }

		public virtual Brand Brand { get; set; }

		public string Series { get; set; }

		public string Model { get; set; }

		public int FretsCount { get; set; }

		public float Scale { get; set; }

		public int BodyWoodId { get; set; }

		public int FretboardWoodId { get; set; }

		public int FingerboardWoodId { get; set; }

		public int ColorId { get; set; }

		public int ManufacturerId { get; set; }

		[MaxLength]
		public string Description { get; set; }

		public string AdditionalInfo { get; set; }

		[Column(TypeName = "image")]
		public byte[] MainImage { get; set; }

		[Column(TypeName = "image")]
		public byte[] UserImage1 { get; set; }

		[Column(TypeName = "image")]
		public byte[] UserImage2 { get; set; }

		[Column(TypeName = "image")]
		public byte[] UserImage3 { get; set; }

		[Column(TypeName = "image")]
		public byte[] UserImage4 { get; set; }

		public decimal Price { get; set; }

		#region Navigation Properties

		public Wood BodyWood { get; set; }

		public Wood FretboardWood { get; set; }

		public Wood FingerboardWood { get; set; }

		public Color Color { get; set; }

		public Manufacturer Manufacturer { get; set; }
		#endregion
	}
}