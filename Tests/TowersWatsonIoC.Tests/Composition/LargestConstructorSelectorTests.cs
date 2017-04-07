// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using TowersWatsonIoC.Composition;
using Xunit;

namespace TowersWatsonIoC.Tests.Composition
{
    public class LargestConstructorSelectorTests
	{
		[Fact]
		public void SelectConstructor()
		{
			var constructorSelector = new LargestConstructorSelector();
			var selectedConstructor = constructorSelector.SelectConstructor(typeof(ConstructorSelectionTestComponent));

			Assert.Equal(3, selectedConstructor.GetParameters().Length);
		}

		[ExcludeFromCodeCoverage]
		public class ConstructorSelectionTestComponent
		{
			public ConstructorSelectionTestComponent()
			{

			}

			public ConstructorSelectionTestComponent(ITestComponent z)
			{

			}

			public ConstructorSelectionTestComponent(string x, string y, ITestComponent z)
			{

			}
		}
	}
}