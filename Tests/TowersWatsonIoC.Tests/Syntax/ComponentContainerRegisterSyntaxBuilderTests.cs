using System;
using TowersWatsonIoC.Syntax;
using TowersWatsonIoC.Components;
using Xunit;
using Moq;

namespace TowersWatsonIoC.Tests.Syntax
{
    public class ComponentContainerRegisterSyntaxBuilderTests
    {
        [Fact]
        public void ConstructorWithNullContainer()
        {
            Assert.Throws<ArgumentNullException>(
                () => new ComponentContainerRegisterSyntaxBuilder<ITestComponent>(null));
        }


        /*
        Message: Moq.MockException : 
Expected invocation on the mock at least once, but was never performed: c => c.AddRegisteredComponent<TestComponent>(It.IsAny<TransientComponent`2>())

Configured setups:
c => c.AddRegisteredComponent<TestComponent>(It.IsAny<TransientComponent`2>()), Times.Never

Performed invocations:
IComponentContainer.AddRegisteredComponent<ITestComponent>(TowersWatsonIoC.Components.TransientComponent`2[TowersWatsonIoC.Tests.ITestComponent,TowersWatsonIoC.Tests.TestComponent])

         */

        [Fact]
        public void To()
        {
            var mockContainer = new Mock<IComponentContainer>();

            mockContainer.Setup(c => c.AddRegisteredComponent<ITestComponent>(It.IsAny<TransientComponent<ITestComponent, TestComponent>>()));

            var container = mockContainer.Object;
            var syntaxBuilder = new ComponentContainerRegisterSyntaxBuilder<ITestComponent>(container);

            syntaxBuilder.To<TestComponent>();

            mockContainer.Verify(c => c.AddRegisteredComponent<ITestComponent>(It.IsAny<TransientComponent<ITestComponent, TestComponent>>()), Times.Once());
            mockContainer.Verify(c => c.ReplaceRegisteredComponent<ITestComponent>(It.IsAny<TransientComponent<ITestComponent, TestComponent>>()), Times.Never());
        }

        [Fact]
        public void ToAsReplace()
        {
            var mockContainer = new Mock<IComponentContainer>();

            mockContainer.Setup(c => c.ReplaceRegisteredComponent<ITestComponent>(It.IsAny<TransientComponent<ITestComponent, TestComponent>>()));

            var container = mockContainer.Object;
            var syntaxBuilder = new ComponentContainerRegisterSyntaxBuilder<ITestComponent>(container, replace: true);

            syntaxBuilder.To<TestComponent>();

            mockContainer.Verify(c => c.AddRegisteredComponent<ITestComponent>(It.IsAny<TransientComponent<ITestComponent, TestComponent>>()), Times.Never());
            mockContainer.Verify(c => c.ReplaceRegisteredComponent<ITestComponent>(It.IsAny<TransientComponent<ITestComponent, TestComponent>>()), Times.Once());
        }
    }
}
