using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using GilbertMusicStore.Models.Attributes;

namespace GilbertMusicStore.Models
{
	public abstract class Guitar
	{
		#region Properties

		[ScaffoldColumn(false)]
		public int Id { get; set; }

		#region Foreign Keys

		[ScaffoldColumn(false)]
		public int BrandId { get; set; }

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
		#endregion

		#region Navigation Properties

		[Display(Name = "Бренд")]
		public virtual Brand Brand { get; set; }

		[Display(Name = "Производитель")]
		public Manufacturer Manufacturer { get; set; }

		[Display(Name = "Материал корпуса")]
		public Wood BodyWood { get; set; }

		[Display(Name = "Материал грифа")]
		public Wood FretboardWood { get; set; }

		[Display(Name = "Материал накладки")]
		public Wood FingerboardWood { get; set; }

		[Display(Name = "Цвет")]
		public Color Color { get; set; }

		[ForeignKey("Id")]
		public ICollection<RelatedGuitar> RelatedGuitars { get; set; }
		#endregion

		[RequiredEx]
		[StringLengthEx(50)]
		[Display(Name = "Серия")]
		public string Series { get; set; }

		[RequiredEx]
		[StringLengthEx(50)]
		[Display(Name = "Модель")]
		public string Model { get; set; }

		[Range(15, 25)]
		[Display(Name = "Количество ладов")]
		public int FretsCount { get; set; }

		[Range(650.0f, 690.0f)]
		[Display(Name = "Мензура")]
		public float Scale { get; set; }

		[MaxLength]
		[Display(Name = "Описание")]
		public string Description { get; set; }

		[Display(Name = "Дополнительная информация")]
		public string AdditionalInfo { get; set; }

		[ScaffoldColumn(false)]
		public string LargeMainImageUrl { get; set; }

		[ScaffoldColumn(false)]
		public string SmallMainImageUrl { get; set; }

		//public string UserImage1Url { get; set; }

		//public string UserImage2Url { get; set; }

		//public string UserImage3Url { get; set; }

		//public string UserImage4Url { get; set; }

		[RequiredEx]
		[DataType(DataType.Currency)]
		[Display(Name = "Цена")]
		public decimal Price { get; set; }
		#endregion

		#region Constructors

		public Guitar()
		{
			RelatedGuitars = new HashSet<RelatedGuitar>();
		}
		#endregion
	}
}