// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System;
using TowersWatsonIoC.Components;
using Xunit;

namespace TowersWatsonIoC.Tests.Components
{
    public class TransientComponentTests : ContainerComponentTests
    {
		[Fact]
		public void Compose()
		{
			var component = new TransientComponent<ITestComponent, TestComponent>();
            var composeResultA = component.Compose(this.Composer, false, this.ConstructorSelector);
			var composeResultB = component.Compose(this.Composer, false, this.ConstructorSelector);

			Assert.NotEqual(composeResultA, composeResultB);
		}

		[Fact]
		public void Dispose()
		{
			var component = new TransientComponent<ITestComponent, TestComponent>();

            component.Dispose();

			Assert.Throws<ObjectDisposedException>(
				() => component.Compose(this.Composer, false, this.ConstructorSelector));
		}

        [Fact]
        public void ComposeWithNullComposer()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    var component = new TransientComponent<ITestComponent, TestComponent>();

                    component.Compose(null, false, this.ConstructorSelector);
                });
        }

        [Fact]
        public void ComposeWithNullConstructorSelector()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    var component = new TransientComponent<ITestComponent, TestComponent>();

                    component.Compose(this.Composer, false, null);
                });
        }
	}
}