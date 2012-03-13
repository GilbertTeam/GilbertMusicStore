using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GilbertMusicStore.Models
{
	public class Wood
	{
		[ScaffoldColumn(false)]
		public int WoodId { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }
	}
}