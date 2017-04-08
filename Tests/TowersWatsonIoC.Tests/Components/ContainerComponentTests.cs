// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using TowersWatsonIoC.Composition;

namespace TowersWatsonIoC.Tests.Components
{
    public abstract class ContainerComponentTests
    {
        // TODO: Refactor a lot more into this, theses tests are all almost the same.

        protected ComponentContainer Container;
        protected ReflectionComponentComposer Composer;
        protected LargestConstructorSelector ConstructorSelector;

        public ContainerComponentTests()
        {
            this.Container = new ComponentContainer();
            this.Composer = new ReflectionComponentComposer(this.Container);
            this.ConstructorSelector = new LargestConstructorSelector();
        }
    }
}