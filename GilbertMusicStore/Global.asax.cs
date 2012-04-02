using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using GilbertMusicStore.Models;

namespace GilbertMusicStore
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		protected void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default",
				"{controller}/{action}/{id}",
				new { controller = "News", action = "Index", id = UrlParameter.Optional }
			);
		}

		protected void Application_Start()
		{
			Database.SetInitializer(new MusicStoreInitializer());

			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}
	}
}