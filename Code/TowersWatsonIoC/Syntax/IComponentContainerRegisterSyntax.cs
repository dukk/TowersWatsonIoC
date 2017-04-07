// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

namespace TowersWatsonIoC.Syntax
{
	/// <summary>
	/// Provides the builder syntax for the containers <see cref="ComponentContainerSyntaxExtensions.Register{T}(IComponentContainer)"/> method.
	/// </summary>
	/// <remarks>This interface is not intended to be used or impmeneted by anything other than the internal builder type.</remarks>
	/// <typeparam name="TComponentType"></typeparam>
	public interface IComponentContainerRegisterSyntax<TComponentType>
	{
		/// <summary>
		/// Creates and registers a <see cref="component.TransientComponentRegistration{TType, TImplementation}"/> (new instance will be created every time it's used).
		/// </summary>
		/// <typeparam name="TImplementation">The type that will be initialized and provided as the implementation for the component component.</typeparam>
		/// <returns></returns>
		IComponentContainerRegisterToSyntax<TComponentType, TImplementation> To<TImplementation>()
			where TImplementation : class, TComponentType;

		/// <summary>
		/// Creates and registers a <see cref="component.StaticComponentRegistration{T}"/>.
		/// </summary>
		/// <remarks>The components life-cycle isn't managed by the container when using this component type.</remarks>
		/// <param name="instance">The instance that will be provided for the component component.</param>
		void To(TComponentType instance);
	}
}