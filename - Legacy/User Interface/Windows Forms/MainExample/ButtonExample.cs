using System;
using System.Windows.Forms;
using MainExample.Properties;

namespace MainExample
{
   public partial class ButtonExample : Form
   {
      public ButtonExample()
      {
         InitializeComponent();
      }

      private void defaultButton_Click(object sender, EventArgs e)
      {
         MessageBox.Show(Resources.DefBtnClick);
      }

      private void threeState_CheckStateChanged(object sender, EventArgs e)
      {
         var checkBox = sender as CheckBox;
         if (null != checkBox)
            threeStateState.Text = string.Format("State is {0}", checkBox.CheckState);
      }
   }
}