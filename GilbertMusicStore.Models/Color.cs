using System.ComponentModel.DataAnnotations;
using GilbertMusicStore.Models.Attributes;

namespace GilbertMusicStore.Models
{
	public class Color
	{
		[ScaffoldColumn(false)]
		public int Id { get; set; }

		[RequiredEx]
		[StringLengthEx(50)]
		public string Name { get; set; }
	}
}