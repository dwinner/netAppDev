using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace i18nViaMarkupExt
{
   public class MainWindowViewModel
   {
      public MainWindowViewModel()
      {
         Languages = CollectionViewSource.GetDefaultView(TranslationManager.Instance.Languages);
         Languages.CurrentChanged +=
            (s, e) => TranslationManager.Instance.CurrentLanguage = (CultureInfo) Languages.CurrentItem;
      }

      public ICollectionView Languages { get; private set; }
   }
}