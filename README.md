# TowersWatsonIoC

This project was created at the request of a company as part of their job interview process. This project was never intended to actually be used (though I really enjoyed making it so I may continue it just for fun).

### Features
* Component life cycle management.
	* Includes static (constant), transient, singleton, and thread static singleton already.
* Simple builder syntax for component management.
* Reflection based composition (for now). 
* Extensible, you can easily extend it with your own:
	* Component life cycles
	* Builder syntax
	* Composition mechanisms
	* Constructor selectors

### Usage
#### Container Creation
```
var container = new ComponentContainer();

// This is the same as above, just showing the optional configuration
var container = new ComponentContainer(
	registerSelf: true, 
	prepareCompositionOnRegister: true, 
	defaultComposer: new ReflectionComponentComposer(),
	defaultConstructorSelector: new LargestConstructorSelector());
```
#### Component Registration (using the builder syntax)
```
// Transient:
container.Register<ISomeComponent>().To<SomeComponentImplementation>();

// Static (constant, no real life cycle management):
container.Register<ISomeComponent>().To(new SomeComponentImplementation());

// Singleton:
container.Register<ISomeComponent>().To<SomeComponentImplementation>().AsSingleton();

// Thread static singleton:
container.Register<ISomeComponent>().To<SomeComponentImplementation>().AsSingleton().PerThread();
```
#### Component Registration (without using the builder syntax)
```
// Transient:
container.AddRegisteredComponent<ISomeComponent>(new TransientComponent<ISomeComponent, SomeComponentImplementation>());

// Static (constant, no real life cycle management):
container.AddRegisteredComponent<ISomeComponent>(new StaticComponent<ISomeComponent>(new SomeComponentImplementation()));

// Singleton:
container.AddRegisteredComponent<ISomeComponent>(new SingletonComponent<ISomeComponent, SomeComponentImplementation>());

// Thread static singleton:
container.AddRegisteredComponent<ISomeComponent>(new PerThreadComponent<ISomeComponent, SomeComponentImplementation>());
```
#### Composition / Dependency Injection
```
container.Compose<SomeController>();

// ...

public class SomeController
{
	public SomeController(ISomeComponenet someComponenet)
	{
		// ...
	}
}
```
##### Another example
```
container.Register<ISomeOtherComponenet>().To<SomeOtherComponenetImplementation>();
container.Register<ISomeComponent>().To<SomeComponentImplementation>();

// ...

container.Compose<ISomeComponent>();

// ...

public class SomeComponentImplementation : ISomeComponent
{
	public SomeComponentImplementation(ISomeOtherComponenet someOtherComponenet)
	{
		// ...
	}
}

```
