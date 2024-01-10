/*
 * Runtime activation
 */

using System;
using System.Linq;
using DevExpress.Xpf.Gauges;

namespace RuntimeInstanceActivationSample
{
   internal static class Program
   {
      private static readonly DigitalGaugeLayerPresentationKind[] _DigitalGaugeLayerPresentationKinds =
         Enum.GetValues(typeof(DigitalGaugeLayerPresentationKind))
            .Cast<DigitalGaugeLayerPresentationKind>()
            .Where(kind => kind != default(DigitalGaugeLayerPresentationKind))
            .ToArray();

      private static void Main()
      {
         Array.ForEach(_DigitalGaugeLayerPresentationKinds, kind =>
         {
            var presentation = Convert(kind);
            var kindToCheck = Convert(presentation);
            Console.WriteLine(kindToCheck == kind ? $"OK: for kind {kind}" : $"Not OK for kind {kind}");
         });
      }

      private static DigitalGaugeLayerPresentation Convert(
         DigitalGaugeLayerPresentationKind digitalGaugeLayerPresentationKind)
      {
         if (digitalGaugeLayerPresentationKind == DigitalGaugeLayerPresentationKind.None)
         {
            throw new ArgumentException(
               "Cannot create abstract type", nameof(digitalGaugeLayerPresentationKind));
         }

         var layerPresentationType = digitalGaugeLayerPresentationKind.GetDigitalGaugeLayerPresentationType();
         var defaultCtorExists = layerPresentationType.GetConstructors()
            .FirstOrDefault(ctor => ctor.GetParameters().Length == 0);
         if (defaultCtorExists == null)
         {
            throw new ArgumentException(
               "Default constructor doesn't exixts", nameof(digitalGaugeLayerPresentationKind));
         }

         return (DigitalGaugeLayerPresentation) Activator.CreateInstance(layerPresentationType);
      }

      private static DigitalGaugeLayerPresentationKind Convert(DigitalGaugeLayerPresentation layerPresentation)
      {
         var gaugeLayerType = layerPresentation.GetType();
         var layerPresentationKind = _DigitalGaugeLayerPresentationKinds
            .FirstOrDefault(presentationKind =>
               presentationKind.GetDigitalGaugeLayerPresentationType() == gaugeLayerType);

         if (layerPresentationKind != default(DigitalGaugeLayerPresentationKind))
         {
            return layerPresentationKind;
         }

         throw new ArgumentException(
            $"class {layerPresentation.GetType().FullName} not supported for enum {nameof(DigitalGaugeLayerPresentationKind)}",
            nameof(layerPresentation));
      }
   }
}