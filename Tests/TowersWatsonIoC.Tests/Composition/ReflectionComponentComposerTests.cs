// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System;
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

        [Fact]
        public void PrepareToComposeUsingConstructor()
        {
            var container = new ComponentContainer();
            var composer = new ReflectionComponentComposer(container);
            var constructorSelector = new LargestConstructorSelector();
            var componentType = typeof(TestComponent);

            composer.PrepareToComposeUsingConstructor(componentType, constructorSelector);

            var isPrepared = composer.IsPreparedToComposeUsingConstructor(componentType, constructorSelector);

            Assert.True(isPrepared);
        }

        [Fact]
        public void PrepareToComposeUsingConstructorWithNullComponentType()
        {
            var container = new ComponentContainer();
            var composer = new ReflectionComponentComposer(container);
            var constructorSelector = new LargestConstructorSelector();

            Assert.Throws<ArgumentNullException>(() =>
                composer.PrepareToComposeUsingConstructor(null, constructorSelector));
        }

        [Fact]
        public void PrepareToComposeUsingConstructorWithNullConstructorSelector()
        {
            var container = new ComponentContainer();
            var composer = new ReflectionComponentComposer(container);

            Assert.Throws<ArgumentNullException>(() =>
                composer.PrepareToComposeUsingConstructor(typeof(TestComponent), null));
        }
    }
}