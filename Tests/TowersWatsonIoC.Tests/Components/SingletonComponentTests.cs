﻿// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using Moq;
using System;
using TowersWatsonIoC.Components;
using TowersWatsonIoC.Composition;
using Xunit;

namespace TowersWatsonIoC.Tests.Components
{
	public class SingletonComponentTests : ContainerComponentTests
    {
        [Fact]
        public void ConstructWithAbstractImplementation()
        {
            Assert.Throws<ArgumentException>(() =>
                new SingletonComponent<ITestComponent, AbstractTestComponent>());
        }

        [Fact]
		public void Compose()
		{
            var component = new SingletonComponent<ITestComponent, TestComponent>();
            var composeResultA = component.Compose(this.Composer, false, this.ConstructorSelector);
            var composeResultB = component.Compose(this.Composer, false, this.ConstructorSelector);

            Assert.Equal(composeResultA, composeResultB);
		}

		[Fact]
		public void Dispose()
		{
			var component = new SingletonComponent<ITestComponent, TestComponent>();

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
                    var component = new SingletonComponent<ITestComponent, TestComponent>();

                    component.Compose(null, false, this.ConstructorSelector);
                });
        }

        [Fact]
        public void PrepareCompositionWithNullComposer()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    var component = new SingletonComponent<ITestComponent, TestComponent>();

                    component.PrepareComposition(null, this.ConstructorSelector);
                });
        }

        [Fact]
        public void PrepareCompositionWithNullConstructorSelector()
        {
            Assert.Throws<ArgumentNullException>(
               () =>
               {
                   var component = new SingletonComponent<ITestComponent, TestComponent>();

                   component.PrepareComposition(this.Composer, null);
               });
        }

        [Fact]
        public void PrepareComposition()
        {
            var composer = Mock.Of<IComponentComposer>();
            var component = new SingletonComponent<ITestComponent, TestComponent>();

            component.PrepareComposition(composer, this.ConstructorSelector);

            var mockComposer = Mock.Get(composer);

            mockComposer.Verify(
                c => c.PrepareToComposeUsingConstructor(typeof(TestComponent), this.ConstructorSelector),
                Times.Once);
        }
    }
}