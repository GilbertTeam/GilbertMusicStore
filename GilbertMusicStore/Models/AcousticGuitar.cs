using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GilbertMusicStore.Models
{
	public class AcousticGuitar : Guitar
	{
		public int BodyTypeId { get; set; }

		public bool IsClassical { get; set; }

		public virtual BodyType BodyType { get; set; }
	}
}