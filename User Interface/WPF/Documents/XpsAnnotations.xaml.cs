using System;
using System.IO;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Annotations;
using System.Windows.Annotations.Storage;
using System.Windows.Xps.Packaging;

namespace Documents
{
   public partial class XpsAnnotations
   {
      private XpsDocument _doc;
      private AnnotationService _service;

      public XpsAnnotations()
      {
         InitializeComponent();
      }

      private void OnLoaded(object sender, RoutedEventArgs e)
      {
         _doc = new XpsDocument("ch19.xps", FileAccess.ReadWrite);
         DocViewer.Document = _doc.GetFixedDocumentSequence();
         _service = AnnotationService.GetService(DocViewer);
         if (_service != null)
            return;

         var annotationUri = PackUriHelper.CreatePartUri(new Uri("AnnotationStream", UriKind.Relative));
         var package = PackageStore.GetPackage(_doc.Uri);
         var annotationPart = package.PartExists(annotationUri)
            ? package.GetPart(annotationUri)
            : package.CreatePart(annotationUri, "Annotations/Stream");

         // Load annotations from the package.
         if (annotationPart != null)
         {
            AnnotationStore store = new XmlStreamStore(annotationPart.GetStream());
            _service = new AnnotationService(DocViewer);
            _service.Enable(store);
         }
      }

      private void OnUnloaded(object sender, RoutedEventArgs e)
      {
         if (_service != null && _service.IsEnabled)
         {
            // Flush annotations to stream.
            _service.Store.Flush();

            // Disable annotations.
            _service.Disable();
         }

         _doc.Close();
      }
   }
}