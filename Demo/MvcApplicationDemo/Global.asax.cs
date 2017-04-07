// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System.Web.Routing;

namespace MvcApplicationDemo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Bootstrap.Setup();
            //AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}