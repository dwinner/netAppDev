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

      private void cmdBold_Click(object sender, RoutedEventArgs e)
      {
         if (RichTextBox.Selection.Text == "")
         {
            var fontWeight = RichTextBox.Selection.Start.Paragraph.FontWeight;
            if (fontWeight == FontWeights.Bold)
               fontWeight = FontWeights.Normal;
            else
               fontWeight = FontWeights.Bold;

            RichTextBox.Selection.Start.Paragraph.FontWeight = fontWeight;
         }
         else
         {
            var obj = RichTextBox.Selection.GetPropertyValue(TextElement.FontWeightProperty);
            if (obj == DependencyProperty.UnsetValue)
            {
               var range = new TextRange(RichTextBox.Selection.Start,
                  RichTextBox.Selection.Start);
               obj = range.GetPropertyValue(TextElement.FontWeightProperty);
            }

            var fontWeight = (FontWeight) obj;

            if (fontWeight == FontWeights.Bold)
               fontWeight = FontWeights.Normal;
            else
               fontWeight = FontWeights.Bold;

            RichTextBox.Selection.ApplyPropertyValue(
               TextElement.FontWeightProperty, fontWeight);
         }
      }

      private void cmdShowXAML_Click(object sender, RoutedEventArgs e)
      {
         UpdateMarkupDisplay();
      }

      private void UpdateMarkupDisplay()
      {
         TextRange range;

         range = new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd);

         var stream = new MemoryStream();
         range.Save(stream, DataFormats.Xaml);
         stream.Position = 0;

         var r = new StreamReader(stream);

         FlowDocumentMarkupTextBox.Text = r.ReadToEnd();
         r.Close();
         stream.Close();
      }


      private void cmdOpen_Click(object sender, RoutedEventArgs e)
      {
         var openFile = new OpenFileDialog();
         openFile.Filter = "XAML Files (*.xaml)|*.xaml|RichText Files (*.rtf)|*.rtf|All Files (*.*)|*.*";

         if (openFile.ShowDialog() == true)
         {
            // Create a TextRange around the entire document.
            var documentTextRange = new TextRange(
               RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd);

            using (var fs = File.Open(openFile.FileName, FileMode.Open))
            {
               if (Path.GetExtension(openFile.FileName).ToLower() == ".rtf")
                  documentTextRange.Load(fs, DataFormats.Rtf);
               else
                  documentTextRange.Load(fs, DataFormats.Xaml);
            }
         }
      }

      private void cmdSave_Click(object sender, RoutedEventArgs e)
      {
         var saveFile = new SaveFileDialog();
         saveFile.Filter = "XAML Files (*.xaml)|*.xaml|RichText Files (*.rtf)|*.rtf|All Files (*.*)|*.*";

         if (saveFile.ShowDialog() == true)
         {
            // Create a TextRange around the entire document.
            var documentTextRange = new TextRange(
               RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd);

            // If this file exists, it's overwritten.
            using (var fs = File.Create(saveFile.FileName))
            {
               if (Path.GetExtension(saveFile.FileName).ToLower() == ".rtf")
                  documentTextRange.Save(fs, DataFormats.Rtf);
               else
                  documentTextRange.Save(fs, DataFormats.Xaml);
            }
         }
      }

      private void cmdNew_Click(object sender, RoutedEventArgs e)
      {
         RichTextBox.Document = new FlowDocument();
      }

      private void OnRichTextBox_MouseDown(object sender, MouseEventArgs e)
      {
         if (e.RightButton == MouseButtonState.Pressed)
         {
            var location = RichTextBox.GetPositionFromPoint(Mouse.GetPosition(RichTextBox), true);
            var word = WordBreaker.GetWordRange(location);
            FlowDocumentMarkupTextBox.Text = word.Text;
         }
      }
   }
}