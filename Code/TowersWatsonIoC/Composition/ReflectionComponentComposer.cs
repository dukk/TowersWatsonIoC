// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TowersWatsonIoC.Composition
{
	public class ReflectionComponentComposer : IComponentComposer
	{
		public ReflectionComponentComposer(IComponentContainer componentContainer, IConstructorSelector defaultConstructorSelector = null)
		{
			this.Container = componentContainer ?? throw new ArgumentNullException(nameof(componentContainer));
			this.DefaultConstructorSelector = defaultConstructorSelector ?? new LargestConstructorSelector();
		}

		protected Dictionary<Tuple<Type, Type>, Func<bool, object>> Activators { get; private set; } = new Dictionary<Tuple<Type, Type>, Func<bool, object>>();

		protected IComponentContainer Container { get; private set; }

		protected IConstructorSelector DefaultConstructorSelector { get; private set; }

		public object ComposeUsingConstructor(Type componentType, bool throwOnUnknown, IConstructorSelector constructorSelector)
		{
            if (null == componentType)
                throw new ArgumentNullException(nameof(componentType));

            if (null == constructorSelector)
                throw new ArgumentNullException(nameof(constructorSelector));

            if (!this.IsPreparedToComposeUsingConstructor(componentType, constructorSelector))
				this.PrepareToComposeUsingConstructor(componentType, constructorSelector);

            var activatorKey = Tuple.Create(componentType, constructorSelector.GetType());

            return this.Activators[activatorKey](throwOnUnknown);
		}

		public bool IsPreparedToComposeUsingConstructor(Type componentType, IConstructorSelector constructorSelector)
		{
            if (null == componentType)
                throw new ArgumentNullException(nameof(componentType));

            if (null == constructorSelector)
                throw new ArgumentNullException(nameof(constructorSelector));

            var activatorKey = Tuple.Create(componentType, constructorSelector.GetType());

            return this.Activators.ContainsKey(activatorKey);
		}

		public virtual void PrepareToComposeUsingConstructor(Type componentType, IConstructorSelector constructorSelector)
		{
            if (null == componentType)
                throw new ArgumentNullException(nameof(componentType));

            if (null == constructorSelector)
                throw new ArgumentNullException(nameof(constructorSelector));

            var selectedConstructorInfo = this.DefaultConstructorSelector.SelectConstructor(componentType);
            var activatorKey = Tuple.Create(componentType, constructorSelector.GetType());

            this.Activators.Add(activatorKey, 
                (throwOnUnknown) => this.ComposeUsingConstructor(throwOnUnknown, selectedConstructorInfo, constructorSelector));
		}

		protected virtual object ComposeUsingConstructor(bool throwOnUnknown, ConstructorInfo constructorInfo, IConstructorSelector constructorSelector)
		{
            if (null == constructorInfo)
                throw new ArgumentNullException(nameof(constructorInfo));

            var parameters = constructorInfo.GetParameters();
			var compsedParameters = new object[parameters.Length];

			for (var i = 0; i < parameters.Length; i++)
			{
				compsedParameters[i] = this.Container.Compose(parameters[i].ParameterType, 
                    throwOnUnknown, this, constructorSelector);
			}

			return constructorInfo.Invoke(compsedParameters);
		}
	}	
}