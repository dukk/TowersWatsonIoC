// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using TowersWatsonIoC.Components;
using TowersWatsonIoC.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Concurrent;
using System.Data;
using System.Threading;

namespace TowersWatsonIoC
{
    public class ComponentContainer : IComponentContainer, IDisposable
    {
        private bool disposed = false;
        private ReaderWriterLockSlim componentsLocker = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        public ComponentContainer(bool prepareCompositionOnRegister = true, bool registerSelf = true, IComponentComposer defaultComposer = null, IConstructorSelector defaultConstructorSelector = null)
        {
            // NOTE: The settings in this constructor are getting pretty long, change it to a configuration class if more are added.
            this.PrepareCompositionOnRegister = prepareCompositionOnRegister;
            this.DefaultComposer = defaultComposer ?? new ReflectionComponentComposer(this);
            this.DefaultConstructorSelector = defaultConstructorSelector ?? new LargestConstructorSelector();

            if (registerSelf)
                this.Register<IComponentContainer>().To(this);
        }

        [ExcludeFromCodeCoverage]
        ~ComponentContainer()
        {
            this.Dispose(false);
        }

        public bool PrepareCompositionOnRegister { get; private set; }

        public IComponentComposer DefaultComposer { get; private set; }

        public IConstructorSelector DefaultConstructorSelector { get; private set; }

        protected Dictionary<Type, IContainerComponent> Components { get; private set; } = new Dictionary<Type, IContainerComponent>();

        public void AddRegisteredComponent<T>(IContainerComponent component)
        {
            if (null == component)
                throw new ArgumentNullException(nameof(component));

            if (this.HasRegisteredComponent<T>())
                throw new ArgumentException("This type is already registred.", nameof(T));

            this.componentsLocker.EnterWriteLock();

            try
            {
                this.Components.Add(typeof(T), component);
            }
            finally
            {
                this.componentsLocker.ExitWriteLock();
            }

            if (this.PrepareCompositionOnRegister && component.IsCompositionPreparationSupported)
                component.PrepareComposition(this.DefaultComposer, this.DefaultConstructorSelector);
        }

        public void RemoveRegisteredComponent<T>()
        {
            this.RemoveRegisteredComponent(typeof(T));
        }

        public void RemoveRegisteredComponent(Type componentType)
        {
            if (this.disposed)
                throw new ObjectDisposedException(this.GetType().Name);

            this.componentsLocker.EnterWriteLock();

            try
            {
                this.Components.Remove(componentType);
            }
            finally
            {
                this.componentsLocker.ExitWriteLock();
            }
        }

        public void ReplaceRegisteredComponent<T>(IContainerComponent component)
        {
            this.ReplaceRegisteredComponent(typeof(T), component);
        }

        public void ReplaceRegisteredComponent(Type componentType, IContainerComponent component)
        {
            if (this.disposed)
                throw new ObjectDisposedException(this.GetType().Name);

            this.componentsLocker.EnterWriteLock();

            try
            {
                this.Components[componentType] = component;
            }
            finally
            {
                this.componentsLocker.ExitWriteLock();
            }
        }

        public bool HasRegisteredComponent<T>()
        {
            return this.HasRegisteredComponent(typeof(T));
        }

        public bool HasRegisteredComponent(Type componentType)
        {
            if (this.disposed)
                throw new ObjectDisposedException(this.GetType().Name);

            this.componentsLocker.EnterReadLock();

            try
            {
                return this.Components.ContainsKey(componentType);
            }
            finally
            {
                this.componentsLocker.ExitReadLock();
            }
        }

        public IContainerComponent GetRegisteredComponent<T>()
        {
            return this.GetRegisteredComponent(typeof(T));
        }

        public IContainerComponent GetRegisteredComponent(Type componentType)
        {
            if (this.disposed)
                throw new ObjectDisposedException(this.GetType().Name);

            this.componentsLocker.EnterReadLock();

            if (!this.Components.ContainsKey(componentType))
                return null;

            try
            {
                return this.Components[componentType];
            }
            finally
            {
                this.componentsLocker.ExitReadLock();
            }
        }

        // TODO: Add composition support using parameters that aren't in the container (Builder extension syntax).

        public T Compose<T>(bool throwOnUnknown = true, 
            IComponentComposer composer = null, 
            IConstructorSelector constructorSelector = null)
        {
            if (this.disposed)
                throw new ObjectDisposedException(this.GetType().Name);

            return (T)this.Compose(typeof(T), throwOnUnknown, composer, constructorSelector);
        }

        public object Compose(Type componentType, 
            bool throwOnUnknown = true, 
            IComponentComposer composer = null, 
            IConstructorSelector constructorSelector = null)
        {
            if (this.disposed)
                throw new ObjectDisposedException(this.GetType().Name);

            if (null == componentType)
                throw new ArgumentNullException(nameof(componentType));

            var component = this.GetRegisteredComponent(componentType);

            if (null == component)
            {
                if (!componentType.IsClass)
                    throw new ArgumentException($"Failed to compose '{componentType.FullName}', no matching component registration was found. Did you forget to register a component?", nameof(componentType));

                if (componentType.IsAbstract)
                    throw new ArgumentException($"Failed to compose '{componentType.FullName}', no matching component registration was found and the requested type can not be constructed because it is abstract.", nameof(componentType));

                return (composer ?? this.DefaultComposer).ComposeUsingConstructor(
                    componentType, throwOnUnknown, constructorSelector ?? this.DefaultConstructorSelector);
            }

            return component.Compose(composer ?? this.DefaultComposer, throwOnUnknown,
                    constructorSelector ?? this.DefaultConstructorSelector);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
                return;

            if (disposing)
            {
                foreach (var component in this.Components)
                    component.Value.Dispose();

                componentsLocker.Dispose();
            }

            this.disposed = true;
        }

        object IServiceProvider.GetService(Type serviceType)
        {
            return this.Compose(serviceType);
        }
    }
}