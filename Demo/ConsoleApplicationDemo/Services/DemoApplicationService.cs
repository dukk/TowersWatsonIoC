// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

namespace ConsoleApplicationDemo.Services
{
    internal class DemoApplicationService : IApplicationService
    {
        public string GetText()
        {
            return "Application is running... Press enter to exit.";
        }
    }
}