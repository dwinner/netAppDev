/**
 * Runtime activation
 */

using System;
using DevExpress.Xpf.Gauges;

namespace RuntimeInstanceActivationSample
{
   [AttributeUsage(AttributeTargets.Field)]
   public sealed class DigitalGaugeLayerPresentationClassAttribute : Attribute
   {
      private static readonly Func<Type, Type, bool> _CreationPredicate = (baseType, instanceType) =>
         instanceType.IsClass && instanceType.IsPublic && !instanceType.IsAbstract
         && baseType.IsAssignableFrom(instanceType);

      public DigitalGaugeLayerPresentationClassAttribute(Type instanceType)
      {
         if (!_CreationPredicate(typeof(DigitalGaugeLayerPresentation), instanceType))
         {
            throw new ArgumentException(
               $"{instanceType.Name} isn't inherited from {nameof(DigitalGaugeLayerPresentation)}",
               nameof(instanceType));
         }

         InstanceType = instanceType;
      }

      public Type InstanceType { get; }
   }
}