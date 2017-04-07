// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using ConsoleApplicationDemo.Services;
using System;

namespace ConsoleApplicationDemo.Controllers
{
    internal class DemoApplicationController : IApplicationController
    {
        public DemoApplicationController(IApplicationService applicationService /* Add any dependancies here (forms, views, services, etc...) */)
        {
            this.ApplicationService = applicationService ?? throw new NullReferenceException(nameof(applicationService));
        }

        protected IApplicationService ApplicationService { get; private set; }

        public void Run()
        {
            Console.WriteLine(this.ApplicationService.GetText());
            Console.ReadLine();
        }
    }
}