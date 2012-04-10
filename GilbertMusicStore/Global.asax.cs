using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using GilbertMusicStore.Models;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GilbertMusicStore
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : HttpApplication
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
			Database.SetInitializer(new MusicStoreInitializer(HttpContext.Current.Server.MapPath("~")));

			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}

		protected void Session_Start(object sender, EventArgs e)
		{
			System.Diagnostics.Trace.WriteLine("Gilbert Music Store. New session started.");
			Application.Lock();
			Application["GuitarList"] = new GuitarList();
			Application.UnLock();
		}

		protected void Session_End(object sender, EventArgs e)
		{
			Application.Lock();

			GuitarList guitarList = (GuitarList)Application["GuitarList"];

			if (guitarList != null &&
				guitarList.Count > 0)
			{
				System.Diagnostics.Trace.WriteLine(string.Format("Gilbert Music Store. End of session. Count of related guitars: {0}.", guitarList.Count));

				using (MusicStoreContext context = new MusicStoreContext())
				{
					context.RelatedGuitars.Load();

					foreach (int guitarId in guitarList)
					{
						Guitar guitar = context.Guitars.Find(guitarId);

						if (guitar != null)
						{
							var relatedGuitars = guitarList.Where(index => index != guitarId);

							foreach (int relatedGuitarId in relatedGuitars)
							{
								RelatedGuitar relatedGuitar = guitar.RelatedGuitars.SingleOrDefault(g => g.GuitarId == relatedGuitarId);

								if (relatedGuitar != null)
								{
									System.Diagnostics.Trace.WriteLine(string.Format("Gilbert Music Store. End of session. Guitar: {0}. Increase index of related guitar: {1}.", guitarId, relatedGuitarId));

									relatedGuitar.Index++;
								}
								else
								{
									System.Diagnostics.Trace.WriteLine(string.Format("Gilbert Music Store. End of session. Guitar: {0}. Added new related guitar: {1}.", guitarId, relatedGuitarId));

									context.RelatedGuitars.Add(
										new RelatedGuitar
										{
											Id = guitarId,
											GuitarId = relatedGuitarId,
											Index = 1
										});
								}
							}
						}
					}

					int affectedGuitars = 0;

					try
					{
						affectedGuitars = context.SaveChanges();
					}
					catch (Exception exception)
					{
						System.Diagnostics.Trace.WriteLine(string.Format("Gilbert Music Store. End of session. Exception occured: {0}.", exception.Message));
					}

					System.Diagnostics.Trace.WriteLine(string.Format("Gilbert Music Store. End of session. Affected guitars: {0}.", affectedGuitars));
					System.Diagnostics.Trace.WriteLine(string.Format("Gilbert Music Store. End of session. Count of related guitars in DB: {0}.", context.RelatedGuitars.Count()));
				}
			}
			Application.UnLock();
		}
	}
}