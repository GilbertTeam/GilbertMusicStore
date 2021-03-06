﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GilbertMusicStore.Models.Attributes
{
	public class RequiredExAttribute : RequiredAttribute
	{
		#region Methods

		public override string FormatErrorMessage(string name)
		{
			return string.Format(Properties.Resources.RequiredErrorMessage, name);
		}

		#endregion
	}
}