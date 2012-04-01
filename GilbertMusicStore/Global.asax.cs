using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using GilbertMusicStore.Models;
using System.Web.Security;

namespace GilbertMusicStore
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Store", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);

		}

        public static void AddAdmin()
        {
            // Если нет в системе роли admin, создаём её
            if (!Roles.RoleExists("admin"))
                Roles.CreateRole("admin");

            // Если нет в системе пользователя admin, создаём его
            if (Membership.GetUser("admin") == null)
            {
                MembershipCreateStatus status = MembershipCreateStatus.Success;
                Membership.CreateUser("admin", "temp_pass", "temp@temp.com", "2*2", "4", true, out status);
            }

            // Если у пользователя admin нет роли admin, присваиваем ему эту роль
            if (!Roles.IsUserInRole("admin", "admin"))
                Roles.AddUserToRole("admin", "admin");
        }

		protected void Application_Start()
		{
			Database.SetInitializer(new MusicStoreInitializer());

			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            AddAdmin();
		}
	}
}