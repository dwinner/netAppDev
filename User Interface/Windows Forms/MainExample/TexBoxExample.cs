using System.Windows.Forms;
using MainExample.Properties;

namespace MainExample
{
   public partial class TexBoxExample : Form
   {
      public TexBoxExample()
      {
         InitializeComponent();
         richTextBox1.Rtf = Resources.Hello;
      }
   }
}
