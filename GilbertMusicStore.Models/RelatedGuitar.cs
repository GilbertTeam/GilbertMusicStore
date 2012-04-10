using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace GilbertMusicStore.Models
{
	public class RelatedGuitar
	{
		[Key, Column(Order = 0)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[ScaffoldColumn(false)]
		public int Id { get; set; }

		[Key, Column(Order = 1)]
		[ScaffoldColumn(false)]
		public int GuitarId { get; set; }

		public int Index { get; set; }

		[ForeignKey("GuitarId")]
		public virtual Guitar Guitar { get; set; }
	}
}
