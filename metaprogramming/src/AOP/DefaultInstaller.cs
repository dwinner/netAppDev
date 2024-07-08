using System.Reflection;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.Extensions.Logging;

namespace AOP;

public class DefaultInstaller : IWindsorInstaller
{
   public void Install(IWindsorContainer container, IConfigurationStore store)
   {
      container.Register(Component.For<InterceptorSelector>());
      container.Register(Component.For<AuthorizationInterceptor>());

      container.Register(
         Component.For<IAuthenticator>()
            .ImplementedBy<Authenticator>()
            .LifestyleTransient());

      container.Register(
         Component.For<IAuthorizer>()
            .ImplementedBy<Authorizer>()
            .LifestyleTransient());

      container.Register(
         Component.For<IUsersService>()
            .ImplementedBy<UsersService>()
            .Proxy.AdditionalInterfaces(typeof(IAuthorizer), typeof(IAuthenticator))
            .Proxy.MixIns(registration => registration
               .Component<Authorizer>()
               .Component<Authenticator>())
            .Interceptors<LoggingInterceptor>()
            .Interceptors<AuthorizationInterceptor>()
            .SelectInterceptorsWith(
               registration => registration.Service<InterceptorSelector>())
            .LifestyleTransient());

      container.Register(
         Component.For<IUsersServiceComposition>()
            .UsingFactoryMethod((_, _) =>
            {
               var proxyGenerator = new ProxyGenerator();
               var proxyGenerationOptions = new ProxyGenerationOptions();
               proxyGenerationOptions.AddMixinInstance(container.Resolve<IAuthorizer>());
               proxyGenerationOptions.AddMixinInstance(container.Resolve<IAuthenticator>());
               var logger = container.Resolve<ILogger<UsersService>>();
               proxyGenerationOptions.AddMixinInstance(new UsersService(logger));
               return (proxyGenerator.CreateClassProxyWithTarget(
                  typeof(object),
                  [typeof(IUsersServiceComposition)],
                  new object(),
                  proxyGenerationOptions) as IUsersServiceComposition)!;
            }));

      container.Register(Component.For<LoggingInterceptor>());
      container.Register(Classes.FromAssemblyInThisApplication(Assembly.GetEntryAssembly())
         .Pick()
         .WithService.DefaultInterfaces()
         .Configure(registration => registration
            .Interceptors<LoggingInterceptor>()
            .Interceptors<AuthorizationInterceptor>()
            .SelectInterceptorsWith(itemRegistration => itemRegistration.Service<InterceptorSelector>()))
         .LifestyleTransient());
   }
}