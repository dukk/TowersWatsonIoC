// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Diagnostics.CodeAnalysis;
using TowersWatsonIoC.Composition;

namespace TowersWatsonIoC.Components
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>The singleton instance is lazy loaded at the time of the first request to fulfill composition. 
    /// If at that time one or more of the components your implementation type depends on hasn't been added yet it may fail.
    /// </remarks>
    /// <typeparam name="T"></typeparam>
	public class SingletonComponent<TComponenet, TImplementation> : ContainerComponenet<TImplementation>
        where TImplementation : class, TComponenet
    {
        // TODO: May want to make two different singleton implementations using different locking mechanisums 
        // based on how often this object is expected to be used.

        private readonly Lazy<TImplementation> lazyInstance;

        public SingletonComponent()
            : base(typeof(TImplementation))
        {
            this.lazyInstance = new Lazy<TImplementation>(LazyCompose, true);

            this.IsCompositionPreparationSupported = true;
        }

        protected IComponentComposer Composer { get; set; }

        protected bool ThrowOnUnknown { get; set; }

        protected IConstructorSelector ConstructorSelector { get; set; }

		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
				return;

			if (disposing && this.lazyInstance.IsValueCreated)
			{
				(this.lazyInstance.Value as IDisposable)?.Dispose();
			}

            base.Dispose(disposing);
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

            if (!this.lazyInstance.IsValueCreated)
            {
                this.Composer = composer;
                this.ThrowOnUnknown = throwOnUnknown;
                this.ConstructorSelector = constructorSelector;
            }

            return this.lazyInstance.Value;
		}

		public override void PrepareComposition(IComponentComposer composer, IConstructorSelector constructorSelector)
		{
            if (null == composer)
                throw new ArgumentNullException(nameof(composer));

            if (null == constructorSelector)
                throw new ArgumentNullException(nameof(constructorSelector));

            composer.PrepareToComposeUsingConstructor(typeof(TImplementation), constructorSelector);
		}

        protected virtual TImplementation LazyCompose()
        {
            // Had to pass the arguments in via protected properties because they can't be passed the Lazy types Func<out T>.
            // Because of that the state needs to be validated here.
            if (null == this.Composer || null == this.ConstructorSelector)
                throw new InvalidOperationException($"The {nameof(LazyCompose)} method was called when the object was in an invalid state. " +
                    $"The {nameof(Composer)}, {nameof(ThrowOnUnknown)}, and {nameof(ConstructorSelector)} properties must be set prior to the {nameof(LazyCompose)} method being called.");

            return (TImplementation)this.Composer.ComposeUsingConstructor(typeof(TImplementation), this.ThrowOnUnknown, this.ConstructorSelector);
        } 
    }
}