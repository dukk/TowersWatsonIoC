// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using ConsoleApplicationDemo.Controllers;
using System;

namespace ConsoleApplicationDemo
{
    internal class Program
	{
		internal static void Main(string[] args)
		{
			try
			{
				IServiceProvider serviceProvider = Bootstrapper.GetProvider();
				IApplicationController applicationController = serviceProvider.GetService(typeof(IApplicationController)) as IApplicationController;

				if (null == applicationController)
					throw new ApplicationException("Application controller is missing.");

				applicationController.Run();
			}
			catch (Exception exception)
			{
				Console.Write(exception.Message);
			}
		}
	}
}