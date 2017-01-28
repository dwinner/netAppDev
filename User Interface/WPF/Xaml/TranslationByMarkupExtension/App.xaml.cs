using System.Reflection;
using System.Windows;

namespace TranslationByMarkupExtension
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