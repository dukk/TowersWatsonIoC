// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Web.Mvc;
using System.Web.Routing;
using TowersWatsonIoC;

namespace MvcApplicationDemo
{
    public class ComponentContainerControllerFactory : DefaultControllerFactory
    {
        public ComponentContainerControllerFactory(IComponentContainer container)
        {
            if (null == container)
                throw new ArgumentNullException(nameof(container));

            this.Container = container;
        }

        protected IComponentContainer Container { get; private set; }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return (IController)this.Container.Compose(controllerType);
        }
    }
}