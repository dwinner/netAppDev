using System.Windows.Forms;
using UIFactory.Core;

namespace UIFactory.Main
{
   public partial class Form1 : Form
   {
      public Form1(IUiFactory uiFactory)
      {
         InitializeComponent();

         for (var i = 0; i < 10; i++)
         {
            panel.Controls.Add(uiFactory.GetCheckBox());
         }

         panel.Controls.Add(uiFactory.GetButton());
      }
   }
}