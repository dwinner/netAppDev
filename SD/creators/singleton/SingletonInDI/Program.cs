using Autofac;
using static System.Console;

var builder = new ContainerBuilder();
builder.RegisterType<EventBroker>().SingleInstance();
builder.RegisterType<Foo>();

using var container = builder.Build();
var foo1 = container.Resolve<Foo>();
var foo2 = container.Resolve<Foo>();

WriteLine(ReferenceEquals(foo1, foo2));
WriteLine(ReferenceEquals(foo1.Broker, foo2.Broker));