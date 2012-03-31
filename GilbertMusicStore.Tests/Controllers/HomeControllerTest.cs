using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GilbertMusicStore;
using GilbertMusicStore.Controllers;

namespace GilbertMusicStore.Tests.Controllers
{
	[TestClass]
	public class HomeControllerTest
	{
		[TestMethod]
		public void Index()
		{
			// Arrange
			NewsController controller = new NewsController();

			// Act
			ViewResult result = controller.Index() as ViewResult;

			// Assert
			Assert.AreEqual("Welcome to ASP.NET MVC!", result.ViewBag.Message);
		}

		[TestMethod]
		public void About()
		{
			// Arrange
			NewsController controller = new NewsController();

			// Act
			ViewResult result = controller.About() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
		}
	}
}
