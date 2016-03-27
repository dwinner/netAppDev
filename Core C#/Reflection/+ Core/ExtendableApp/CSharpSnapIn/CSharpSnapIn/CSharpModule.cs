using System.Windows.Forms;
using CommonSnappableTypes;

namespace CSharpSnapIn
{
   [CompanyInfo(CompanyName = "My Company", CompanyUrl = "www.MyCompany.com")]
   public class CSharpModule : IAppFunctionality
   {
      public void DoIt()
      {
         MessageBox.Show("You have just used the C# snap in");
      }
   }
}