// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using TowersWatsonIoC.Composition;

namespace TowersWatsonIoC.Components
{
    public class PerThreadComponent<TType, TImplementation> : IContainerComponent<TImplementation>
        where TImplementation : class, TType
    {
		private bool disposed = false;
		private readonly ThreadLocal<TImplementation> threadLocalInstance;

		public PerThreadComponent()
		{
            if (typeof(TImplementation).IsAbstract)
                throw new ArgumentException($"Generic argument '{nameof(TImplementation)}' specified an abstract class that can not be constructed. Check your component registrations, you have an invalid implementation type.", nameof(TImplementation));

            this.threadLocalInstance = new ThreadLocal<TImplementation>(false);
		}

        [ExcludeFromCodeCoverage]
        ~PerThreadComponent()
        {
            this.Dispose(false);
            GC.SuppressFinalize(this);
        }

        public bool IsCompositionPreparationSupported { get; private set; } = true;

        public void Dispose()
		{
			this.Dispose(true);
		}

		protected void Dispose(bool disposing)
		{
			if (disposed)
				return;

			if (disposing)
			{
				threadLocalInstance.Dispose();
			}

			this.disposed = true;
		}

        public TImplementation Compose(IComponentComposer composer, bool throwOnUnknown,
            IConstructorSelector constructorSelector)
        {
			if (disposed)
				throw new ObjectDisposedException(this.GetType().Name);

            if (null == composer)
                throw new ArgumentNullException(nameof(composer));

            if (null == constructorSelector)
                throw new ArgumentNullException(nameof(constructorSelector));

            if (null == threadLocalInstance.Value)
                threadLocalInstance.Value = (TImplementation)composer.ComposeUsingConstructor(typeof(TImplementation), throwOnUnknown, constructorSelector);


            return threadLocalInstance.Value;
		}

		public void PrepareComposition(IComponentComposer composer, IConstructorSelector constructorSelector)
		{
            if (null == composer)
                throw new ArgumentNullException(nameof(composer));

            if (null == constructorSelector)
                throw new ArgumentNullException(nameof(constructorSelector));

            composer.PrepareToComposeUsingConstructor(typeof(TImplementation), constructorSelector);
		}

        object IContainerComponent.Compose(IComponentComposer composer,
            bool throwOnUnknown, IConstructorSelector constructorSelector)
        {
            return this.Compose(composer, throwOnUnknown, constructorSelector);
        }
    }
}