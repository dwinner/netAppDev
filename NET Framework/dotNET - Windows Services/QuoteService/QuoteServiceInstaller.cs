using System.ComponentModel;

namespace QuoteService
{
   [RunInstaller(true)]
   public partial class QuoteServiceInstaller : System.Configuration.Install.Installer
   {
      public QuoteServiceInstaller()
      {
         InitializeComponent();
      }
   }
}
