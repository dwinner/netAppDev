/**
 * Локализация приложений с помощью расширений разметки
 */

using System.Reflection;
using System.Windows;
using TranslationByMarkupExtension;

namespace i18nViaMarkupExt
{
   public partial class App
   {
      protected override void OnStartup(StartupEventArgs e)
      {
         TranslationManager.Instance.TranslationProvider =
            new ResxTranslationProvider("TranslationByMarkupExtension.Resources", Assembly.GetExecutingAssembly());
         base.OnStartup(e);
      }
   }
}