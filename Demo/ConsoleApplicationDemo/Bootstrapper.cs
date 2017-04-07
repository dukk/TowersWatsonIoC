// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using ConsoleApplicationDemo.Controllers;
using ConsoleApplicationDemo.Services;
using System;
using TowersWatsonIoC;

namespace ConsoleApplicationDemo
{
    internal static class Bootstrapper
    {
        private static readonly object syncRoot = new object();
        private static ComponentContainer container;

        public static IServiceProvider GetProvider()
        {
            if (null == Bootstrapper.container)
            {
                lock (Bootstrapper.syncRoot)
                {
                    if (null == Bootstrapper.container)
                    {
                        Bootstrapper.container = new ComponentContainer();

                        Bootstrapper.Register(container);
                    }
                }
            }

            return container;
        }

        private static void Register(ComponentContainer container)
        {
            // This is where you compose the application.
            container.Register<IApplicationController>().To<DemoApplicationController>();
            container.Register<IApplicationService>().To<DemoApplicationService>().AsSingleton();
        }
    }
}