// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System;
using TowersWatsonIoC.Composition;

namespace TowersWatsonIoC.Components
{
    public class TransientComponent<TComponent, TImplementation> : ContainerComponenet<TComponent>
		where TImplementation : class, TComponent
	{
        public TransientComponent()
            : base(typeof(TImplementation))
        {

        }

		public override TComponent Compose(IComponentComposer composer, bool throwOnUnknown, IConstructorSelector constructorSelector)
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.GetType().Name);

            if (null == composer)
                throw new ArgumentNullException(nameof(composer));

            if (null == constructorSelector)
                throw new ArgumentNullException(nameof(constructorSelector));

            return (TComponent)composer.ComposeUsingConstructor(typeof(TImplementation), throwOnUnknown, constructorSelector);
		}

		public override void PrepareComposition(IComponentComposer composer, IConstructorSelector constructorSelector)
		{
            if (null == composer)
                throw new ArgumentNullException(nameof(composer));

            if (null == constructorSelector)
                throw new ArgumentNullException(nameof(constructorSelector));

            composer.PrepareToComposeUsingConstructor(typeof(TImplementation), constructorSelector);
		}
    }
}