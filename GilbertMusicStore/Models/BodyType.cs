using System.ComponentModel.DataAnnotations;

namespace GilbertMusicStore.Models
{
	public class BodyType
	{
		[ScaffoldColumn(false)]
		public int Id { get; set; }

		[MaxLength(50)]
		public string Name { get; set; }
	}
}