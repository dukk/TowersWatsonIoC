// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System;
using TowersWatsonIoC.Components;

namespace TowersWatsonIoC.Syntax
{
    internal class ComponentContainerRegisterToSyntaxBuilder<TComponentType, TImplementation> : IComponentContainerRegisterToSyntax<TComponentType, TImplementation>
		where TImplementation : class, TComponentType
	{
		public ComponentContainerRegisterToSyntaxBuilder(IComponentContainer container)
		{
			this.Container = container ?? throw new ArgumentNullException(nameof(container));
		}

		protected IComponentContainer Container { get; private set; }

		public IComponentContainerRegisterAsSingletonSyntax<TComponentType, TImplementation> AsSingleton()
		{
			var component = new SingletonComponent<TComponentType, TImplementation>();

			this.Container.ReplaceRegisteredComponent<TComponentType>(component);

            return new ComponentContainerRegisterAsSingletonSyntax<TComponentType, TImplementation>(this.Container);
		}
	}
}