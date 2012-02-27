using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GilbertMusicStore.Models
{
	public class SemiAcousticGuitar : AcousticGuitar
	{
		public int PreampId { get; set; }
		public int PickupId { get; set; }

		public virtual Preamp Preamp { get; set; }
		public virtual Pickup Pickup { get; set; }
	}
}