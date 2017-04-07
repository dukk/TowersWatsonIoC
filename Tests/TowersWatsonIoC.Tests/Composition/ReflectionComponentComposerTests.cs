// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using TowersWatsonIoC.Composition;
using Xunit;

namespace TowersWatsonIoC.Tests.Composition
{
    public class ReflectionComponentComposerTests
	{
		[Fact]
		public void ComposeUsingConstructor()
		{
			var container = new ComponentContainer();
			var composer = new ReflectionComponentComposer(container);
            var constructorSelector = new LargestConstructorSelector();
			var component = composer.ComposeUsingConstructor(typeof(TestComponent), true, constructorSelector);

			Assert.IsType<TestComponent>(component);
		}
	}
}