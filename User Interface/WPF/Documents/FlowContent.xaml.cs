using System.Windows;
using System.Windows.Documents;

namespace Documents
{
   public partial class FlowContent
   {
      public FlowContent()
      {
         InitializeComponent();
      }

      private void OnCreateDynamicDocument(object sender, RoutedEventArgs e)
      {
         // Create first part of sentence.
         var runFirst = new Run {Text = "Hello world of "};

         // Create bolded text.
         var bold = new Bold();
         var runBold = new Run {Text = "dynamically generated"};
         bold.Inlines.Add(runBold);

         // Create last part of sentence.
         var runLast = new Run {Text = " documents"};

         // Add three parts of sentence to a paragraph, in order.
         var paragraph = new Paragraph();
         paragraph.Inlines.Add(runFirst);
         paragraph.Inlines.Add(bold);
         paragraph.Inlines.Add(runLast);

         // Create a document and add this paragraph.
         var document = new FlowDocument();
         document.Blocks.Add(paragraph);

         // Show the document.
         DocViewer.Document = document;
      }
   }
}