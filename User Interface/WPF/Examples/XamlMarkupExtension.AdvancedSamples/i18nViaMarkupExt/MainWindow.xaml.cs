using TranslationByMarkupExtension;

namespace i18nViaMarkupExt
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();         
         DataContext = new MainWindowViewModel();
      }
   }
}