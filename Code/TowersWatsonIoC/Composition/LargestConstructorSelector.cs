// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;
using System.Reflection;

namespace TowersWatsonIoC.Composition
{
    public class LargestConstructorSelector : IConstructorSelector
    {
        public ConstructorInfo SelectConstructor(Type componentType)
        {
            var selectedConstructor = (from constructor in componentType.GetConstructors()
                                       orderby constructor.GetParameters().Length descending
                                       select constructor).First();

            return selectedConstructor;
        }
    }
}