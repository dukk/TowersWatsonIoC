// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System;

namespace TowersWatsonIoC.Composition
{
    // TODO: Implement a IComponentComposer using Emit to increase performance.

    // This may work better as a "IComponentConstructorComposer" but because this is the only way to compose for now it is what it is.
    // This interface will get to big if it's not broken out and more ways to compose are added.
    public interface IComponentComposer
	{
		object ComposeUsingConstructor(Type componentType, bool throwOnUnknown, IConstructorSelector constructorSelector);

		bool IsPreparedToComposeUsingConstructor(Type componentType, IConstructorSelector constructorSelector);

		void PrepareToComposeUsingConstructor(Type componentType, IConstructorSelector constructorSelector);
	}
}