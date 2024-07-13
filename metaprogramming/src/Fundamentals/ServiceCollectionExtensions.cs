using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamentals;

public static class ServiceCollectionExtensions
{
   private static readonly string[] NamespacesToIgnoreForSelfBinding = ["System", "Microsoft"];

   public static IServiceCollection AddBindingsByConvention(this IServiceCollection services, ITypes types)
   {
      var conventionBasedTypes = types.All.Where(type =>
      {
         var interfaces = type.GetInterfaces();
         if (interfaces.Length > 0)
         {
            var conventionInterface = interfaces.SingleOrDefault(i => GetConvention(i, type));
            if (conventionInterface != default)
            {
               return types.All.Count(lType => lType.HasInterface(conventionInterface)) == 1;
            }
         }

         return false;
      });

      foreach (var conventionBasedType in conventionBasedTypes)
      {
         var interfaceToBind = types.All.Single(
            type => type.IsInterface && GetConvention(type, conventionBasedType));
         if (services.Any(serviceDescriptor => serviceDescriptor.ServiceType == interfaceToBind))
         {
            continue;
         }

         _ = conventionBasedType.HasAttribute<SingletonAttribute>()
            ? services.AddSingleton(interfaceToBind, conventionBasedType)
            : services.AddTransient(interfaceToBind, conventionBasedType);
      }

      return services;

      bool GetConvention(Type iType, Type tType) =>
         iType.Namespace == tType.Namespace && iType.Name == $"I{tType.Name}";
   }

   public static IServiceCollection AddSelfBinding(this IServiceCollection services, ITypes types)
   {
      const TypeAttributes staticType = TypeAttributes.Abstract | TypeAttributes.Sealed;

      types.All.Where(type =>
         (type.Attributes & staticType) != staticType
         && type is { IsInterface: false, IsAbstract: false }
         && !ShouldIgnoreNamespace(type.Namespace ?? string.Empty)
         && !HasConstructorWithUnresolvableParameters(type)
         && !HasConstructorWithRecordTypes(type)
         && !type.IsAssignableTo(typeof(Exception))
         && services.Any(descriptor => descriptor.ServiceType != type)).ToList().ForEach(serviceType =>
      {
         _ = serviceType.HasAttribute<SingletonAttribute>()
            ? services.AddSingleton(serviceType, serviceType)
            : services.AddTransient(serviceType, serviceType);
      });

      return services;
   }

   private static bool ShouldIgnoreNamespace(string namespaceToCheck) =>
      NamespacesToIgnoreForSelfBinding.Any(namespaceToCheck.StartsWith);

   private static bool HasConstructorWithUnresolvableParameters(Type type) =>
      type.GetConstructors()
         .Any(constructorInfo => constructorInfo.GetParameters()
            .Any(parameterInfo => parameterInfo.ParameterType.IsAPrimitiveType()));

   private static bool HasConstructorWithRecordTypes(Type type) =>
      type.GetConstructors().Any(
         constructorInfo => constructorInfo.GetParameters().Any(p => p.ParameterType.IsRecord()));
}