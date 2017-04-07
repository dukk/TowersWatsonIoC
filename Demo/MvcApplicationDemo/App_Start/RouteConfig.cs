﻿// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcApplicationDemo
{
	public static class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapMvcAttributeRoutes();

			//routes.MapRoute(
			//    name: "Default",
			//    url: "{controller}/{action}/{id}",
			//    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			//);
		}
	}
}