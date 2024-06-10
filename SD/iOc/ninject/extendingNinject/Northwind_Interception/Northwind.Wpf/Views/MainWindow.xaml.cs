using System;
using Northwind.Wpf.Infrastructure;
using Northwind.Wpf.ViewModels;

namespace Northwind.Wpf.Views
{
   public partial class MainWindow : IMainView
   {
      public MainWindow(MainViewModel viewModel)
      {
         DataContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
         InitializeComponent();
      }
   }
}