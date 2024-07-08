using System.Reflection;
using AOP;
using AOP.Todo;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Microsoft.Extensions.Logging;

var container = new WindsorContainer();

var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
container.Register(Component.For<ILoggerFactory>().Instance(loggerFactory));

var createLoggerMethod = typeof(LoggerFactoryExtensions)
   .GetMethods(BindingFlags.Public | BindingFlags.Static)
   .First(methodInfo => methodInfo is { Name: nameof(LoggerFactory.CreateLogger), IsGenericMethod: true });

container.Register(Component.For<ILogger>().UsingFactoryMethod((kernel, context) =>
{
   var factory = kernel.Resolve<ILoggerFactory>();
   return factory.CreateLogger(context.Handler.ComponentModel.Implementation);
}).LifestyleTransient());
container.Register(Component.For(typeof(ILogger<>)).UsingFactoryMethod((kernel, context) =>
{
   var factory = kernel.Resolve<ILoggerFactory>();
   var logger = createLoggerMethod.MakeGenericMethod(context.RequestedType.GenericTypeArguments[0])
      .Invoke(null, [factory]);
   return logger;
}));

container.Install(FromAssembly.InThisApplication(Assembly.GetEntryAssembly()));

var usersService = container.Resolve<IUsersService>();
await usersService.Register("jane@doe.io", "Password1");

var authenticated = (usersService as IAuthenticator)!.Authenticate("jane@doe.io", "Password1");
var authorized = (usersService as IAuthorizer)!.IsAuthorized("jane@doe.io", "Some Action");
Console.WriteLine($"Authenticated: {authenticated}");
Console.WriteLine($"Authorized: {authorized}");

var composition = container.Resolve<IUsersServiceComposition>();
authenticated = composition.Authenticate("jane@doe.io", "Password1");
authorized = composition.IsAuthorized("jane@doe.io", "Some Action");
Console.WriteLine($"Authenticated: {authenticated}");
Console.WriteLine($"Authorized: {authorized}");

var todo = container.Resolve<ITodoService>();
todo.Add("Buy milk");

Console.ReadLine();