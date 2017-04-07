// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Reflection;

namespace TowersWatsonIoC.Composition
{
    public interface IConstructorSelector
    {
        ConstructorInfo SelectConstructor(Type componentType);
    }
}