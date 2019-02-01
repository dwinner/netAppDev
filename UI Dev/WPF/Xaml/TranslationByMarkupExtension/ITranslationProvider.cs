using System.Collections.Generic;
using System.Globalization;

namespace TranslationByMarkupExtension
{
   public interface ITranslationProvider
   {
      /// <summary>
      ///    Gets the available languages.
      /// </summary>
      /// <value>The available languages.</value>
      IEnumerable<CultureInfo> Languages { get; }

      /// <summary>
      ///    Translates the specified key.
      /// </summary>
      /// <param name="key">The key.</param>
      /// <returns></returns>
      object Translate(string key);
   }
}