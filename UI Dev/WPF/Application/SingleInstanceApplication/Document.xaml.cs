using System;
using System.IO;
using System.Windows;

namespace SingleInstanceApplication
{
   public partial class Document
   {
      private DocumentReference _docRef;

      public Document()
      {
         InitializeComponent();
      }

      public void LoadFile(DocumentReference docRef)
      {
         _docRef = docRef;
         Content = File.ReadAllText(docRef.Name);
         Title = docRef.Name;
      }

      protected override void OnClosed(EventArgs e)
      {
         base.OnClosed(e);

         ((WpfApplication)Application.Current).Documents.Remove(_docRef);
      }
   }
}