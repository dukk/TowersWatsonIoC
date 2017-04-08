// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Diagnostics.CodeAnalysis;
using TowersWatsonIoC.Composition;

namespace TowersWatsonIoC.Components
{
    public abstract class ContainerComponenet<TComponent> : IContainerComponent<TComponent>
    {
        protected bool disposed = false;

        public ContainerComponenet(Type instanceType) // Can't do instanceType as a generic because it could be dynamic, like in StaticComponent.
        {
            this.InstanceType = instanceType ?? throw new ArgumentNullException(nameof(InstanceType));

            if (this.InstanceType.IsAbstract)
                throw new ArgumentException($"The { nameof(instanceType) } specified, '{ instanceType.FullName }' is an abstract class that can not be constructed. There may be a bug in { this.GetType().FullName } causing this invalid state because it passed in an invalid type.", nameof(instanceType));
        }

        [ExcludeFromCodeCoverage]
        ~ContainerComponenet()
        {
            this.Dispose(false);
        }

        public Type InstanceType { get; protected set; }

        public bool IsCompositionPreparationSupported { get; protected set; } = false;

        public abstract TComponent Compose(IComponentComposer composer, bool throwOnUnknown, IConstructorSelector constructorSelector);

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void PrepareComposition(IComponentComposer composer, IConstructorSelector constructorSelector)
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
                return;

            this.disposed = true;
        }

        object IContainerComponent.Compose(IComponentComposer composer,
            bool throwOnUnknown, IConstructorSelector constructorSelector)
        {
            return this.Compose(composer, throwOnUnknown, constructorSelector);
        }
    }
}