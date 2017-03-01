using System.Drawing;
using System.Windows.Forms;

namespace SplashScreenWinForms
{
   public sealed partial class SplashScreen : Form
   {
      public SplashScreen(Image image)
      {
         InitializeComponent();
         FormBorderStyle = FormBorderStyle.None;
         BackgroundImage = image;
         Size = image.Size;
         labelStatus.BackColor = Color.Transparent;
      }

      public void UpdateStatus(string status, int percent)
      {
         if (InvokeRequired)
         {
            Invoke(new MethodInvoker(delegate { UpdateStatus(status, percent); }));
         }
         else
         {
            progressBar.Value = percent;
            labelStatus.Text = status;
         }
      }
   }
}