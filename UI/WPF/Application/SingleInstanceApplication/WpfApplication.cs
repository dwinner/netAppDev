using System.Collections.ObjectModel;
using System.Windows;

namespace SingleInstanceApplication
{
   public class WpfApplication : Application
   {
      public WpfApplication()
      {
         Documents = new ObservableCollection<DocumentReference>();
      }

      public ObservableCollection<DocumentReference> Documents { get; private set; }

      protected override void OnStartup(StartupEventArgs e)
      {
         base.OnStartup(e);

         // Load the main window.
         var list = new DocumentList();
         MainWindow = list;
         list.Show();

         // Load the document that was specified as an argument.
         if (e.Args.Length > 0)
            ShowDocument(e.Args[0]);
      }

      public void ShowDocument(string filename)
      {
         try
         {
            var doc = new Document();
            var docRef = new DocumentReference(doc, filename);
            doc.LoadFile(docRef);
            doc.Owner = MainWindow;
            doc.Show();
            doc.Activate();
            Documents.Add(docRef);
         }
         catch
         {
            MessageBox.Show("Could not load document.");
         }
      }
   }
}