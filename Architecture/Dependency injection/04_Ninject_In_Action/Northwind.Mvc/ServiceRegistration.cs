using System.Linq;
using System.Web.Mvc;
using JetBrains.Annotations;
using log4net;
using Ninject.Activation;
using Ninject.Extensions.Conventions;
using Ninject.Modules;
using Ninject.Web.Mvc.Filter;
using Ninject.Web.Mvc.FilterBindingSyntax;

namespace Northwind.Mvc
{
   [UsedImplicitly]
   public class ServiceRegistration : NinjectModule
   {
      public override void Load()
      {
         Kernel.Bind(x => x.FromAssembliesMatching("Northwind.*")
            .SelectAllClasses()
            .BindAllInterfaces());

         Bind<ILog>().ToMethod(GetLogger);

         Kernel.BindFilter<LogFilter>(FilterScope.Action, 0)
            .WhenActionMethodHas<LogAttribute>()
            .WithConstructorArgumentFromActionAttribute<LogAttribute>(
               "logLevel",
               attribute => attribute.LogLevel);
      }

      private static ILog GetLogger(IContext ctx)
      {
         var filterContext = ctx.Request.ParentRequest.Parameters
            .OfType<FilterContextParameter>().SingleOrDefault();
         return LogManager.GetLogger(filterContext == null
            ? ctx.Request.Target.Member.DeclaringType
            : filterContext.ActionDescriptor.ControllerDescriptor.ControllerType);
      }
   }
}