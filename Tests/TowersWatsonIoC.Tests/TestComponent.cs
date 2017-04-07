// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;

namespace TowersWatsonIoC.Tests
{
    public interface ITestComponent { }

	[ExcludeFromCodeCoverage]
    public class TestComponent : ITestComponent { }

    [ExcludeFromCodeCoverage]
    public class TestComponentWithADependancy : ITestComponent
    {
        public TestComponentWithADependancy(ITestComponent a)
        {
            this.A = a;
        }

        public ITestComponent A { get; private set; }
    }

    [ExcludeFromCodeCoverage]
    public class TestComponentWithTwoDependencies : ITestComponent
    {
        public TestComponentWithTwoDependencies(ITestComponent a, ITestComponent b)
        {
            this.A = a;
            this.B = b;
        }

        public ITestComponent A { get; private set; }

        public ITestComponent B { get; private set; }
    }

    [ExcludeFromCodeCoverage]
    public class TestComponentWithAPrimitiveDependancy : ITestComponent
    {
        public TestComponentWithAPrimitiveDependancy(int a)
        {
            this.A = a;
        }

        public int A { get; private set; }
    }
}