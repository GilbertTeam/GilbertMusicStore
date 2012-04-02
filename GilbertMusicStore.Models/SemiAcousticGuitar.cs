using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using GilbertMusicStore.Models.Attributes;

namespace GilbertMusicStore.Models
{
	public class SemiAcousticGuitar : AcousticGuitar
	{
		[ScaffoldColumn(false)]
		public int PreampId { get; set; }

		[ScaffoldColumn(false)]
		public int PickupId { get; set; }

		[RequiredEx]
		[Display(Name = "Предусилитель")]
		public virtual Preamp Preamp { get; set; }

		[RequiredEx]
		[Display(Name = "Звукосниматель")]
		public virtual Pickup Pickup { get; set; }
	}
}