using System.Runtime.InteropServices;
using DevExpress.Xpf.Gauges;

namespace RuntimeInstanceActivationSample
{
   /// <summary>
   ///    COM-wrapper for <see cref="DigitalGaugeLayerPresentation" />
   /// </summary>
   [ComVisible(true)]
   [Guid("749169E5-EDEA-404A-9BC4-CE7E4A501016")]
   public enum DigitalGaugeLayerPresentationKind
   {
      /// <summary>
      ///    Bind for <see cref="DigitalGaugeLayerPresentation" />
      /// </summary>
      [DigitalGaugeLayerPresentationClass(typeof(DigitalGaugeLayerPresentation))]
      None = 0,

      /// <summary>
      ///    Bind for <see cref="ClassicDigitalGaugeBackgroundLayerPresentation" />
      /// </summary>
      [DigitalGaugeLayerPresentationClass(typeof(ClassicDigitalGaugeBackgroundLayerPresentation))]
      Classic,

      /// <summary>
      ///    Bind for <see cref="CleanWhiteDigitalGaugeBackgroundLayerPresentation" />
      /// </summary>
      [DigitalGaugeLayerPresentationClass(typeof(CleanWhiteDigitalGaugeBackgroundLayerPresentation))]
      CleanWhite,

      /// <summary>
      ///    Bind for <see cref="CosmicDigitalGaugeBackgroundLayerPresentation" />
      /// </summary>
      [DigitalGaugeLayerPresentationClass(typeof(CosmicDigitalGaugeBackgroundLayerPresentation))]
      Cosmic,

      /// <summary>
      ///    Bind for <see cref="CustomDigitalGaugeLayerPresentation" />
      /// </summary>
      [DigitalGaugeLayerPresentationClass(typeof(CustomDigitalGaugeLayerPresentation))]
      Custom,

      /// <summary>
      ///    Bind for <see cref="DefaultDigitalGaugeBackgroundLayerPresentation" />
      /// </summary>
      [DigitalGaugeLayerPresentationClass(typeof(DefaultDigitalGaugeBackgroundLayerPresentation))]
      Default,

      /// <summary>
      ///    Bind for <see cref="EcoDigitalGaugeBackgroundLayerPresentation" />
      /// </summary>
      [DigitalGaugeLayerPresentationClass(typeof(EcoDigitalGaugeBackgroundLayerPresentation))]
      Eco,

      /// <summary>
      ///    Bind for <see cref="FutureDigitalGaugeBackgroundLayerPresentation" />
      /// </summary>
      [DigitalGaugeLayerPresentationClass(typeof(FutureDigitalGaugeBackgroundLayerPresentation))]
      Future,

      /// <summary>
      ///    Bind for <see cref="MagicLightDigitalGaugeBackgroundLayerPresentation" />
      /// </summary>
      [DigitalGaugeLayerPresentationClass(typeof(MagicLightDigitalGaugeBackgroundLayerPresentation))]
      MagicLight,

      /// <summary>
      ///    Bind for <see cref="ProgressiveDigitalGaugeBackgroundLayerPresentation" />
      /// </summary>
      [DigitalGaugeLayerPresentationClass(typeof(ProgressiveDigitalGaugeBackgroundLayerPresentation))]
      Progressive,

      /// <summary>
      ///    Bind for <see cref="RedClockDigitalGaugeBackgroundLayerPresentation" />
      /// </summary>
      [DigitalGaugeLayerPresentationClass(typeof(RedClockDigitalGaugeBackgroundLayerPresentation))]
      RedClock,

      /// <summary>
      ///    Bind for <see cref="SmartDigitalGaugeBackgroundLayerPresentation" />
      /// </summary>
      [DigitalGaugeLayerPresentationClass(typeof(SmartDigitalGaugeBackgroundLayerPresentation))]
      SmartDigital,

      /// <summary>
      ///    Bind for <see cref="YellowSubmarineDigitalGaugeBackgroundLayerPresentation" />
      /// </summary>
      [DigitalGaugeLayerPresentationClass(typeof(YellowSubmarineDigitalGaugeBackgroundLayerPresentation))]
      YellowSubmarine,

      /// <summary>
      ///    Bind for <see cref="IStyleDigitalGaugeBackgroundLayerPresentation" />
      /// </summary>
      [DigitalGaugeLayerPresentationClass(typeof(IStyleDigitalGaugeBackgroundLayerPresentation))]
      Style
   }
}