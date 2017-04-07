// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System;
using TowersWatsonIoC.Composition;

namespace TowersWatsonIoC.Components
{
    public class StaticComponent<TImplementation> : IContainerComponent<TImplementation>
    {
		private bool disposed = false;
		private readonly TImplementation staticInstance;

		public StaticComponent(TImplementation staticInstance)
		{
			this.staticInstance = staticInstance;
		}

        public bool IsCompositionPreparationSupported { get; private set; } = false;

		public void Dispose()
		{
			// Because the static instance's life cycle isn't managed by the container, don't do anything here. 
			// Except fake that we're disposed so we act like all the other IComponentRegistration's.
			this.disposed = true;
		}

		public TImplementation Compose(IComponentComposer composer, bool throwOnUnknown, IConstructorSelector constructorSelector)
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.GetType().Name);

            if (null == composer)
                throw new ArgumentNullException(nameof(composer));

            if (null == constructorSelector)
                throw new ArgumentNullException(nameof(constructorSelector));

            return this.staticInstance;
		}

        public void PrepareComposition(IComponentComposer composer, IConstructorSelector constructorSelector)
        {
            throw new NotImplementedException();
		}

        object IContainerComponent.Compose(IComponentComposer composer,
            bool throwOnUnknown, IConstructorSelector constructorSelector)
        {
            return this.Compose(composer, throwOnUnknown, constructorSelector);
        }
    }
}