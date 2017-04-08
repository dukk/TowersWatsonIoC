using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowersWatsonIoC.Syntax
{
    public interface IComponentContainerRegisterAsSingletonSyntax<TComponentType, TImplementation>
        where TImplementation : class
    {
        /// <summary>
		/// Replaces the singleton component component with a <see cref="component.PerThreadComponent{T}"/>.
		/// </summary>
		void PerThread();
    }
}