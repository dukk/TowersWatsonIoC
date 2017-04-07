// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using TowersWatsonIoC.Syntax;

namespace TowersWatsonIoC
{
	public static class ComponentContainerSyntaxExtensions
	{
        /// <summary>
        /// Registers a component with the component container.
        /// </summary>
        /// <typeparam name="T">The type the component instance will represent (usually an interface).</typeparam>
        /// <param name="container">The container to register the component with.</param>
        /// <returns>A container component builder to simplify the component registration.</returns>
		public static IComponentContainerRegisterSyntax<T> Register<T>(this IComponentContainer container)
		{
			return new ComponentContainerRegisterSyntaxBuilder<T>(container);
		}

        /// <summary>
        /// Replaces an existing component within the component container.
        /// </summary>
        /// <typeparam name="T">The type the component instance will represent (usually an interface).</typeparam>
        /// <param name="container">The container to register the component with.</param>
        /// <returns>A container component builder to simplify the component registration.</returns>
		public static IComponentContainerRegisterSyntax<T> Replace<T>(this IComponentContainer container)
		{
			return new ComponentContainerRegisterSyntaxBuilder<T>(container, replace: true);
		}

        /// <summary>
        /// Unregisters a component with the component container.
        /// </summary>
        /// <typeparam name="T">The type the component instance will represent (usually an interface).</typeparam>
        /// <param name="container">The container to register the component with.</param>
		public static void Unregister<T>(this IComponentContainer container)
		{
			container.RemoveRegisteredComponent<T>();
		}
	}
}