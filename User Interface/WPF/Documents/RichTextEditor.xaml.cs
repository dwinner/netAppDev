using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Microsoft.Win32;

namespace Documents
{
   public partial class RichTextEditor
   {
      public RichTextEditor()
      {
         InitializeComponent();
      }

      private void OnBold(object sender, RoutedEventArgs e)
      {
         if (RichTextBox.Selection.Text == string.Empty && RichTextBox.Selection.Start.Paragraph != null)
         {
            var fontWeight = RichTextBox.Selection.Start.Paragraph.FontWeight;
            fontWeight = fontWeight == FontWeights.Bold ? FontWeights.Normal : FontWeights.Bold;
            RichTextBox.Selection.Start.Paragraph.FontWeight = fontWeight;
         }
         else
         {
            var obj = RichTextBox.Selection.GetPropertyValue(TextElement.FontWeightProperty);
            if (obj == DependencyProperty.UnsetValue)
            {
               var range = new TextRange(RichTextBox.Selection.Start, RichTextBox.Selection.Start);
               obj = range.GetPropertyValue(TextElement.FontWeightProperty);
            }

            var fontWeight = (FontWeight) obj;
            fontWeight = fontWeight == FontWeights.Bold ? FontWeights.Normal : FontWeights.Bold;
            RichTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, fontWeight);
         }
      }

      private void OnShowXaml(object sender, RoutedEventArgs e)
      {
         UpdateMarkupDisplay();
      }

      private void UpdateMarkupDisplay()
      {
         var range = new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd);
         using (var memoryStream = new MemoryStream())
         {
            range.Save(memoryStream, DataFormats.Xaml);
            memoryStream.Position = 0;
            using (var streamReader = new StreamReader(memoryStream))
            {
               FlowDocumentMarkupTextBox.Text = streamReader.ReadToEnd();
            }
         }
      }

      private void OnOpen(object sender, RoutedEventArgs e)
      {
         var openFile = new OpenFileDialog
         {
            Filter = "XAML Files (*.xaml)|*.xaml|RichText Files (*.rtf)|*.rtf|All Files (*.*)|*.*"
         };

         if (openFile.ShowDialog() == true)
         {
            // Create a TextRange around the entire document.
            var documentTextRange = new TextRange(
               RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd);

            using (var fileStream = File.Open(openFile.FileName, FileMode.Open))
            {
               var extension = Path.GetExtension(openFile.FileName);
               documentTextRange.Load(fileStream,
                  extension != null && extension.ToLower() == ".rtf" ? DataFormats.Rtf : DataFormats.Xaml);
            }
         }
      }

      private void OnSave(object sender, RoutedEventArgs e)
      {
         var saveFile = new SaveFileDialog
         {
            Filter = "XAML Files (*.xaml)|*.xaml|RichText Files (*.rtf)|*.rtf|All Files (*.*)|*.*"
         };

         if (saveFile.ShowDialog() == true)
         {
            // Create a TextRange around the entire document.
            var documentTextRange = new TextRange(
               RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd);

            // If this file exists, it's overwritten.
            using (var fileStream = File.Create(saveFile.FileName))
            {
               var extension = Path.GetExtension(saveFile.FileName);
               documentTextRange.Save(fileStream,
                  extension != null && extension.ToLower() == ".rtf" ? DataFormats.Rtf : DataFormats.Xaml);
            }
         }
      }

      private void OnNew(object sender, RoutedEventArgs e)
      {
         RichTextBox.Document = new FlowDocument();
      }

      private void OnRichTextBox_MouseDown(object sender, MouseEventArgs e)
      {
         if (e.RightButton == MouseButtonState.Pressed)
         {
            var location = RichTextBox.GetPositionFromPoint(Mouse.GetPosition(RichTextBox), true);
            var word = location.GetWordRange();
            FlowDocumentMarkupTextBox.Text = word.Text;
         }
      }
   }
}