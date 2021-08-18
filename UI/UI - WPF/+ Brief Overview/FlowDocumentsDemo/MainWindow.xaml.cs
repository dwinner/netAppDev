using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using Microsoft.Win32;

namespace FlowDocumentsDemo
{
   public partial class MainWindow
   {
      private dynamic _activedocumentReader;

      private List<object> _documentReaders;

      public MainWindow()
      {
         InitializeComponent();
         DataContext = this;
      }

      public IEnumerable<object> Readers => GetReaders();

      private void OnOpenDocument(object sender, RoutedEventArgs e)
      {
         try
         {
            var dlg = new OpenFileDialog
            {
               DefaultExt = "*.xaml",
               InitialDirectory = Environment.CurrentDirectory
            };
            if (dlg.ShowDialog() == true)
               using (var xamlFile = File.OpenRead(dlg.FileName))
               {
                  var doc = XamlReader.Load(xamlFile) as FlowDocument;
                  if (doc != null)
                  {
                     _activedocumentReader.Document = doc;
                     _activedocumentReader.Visibility = Visibility.Visible;
                  }
               }
         }
         catch (XamlParseException ex)
         {
            MessageBox.Show($"Check content for a Flow document, {ex.Message}");
         }
      }

      private void OnReaderSelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         dynamic item = (sender as ComboBox)?.SelectedItem;

         if (_activedocumentReader != null)
            _activedocumentReader.Visibility = Visibility.Collapsed;
         _activedocumentReader = item?.Instance;
      }

      private IEnumerable<object> GetReaders()
         => _documentReaders ?? (_documentReaders =
               LogicalTreeHelper.GetChildren(Grid1).OfType<FrameworkElement>()
                  .Where(
                     el =>
                        el.GetType()
                           .GetProperties()
                           .Any(pi => pi.Name == "Document"))
                  .Select(el => new
                  {
                     el.GetType().Name,
                     Instance = el
                  }).Cast<object>().ToList());
   }
}