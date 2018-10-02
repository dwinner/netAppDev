using System;
using System.IO;
using System.Security.Principal;
using System.Windows;
using System.Windows.Annotations;
using System.Windows.Annotations.Storage;
using System.Windows.Documents;

namespace Documents
{
   public partial class FlowDocumentAnnotations
   {
      private Stream _annotationStream;
      private AnnotationService _service;

      public FlowDocumentAnnotations()
      {
         InitializeComponent();
      }

      private void OnWindowLoaded(object sender, RoutedEventArgs e)
      {
         var identity = WindowsIdentity.GetCurrent();
         Resources["AuthorName"] = identity.Name;

         _service = AnnotationService.GetService(DocReader);
         if (_service == null)
         {
            // Create a stream for the annotations to be stored in.
            //AnnotationStream =
            // new FileStream("annotations.xml", FileMode.OpenOrCreate);
            _annotationStream = new MemoryStream();

            // Create the on the document container. 
            _service = new AnnotationService(DocReader);

            // Create the AnnotationStore using the stream.
            AnnotationStore store = new XmlStreamStore(_annotationStream);

            // Enable annotations.
            _service.Enable(store);
         }
      }

      private void OnWindowUnloaded(object sender, RoutedEventArgs e)
      {
         if (_service != null && _service.IsEnabled)
         {
            // Flush annotations to stream.
            _service.Store.Flush();

            // Disable annotations.
            _service.Disable();
            _annotationStream.Close();
         }
      }

      private void OnShowAllAnotations(object sender, RoutedEventArgs e)
      {
         var annotations = _service.Store.GetAnnotations();
         foreach (var annotation in annotations)
            if (annotation.Cargos.Count > 1)
            {
               // Decode the note text.
               var base64Text = annotation.Cargos[1].Contents[0].InnerText;
               var decoded = Convert.FromBase64String(base64Text);

               // Write the decoded text to a stream.
               string annotationXaml;
               using (var stream = new MemoryStream(decoded))
               using (var reader = new StreamReader(stream))
               {
                  annotationXaml = reader.ReadToEnd();
               }

               MessageBox.Show(annotationXaml);

               // Get the annotated text.
               var anchorInfo = AnnotationHelper.GetAnchorInfo(_service, annotation);
               var resolvedAnchor = anchorInfo.ResolvedAnchor as TextAnchor;
               if (resolvedAnchor != null)
               {
                  var startPointer = (TextPointer) resolvedAnchor.BoundingStart;
                  var endPointer = (TextPointer) resolvedAnchor.BoundingEnd;
                  var range = new TextRange(startPointer, endPointer);
                  MessageBox.Show(range.Text);
               }
            }

         #region Code to print annotations

         //PrintDialog dialog = new PrintDialog();
         //bool? result = dialog.ShowDialog();
         //if (result != null && result.Value)
         //{
         //    System.Windows.Xps.XpsDocumentWriter writer = System.Printing.PrintQueue.CreateXpsDocumentWriter(dialog.PrintQueue);

         //    AnnotationDocumentPaginator adp = new AnnotationDocumentPaginator(
         //                            ((IDocumentPaginatorSource)docReader.Document).DocumentPaginator,
         //                            service.Store);
         //    writer.Write(adp);
         //}

         #endregion
      }
   }
}