using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace TranslationByMarkupExtension
{
   public class TranslationManager
   {
      private static TranslationManager _translationManager;

      public CultureInfo CurrentLanguage
      {
         get { return Thread.CurrentThread.CurrentUICulture; }
         set
         {
            if (!Equals(value, Thread.CurrentThread.CurrentUICulture))
            {
               Thread.CurrentThread.CurrentUICulture = value;
               OnLanguageChanged();
            }
         }
      }

      public IEnumerable<CultureInfo> Languages
      {
         get
         {
            return TranslationProvider != null
               ? TranslationProvider.Languages
               : Enumerable.Empty<CultureInfo>();
         }
      }

      public static TranslationManager Instance
      {
         get { return _translationManager ?? (_translationManager = new TranslationManager()); }
      }

      public ITranslationProvider TranslationProvider { get; set; }

      public event EventHandler LanguageChanged;

      private void OnLanguageChanged()
      {
         if (LanguageChanged != null)
            LanguageChanged(this, EventArgs.Empty);
      }

      public object Translate(string key)
      {
         if (TranslationProvider != null)
         {
            var translatedValue = TranslationProvider.Translate(key);
            if (translatedValue != null)
               return translatedValue;
         }

         return string.Format("!{0}!", key);
      }
   }
}