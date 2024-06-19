using Autofac;
using RendererSample;

var containerBuilder = new ContainerBuilder();
containerBuilder.RegisterType<VectorRenderer>().As<IRenderer>();
containerBuilder.Register(
   (componentContext, parameters) => new Circle(componentContext.Resolve<IRenderer>(),
   parameters.Positional<float>(0)));
using var container = containerBuilder.Build();
var circle = container.Resolve<Circle>(new PositionalParameter(0, 5.0f));
circle.Draw();
circle.Resize(2);
circle.Draw();