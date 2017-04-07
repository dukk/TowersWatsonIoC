// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System;
using TowersWatsonIoC.Components;
using Xunit;

namespace TowersWatsonIoC.Tests.Components
{
    public class StaticComponentTests : ContainerComponentTests
    {
		[Fact]
		public void Compose()
		{
			var staticComponent = new TestComponent();
			var component = new StaticComponent<ITestComponent>(staticComponent);
			var composeResult = component.Compose(this.Composer, true, this.ConstructorSelector);

			Assert.Equal(staticComponent, composeResult);
		}

		[Fact]
		public void Dispose()
		{
			var staticComponent = new TestComponent();
			var component = new StaticComponent<ITestComponent>(staticComponent);

			component.Dispose();

			Assert.Throws<ObjectDisposedException>(
				() => component.Compose(this.Composer, true, this.ConstructorSelector));
		}
	}
}