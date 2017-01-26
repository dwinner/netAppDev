using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace i18nViaMarkupExt
{
   /// <summary>
   /// </summary>
   public class ResxTranslationProvider : ITranslationProvider
   {
      private readonly ResourceManager _resourceManager;

      /// <summary>
      ///    Initializes a new instance of the <see cref="ResxTranslationProvider" /> class.
      /// </summary>
      /// <param name="baseName">Name of the base.</param>
      /// <param name="assembly">The assembly.</param>
      public ResxTranslationProvider(string baseName, Assembly assembly)
      {
         _resourceManager = new ResourceManager(baseName, assembly);
      }

      /// <summary>
      ///    See <see cref="ITranslationProvider.Translate" />
      /// </summary>
      public object Translate(string key)
      {
         return _resourceManager.GetString(key);
      }

      /// <summary>
      ///    See <see cref="ITranslationProvider.AvailableLanguages" />
      /// </summary>
      public IEnumerable<CultureInfo> Languages
      {
         get
         {
            // TODO: Resolve the available languages
            yield return new CultureInfo("de");
            yield return new CultureInfo("en");
         }
      }
   }
}