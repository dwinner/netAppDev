using System;
using System.Windows.Forms;
using ModalVsModeless.Properties;

namespace ModalVsModeless
{
   public partial class PopupForm : Form
   {
      public PopupForm()
      {
         InitializeComponent();
      }

      protected override void OnLoad(EventArgs e)
      {
         labelStatus.Text = Resources.FormDisplayed + (Modal ? "modally" : "modelessly");
      }
   }
}