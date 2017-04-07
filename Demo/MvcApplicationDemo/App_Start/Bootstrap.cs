// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using MvcApplicationDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TowersWatsonIoC;

namespace MvcApplicationDemo
{
    internal static class Bootstrap
    {
        private static IComponentContainer container = new ComponentContainer();

        public static void Setup()
        {
            Bootstrap.registerComponents(container);

            ControllerBuilder.Current.SetControllerFactory(new ComponentContainerControllerFactory(container));
        }

        private static void registerComponents(IComponentContainer container)
        {
            container.Register<IServerVariablesProvider>().To<ServerVariablesProvider>().AsSingleton();
        }
    }
}