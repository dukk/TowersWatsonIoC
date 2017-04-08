// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System;
using TowersWatsonIoC.Composition;

namespace TowersWatsonIoC.Components
{
	public interface IContainerComponent : IDisposable
	{
        Type InstanceType { get; }

        bool IsCompositionPreparationSupported { get; }

        object Compose(IComponentComposer composer, bool throwOnUnknown, 
            IConstructorSelector constructorSelector);

		void PrepareComposition(IComponentComposer composer, IConstructorSelector constructorSelector);
	}

	public interface IContainerComponent<T> : IContainerComponent
	{
		new T Compose(IComponentComposer composer, bool throwOnUnknown, 
            IConstructorSelector constructorSelector);
	}
}