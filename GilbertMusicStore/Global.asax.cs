﻿using System.Data.Entity;
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
				System.Diagnostics.Trace.WriteLine(string.Format("Gilbert Music Store. End of session. Count of related guitars: {0}", guitarList.Count));

				using (MusicStoreContext context = new MusicStoreContext())
				{
					foreach (int guitarId in guitarList)
					{
						Guitar guitar = context.Guitars.Find(guitarId);

						if (guitar != null)
						{
							foreach (int relatedGuitarId in guitarList.SkipWhile(index => index == guitarId))
							{
								RelatedGuitar relatedGuitar = guitar.RelatedGuitars.SingleOrDefault(g => g.GuitarId == relatedGuitarId);

								if (relatedGuitar != null)
								{
									relatedGuitar.Index++;
								}
								else
								{
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

					int addedGuitars = context.SaveChanges();
					System.Diagnostics.Trace.WriteLine(string.Format("Gilbert Music Store. End of session. {0} guitars was added to DB.", addedGuitars));
					System.Diagnostics.Trace.WriteLine(string.Format("Gilbert Music Store. End of session. Count of related guitars in DB: {0}", context.RelatedGuitars.Count()));
				}
			}
			Application.UnLock();
		}
	}
}