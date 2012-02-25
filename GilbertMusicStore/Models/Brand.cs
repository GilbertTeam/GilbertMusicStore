using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GilbertMusicStore.Models
{
	public class Brand
	{
		public int Id { get; set; }

		//[Required(AllowEmptyStrings = false, 
		public string Name { get; set; }
	}
}