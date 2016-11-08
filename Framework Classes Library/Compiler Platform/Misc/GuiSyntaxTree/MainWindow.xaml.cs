using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using GuiSyntaxTree.ViewModels;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Win32;

namespace GuiSyntaxTree
{
   public partial class MainWindow : INotifyPropertyChanged
   {
      private SyntaxNodeViewModel _selectedNode;

      public MainWindow()
      {
         InitializeComponent();
         DataContext = this;
      }

      public ObservableCollection<SyntaxNodeViewModel> Nodes { get; } = new ObservableCollection<SyntaxNodeViewModel>();

      public SyntaxNodeViewModel SelectedNode
      {
         get { return _selectedNode; }
         set
         {
            _selectedNode = value;
            OnPropertyChanged();
         }
      }

      public event PropertyChangedEventHandler PropertyChanged;

      private async void OnLoad(object sender, RoutedEventArgs e)
      {
         var dialog = new OpenFileDialog { Filter = "C# Code (.cs)|*.cs" };
         if (dialog.ShowDialog() == true)
         {
            var code = File.ReadAllText(dialog.FileName);
            var tree = CSharpSyntaxTree.ParseText(code);
            var node = await tree.GetRootAsync().ConfigureAwait(true);
            Nodes.Add(new SyntaxNodeViewModel(node));
         }
      }

      private void OnSelectSyntaxNode(object sender, RoutedPropertyChangedEventArgs<object> e)
         => SelectedNode = e.NewValue as SyntaxNodeViewModel;

      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
   }
}