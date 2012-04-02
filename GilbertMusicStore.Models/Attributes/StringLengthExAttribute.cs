using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GilbertMusicStore.Models.Attributes
{
	public class StringLengthExAttribute : StringLengthAttribute
	{
		#region Constructors

		public StringLengthExAttribute(int maximumLength)
			: base(maximumLength)
		{
			DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(StringLengthExAttribute), typeof(StringLengthAttributeAdapter));
		}
		#endregion

		#region Methods

		public override string FormatErrorMessage(string name)
		{
			if (MinimumLength == 0)
			{
				return string.Format(Properties.Resources.StringLengthMaxErrorMessage, name, MaximumLength);
			}
			else
			{
				return string.Format(Properties.Resources.StringLengthBothErrorMessage, name, MinimumLength);
			}
		}
		#endregion
	}
}
