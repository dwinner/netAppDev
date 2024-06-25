using System;
using System.Linq;

namespace RuntimeInstanceActivationSample
{
   public static class DigitalGaugeLayerPresentationKindExtensions
   {
      public static Type GetDigitalGaugeLayerPresentationType(
         this DigitalGaugeLayerPresentationKind digitalGaugeLayerPresentationKind) =>
         digitalGaugeLayerPresentationKind
            .GetType()
            .GetField(digitalGaugeLayerPresentationKind.ToString())
            .GetCustomAttributes(typeof(DigitalGaugeLayerPresentationClassAttribute), false)
            .Cast<DigitalGaugeLayerPresentationClassAttribute>()
            .FirstOrDefault()?.InstanceType;
   }
}