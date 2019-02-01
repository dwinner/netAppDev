using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using MoreLinq;

namespace Documents
{
   public partial class MadLibs
   {
      public MadLibs()
      {
         InitializeComponent();
      }

      private void OnLoaded(object sender, RoutedEventArgs e)
      {
         // Clear grid of text entry controls.
         GridWords.Children.Clear();

         // Look at paragraphs and then the spans inside
         Document.Blocks.OfType<Paragraph>().SelectMany(paragraph => paragraph.Inlines.OfType<Span>()).ForEach(span =>
         {
            var newRowDefinition = new RowDefinition();
            GridWords.RowDefinitions.Add(newRowDefinition);
            var rowIndex = GridWords.RowDefinitions.Count - 1;

            var newLabel = new Label {Content = string.Format("{0}:", span.Tag)};
            Grid.SetColumn(newLabel, 0);
            Grid.SetRow(newLabel, rowIndex);
            GridWords.Children.Add(newLabel);

            var newTextBox = new TextBox();
            Grid.SetColumn(newTextBox, 1);
            Grid.SetRow(newTextBox, rowIndex);
            GridWords.Children.Add(newTextBox);

            newTextBox.Tag = span.Inlines.FirstInline;
         });
      }

      private void OnGenerate(object sender, RoutedEventArgs e)
      {
         GridWords.Children
            .OfType<UIElement>()
            .Where(element => Grid.GetColumn(element) == 1)
            .Cast<TextBox>()
            .Where(textBox => !string.IsNullOrEmpty(textBox.Text))
            .ForEach(textBox => ((Run) textBox.Tag).Text = textBox.Text);
         DocViewer.Visibility = Visibility.Visible;
      }
   }
}