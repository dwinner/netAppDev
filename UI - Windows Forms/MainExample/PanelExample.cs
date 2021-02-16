using System.Windows.Forms;

namespace MainExample
{
   public partial class PanelExample : Form
   {
      public PanelExample()
      {
         InitializeComponent();
         for (var i = 0; i < 20; i++)
         {
            var aButton = new Button { Text = string.Format("Button{0}", i) };
            flowLayoutPanel1.Controls.Add(aButton);
         }
      }
   }
}