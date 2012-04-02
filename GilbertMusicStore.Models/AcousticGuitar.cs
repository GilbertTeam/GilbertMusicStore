using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GilbertMusicStore.Models
{
	public class AcousticGuitar : Guitar
	{
		[ScaffoldColumn(false)]
		public int BodyTypeId { get; set; }

		public bool IsClassical { get; set; }

		public virtual BodyType BodyType { get; set; }
	}
}