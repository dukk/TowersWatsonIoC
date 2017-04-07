// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System;
using TowersWatsonIoC.Composition;

namespace TowersWatsonIoC.Components
{
    public class TransientComponent<TType, TImplementation> : IContainerComponent<TType>
		where TImplementation : class, TType
	{
		private bool disposed = false;

        public TransientComponent()
        {
            if (typeof(TImplementation).IsAbstract)
                throw new ArgumentException($"Generic argument '{nameof(TImplementation)}' specified an abstract class that can not be constructed. Check your component registrations, you have an invalid implementation type.", nameof(TImplementation));
        }

        public bool IsCompositionPreparationSupported { get; private set; } = true;

		public void Dispose()
		{
			this.disposed = true; // Dispose won't buy anything with this component type, so it's faked.
		}

		public TType Compose(IComponentComposer composer, bool throwOnUnknown, IConstructorSelector constructorSelector)
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.GetType().Name);

            if (null == composer)
                throw new ArgumentNullException(nameof(composer));

            if (null == constructorSelector)
                throw new ArgumentNullException(nameof(constructorSelector));

            return (TType)composer.ComposeUsingConstructor(typeof(TImplementation), throwOnUnknown, constructorSelector);
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