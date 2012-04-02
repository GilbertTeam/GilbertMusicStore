using System;
using System.ComponentModel.DataAnnotations;
using GilbertMusicStore.Models.Attributes;

namespace GilbertMusicStore.Models
{
	public class News
	{
		[ScaffoldColumn(false)]
		public int Id { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "Дата")]
		public DateTime Date { get; set; }

		[RequiredEx]
		[MaxLength]
		[Display(Name = "Заголовок")]
		public string Title { get; set; }

		[RequiredEx]
		[MaxLength]
		[Display(Name = "Содержание")]
		public string Content { get; set; }
	}
}