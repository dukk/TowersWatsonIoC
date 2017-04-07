// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

namespace TowersWatsonIoC.Syntax
{
    /// <summary>
    /// Provides the builder syntax for the containers <see cref="IComponentContainerRegisterSyntax{TComponentType}.To(TComponentType)"/> method.
    /// </summary>
    /// <remarks>This interface is not intended to be used or impmeneted by anything other than the internal builder type.</remarks>
    /// <typeparam name="TComponentType"></typeparam>
    /// <typeparam name="TImplementation"></typeparam>
    public interface IComponentContainerRegisterToSyntax<TComponentType, TImplementation>
	{
		/// <summary>
		/// Replaces the default transient component component with a <see cref="component.SingletonComponentRegistration{T}"/>.
		/// </summary>
		void AsSingleton();

		/// <summary>
		/// Replaces the default transient component component with a <see cref="component.PerThreadComponent{T}"/>.
		/// </summary>
		void AsSingletonPerThread(); // TODO: Move this to .AsSIngleton().PerThread();
	}
}