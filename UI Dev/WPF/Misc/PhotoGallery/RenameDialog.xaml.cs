using System.Windows;

namespace PhotoGallery
{
   public partial class RenameDialog
   {
      private readonly string _oldFilename;

      public RenameDialog()
      {
         InitializeComponent();
         TextBox.Text = _oldFilename;
         TextBox.Focus();
      }

      public RenameDialog(string oldFilename)
      {
         InitializeComponent();
         _oldFilename = oldFilename;
         TextBox.Text = oldFilename;
         TextBox.Focus();
      }

      public string NewFilename { get; private set; }

      private void OnTextChanged(object sender, RoutedEventArgs e)
      {
         NewFilename = TextBox.Text;
      }

      private void OnOk(object sender, RoutedEventArgs e)
      {
         DialogResult = true;
      }
   }
}