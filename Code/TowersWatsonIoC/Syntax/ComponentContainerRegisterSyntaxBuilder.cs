// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System;
using TowersWatsonIoC.Components;

namespace TowersWatsonIoC.Syntax
{
    internal class ComponentContainerRegisterSyntaxBuilder<TComponentType> : IComponentContainerRegisterSyntax<TComponentType>
	{
		public ComponentContainerRegisterSyntaxBuilder(IComponentContainer container, bool replace = false)
		{
			this.Container = container ?? throw new ArgumentNullException(nameof(container));
			this.IsReplace = replace;
		}

		protected bool IsReplace { get; private set; }

		protected IComponentContainer Container { get; private set; }

		public IComponentContainerRegisterToSyntax<TComponentType, TImplementation> To<TImplementation>()
			where TImplementation : class, TComponentType
		{
			var component = new TransientComponent<TComponentType, TImplementation>();

			this.RegisterOrReplace(component);

			return new ComponentContainerRegisterToSyntaxBuilder<TComponentType, TImplementation>(this.Container);
		}

		public void To(TComponentType instance)
		{
			var component = new StaticComponent<TComponentType>(instance);

			this.RegisterOrReplace(component);
		}

		protected void RegisterOrReplace(IContainerComponent component)
		{
			if (this.IsReplace)
			{
				this.Container.ReplaceRegisteredComponent<TComponentType>(component);
			}
			else
			{
				this.Container.AddRegisteredComponent<TComponentType>(component);
			}
		}
	}
}