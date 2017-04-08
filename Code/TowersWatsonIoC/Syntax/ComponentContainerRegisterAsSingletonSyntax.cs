using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowersWatsonIoC.Components;

namespace TowersWatsonIoC.Syntax
{
    internal class ComponentContainerRegisterAsSingletonSyntax<TComponent, TImplementation> : IComponentContainerRegisterAsSingletonSyntax<TComponent, TImplementation>
        where TImplementation : class, TComponent
    {
        public ComponentContainerRegisterAsSingletonSyntax(IComponentContainer container)
        {
            this.Container = container ?? throw new ArgumentNullException(nameof(container));
        }

        protected IComponentContainer Container { get; private set; }

        public void PerThread()
        {
            var component = new PerThreadComponent<TComponent, TImplementation>();

            this.Container.ReplaceRegisteredComponent<TComponent>(component);
        }
    }
}