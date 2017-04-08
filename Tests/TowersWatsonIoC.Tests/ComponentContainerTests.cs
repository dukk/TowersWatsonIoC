// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System;
using TowersWatsonIoC.Components;
using Xunit;

namespace TowersWatsonIoC.Tests
{
    public class ComponentContainerTests
	{
		public ComponentContainer Container = new ComponentContainer();

		[Fact]
		public void Disposed()
		{
			var container = new ComponentContainer();

            container.AddRegisteredComponent<ITestComponent>(new StaticComponent<ITestComponent>(new TestComponent()));

            container.AddRegisteredComponent<ITestComponent>(new TransientComponent<ITestComponent, TestComponent>());

            container.AddRegisteredComponent<ITestComponent>(new SingletonComponent<ITestComponent, TestComponent>());

            container.AddRegisteredComponent<ITestComponent>(new PerThreadComponent<ITestComponent, TestComponent>());

            container.Register<ITestComponent>().To<TestComponent>();

			container.Dispose();
			container.Dispose(); // Get that one extra return if you dispose twice...

			Assert.Throws<ObjectDisposedException>(() => container.Compose<ITestComponent>());
			Assert.Throws<ObjectDisposedException>(() => container.Compose(typeof(ITestComponent)));
			Assert.Throws<ObjectDisposedException>(() => container.Register<ITestComponent>().To<TestComponent>());
			Assert.Throws<ObjectDisposedException>(() => container.Unregister<ITestComponent>());
			Assert.Throws<ObjectDisposedException>(() => container.ReplaceRegisteredComponent<ITestComponent>(null));
			Assert.Throws<ObjectDisposedException>(() => container.GetRegisteredComponent<ITestComponent>());
		}

		[Fact]
		public void RegistertWithNullComponent()
		{
			Assert.Throws<ArgumentNullException>(
				() => this.Container.AddRegisteredComponent<ITestComponent>(null));
		}

		[Fact]
		public void RegisterTheSameComponentTwice()
		{
			this.Container.Register<ITestComponent>().To<TestComponent>();

			Assert.Throws<ArgumentException>(
				() => this.Container.Register<ITestComponent>().To<TestComponent>());
		}

		[Fact]
		public void Static()
		{
			var composedInstance = new TestComponent();

			this.Container.Register<ITestComponent>().To(composedInstance);

			var component = this.Container.GetRegisteredComponent<ITestComponent>();
			var composedInstanceA = this.Container.Compose<ITestComponent>();
			var composedInstanceB = this.Container.Compose<ITestComponent>();

			Assert.IsType<StaticComponent<ITestComponent>>(component);
			Assert.Equal(composedInstance, composedInstanceA);
			Assert.Equal(composedInstance, composedInstanceB);

			this.cleanupComponentRegistration<ITestComponent>();
		}

        [Fact]
        public void Singleton()
        {
            this.Container.Register<ITestComponent>().To<TestComponent>().AsSingleton();

            var component = this.Container.GetRegisteredComponent<ITestComponent>();
            var composedInstanceA = this.Container.Compose<ITestComponent>();
            var composedInstanceB = this.Container.Compose<ITestComponent>();

            Assert.IsType<SingletonComponent<ITestComponent, TestComponent>>(component);
            Assert.IsType<TestComponent>(composedInstanceA);
            Assert.IsType<TestComponent>(composedInstanceB);
            Assert.Equal(composedInstanceA, composedInstanceB);

            this.cleanupComponentRegistration<ITestComponent>();
        }

        [Fact]
		public void Transient()
		{
			this.Container.Register<ITestComponent>().To<TestComponent>();

			var component = this.Container.GetRegisteredComponent<ITestComponent>();
			var composedInstanceA = this.Container.Compose<ITestComponent>();
			var composedInstanceB = this.Container.Compose<ITestComponent>();

			Assert.IsType<TransientComponent<ITestComponent, TestComponent>>(component);
			Assert.IsType<TestComponent>(composedInstanceA);
			Assert.IsType<TestComponent>(composedInstanceB);
			Assert.NotEqual(composedInstanceA, composedInstanceB);

			this.cleanupComponentRegistration<ITestComponent>();
		}

		[Fact]
		public void PerThread()
		{
			this.Container.Register<ITestComponent>().To<TestComponent>().AsSingleton().PerThread();

			var component = this.Container.GetRegisteredComponent<ITestComponent>();
			var composedInstanceA = this.Container.Compose<ITestComponent>();
			var composedInstanceB = this.Container.Compose<ITestComponent>();

			Assert.IsType<PerThreadComponent<ITestComponent, TestComponent>>(component);
			Assert.IsType<TestComponent>(composedInstanceA);
			Assert.IsType<TestComponent>(composedInstanceB);
			Assert.Equal(composedInstanceA, composedInstanceB);

			this.cleanupComponentRegistration<ITestComponent>();
		}

		[Fact]
		public void ServiceProvider()
		{
			this.Container.Register<ITestComponent>().To<TestComponent>();

			var serviceProvider = this.Container as IServiceProvider;
			var component = serviceProvider.GetService(typeof(ITestComponent));

			Assert.IsType<TestComponent>(component);

			this.cleanupComponentRegistration<ITestComponent>();
		}

		[Fact]
		public void ServiceProvider_NullServiceType()
		{
			var serviceProvider = this.Container as IServiceProvider;

			Assert.Throws<ArgumentNullException>(() => serviceProvider.GetService(null));
		}

		private void cleanupComponentRegistration<T>()
		{
			this.Container.Unregister<T>();

			var stillRegistered = this.Container.HasRegisteredComponent<T>();

			Assert.False(stillRegistered);
		}
	}
}