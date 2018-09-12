using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfAutoCompleteBoxSample
{
   public partial class MainWindow
   {
      private readonly IList<string> _countries = new List<string>
      {
         "test_annotation",
         "CString_TEST",
         "teststlport",
         "STL_TEST",
         "PVS-Studio Documentation",
         "UCharTest",
         "PreprocessorDefinitions",
         "TestGdiPlusH",
         "MacroSupression",
         "gtest",
         "TestMIDL",
         "CLRTest",
         "TestMacro_CPPRTTI",
         "Unload_Project",
         "CLine",
         "TreeOfFolders",
         "BuildUnchecked",
         "TestTemplates",
         "PreprocessingHang",
         "OneFileIntoManyProjects",
         "MultilinePragma",
         "PropertySheets",
         "StdAfxSolution",
         "SolutionTree",
         "HPC_Training",
         "OmpSCR",
         "libsiftfast-1.0-src",
         "notepadPlus",
         "Loki",
         "ABackup",
         "spvolren",
         "graphics3D",
         "VirtualDub",
         "StrongDC",
         "miranda32",
         "CamStudio",
         "ffdshow_2010",
         "StarEngine",
         "SObjectizer"
      };

      private string _selectedSolutionToSearch;

      public MainWindow()
      {
         InitializeComponent();
         SolutionAutoCompleteBox.ItemsSource = _countries;
      }

      private void OnCompleteSelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         _selectedSolutionToSearch = e.AddedItems.Cast<string>().FirstOrDefault();
      }

      private void SolutionAutoCompleteBox_OnPreviewKeyUp(object sender, KeyEventArgs e)
      {
         if (e.Key == Key.Return)
         {
            MessageBox.Show(this, _selectedSolutionToSearch ?? "None");
            e.Handled = true;            
         }
      }
   }
}