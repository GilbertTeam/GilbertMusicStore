using System.ComponentModel.DataAnnotations;
using GilbertMusicStore.Models.Attributes;

namespace GilbertMusicStore.Models
{
	public class Wood
	{
		[ScaffoldColumn(false)]
		public int WoodId { get; set; }

		[RequiredEx]
		[StringLengthEx(50)]
		public string Name { get; set; }
	}
}