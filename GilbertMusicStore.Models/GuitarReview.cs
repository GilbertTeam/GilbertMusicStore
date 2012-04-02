using System;
using System.ComponentModel.DataAnnotations;
using GilbertMusicStore.Models.Attributes;

namespace GilbertMusicStore.Models
{
	public class GuitarReview
	{
		[ScaffoldColumn(false)]
		public int Id { get; set; }

		[ScaffoldColumn(false)]
		public int GuitarId { get; set; }

		[ScaffoldColumn(false)]
		public virtual Guitar Guitar { get; set; }

		[RequiredEx]
		[MaxLength]
		[Display(Name = "Содержание")]
		public string Content { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "Дата")]
		public DateTime DateCreated { get; set; }
	}
}