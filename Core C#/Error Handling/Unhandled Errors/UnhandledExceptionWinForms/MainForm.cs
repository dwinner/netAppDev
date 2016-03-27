using System;
using System.Text;
using System.Windows.Forms;

namespace HowToCSharp.Ch04.UnhandledExceptionWinForms
{
   public partial class MainForm : Form
   {
      public MainForm()
      {
         InitializeComponent();
         Application.ThreadException += Application_ThreadException;
      }

      void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
      {
         StringBuilder sb = new StringBuilder();
         sb.AppendLine("Trapped unhandled exception");
         sb.AppendLine(e.Exception.ToString());

         MessageBox.Show(sb.ToString());
      }

      private void Button1Click(object sender, EventArgs e)
      {
         throw new InvalidOperationException("Oops");
      }
   }
}
