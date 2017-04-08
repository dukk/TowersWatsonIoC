// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System;
using TowersWatsonIoC.Composition;

namespace TowersWatsonIoC.Components
{
    public class StaticComponent<TImplementation> : ContainerComponenet<TImplementation>
    {
		private readonly TImplementation staticInstance;

		public StaticComponent(TImplementation staticInstance)
            : base(staticInstance.GetType())
		{
			this.staticInstance = staticInstance;
		}

		public override TImplementation Compose(IComponentComposer composer, bool throwOnUnknown, IConstructorSelector constructorSelector)
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.GetType().Name);

            if (null == composer)
                throw new ArgumentNullException(nameof(composer));

            if (null == constructorSelector)
                throw new ArgumentNullException(nameof(constructorSelector));

            return this.staticInstance;
		}
    }
}