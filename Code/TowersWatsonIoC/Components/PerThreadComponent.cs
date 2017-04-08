// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using TowersWatsonIoC.Composition;

namespace TowersWatsonIoC.Components
{
    public class PerThreadComponent<TComponent, TImplementation> : ContainerComponenet<TImplementation>
        where TImplementation : class, TComponent
    {
		private readonly ThreadLocal<TImplementation> threadLocalInstance;

		public PerThreadComponent()
            : base(typeof(TImplementation))
		{
            this.threadLocalInstance = new ThreadLocal<TImplementation>(false);
		}

        public override TImplementation Compose(IComponentComposer composer, bool throwOnUnknown,
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

		public override void PrepareComposition(IComponentComposer composer, IConstructorSelector constructorSelector)
		{
            if (null == composer)
                throw new ArgumentNullException(nameof(composer));

            if (null == constructorSelector)
                throw new ArgumentNullException(nameof(constructorSelector));

            composer.PrepareToComposeUsingConstructor(typeof(TImplementation), constructorSelector);
		}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.threadLocalInstance.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}