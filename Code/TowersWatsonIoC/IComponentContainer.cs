// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System;
using TowersWatsonIoC.Composition;
using TowersWatsonIoC.Components;

namespace TowersWatsonIoC
{
    public interface IComponentContainer : IServiceProvider
	{
		IComponentComposer DefaultComposer { get; }

		void AddRegisteredComponent<T>(IContainerComponent component);

		void RemoveRegisteredComponent<T>();

		void ReplaceRegisteredComponent<T>(IContainerComponent component);

		bool HasRegisteredComponent<T>();

		IContainerComponent GetRegisteredComponent<T>();

		T Compose<T>(bool throwOnUnknown = true, 
            IComponentComposer composer = null, 
            IConstructorSelector constructorSelector = null);

		object Compose(Type componentType, 
            bool throwOnUnknown = true, 
            IComponentComposer composer = null, 
            IConstructorSelector constructorSelector = null);
	}
}