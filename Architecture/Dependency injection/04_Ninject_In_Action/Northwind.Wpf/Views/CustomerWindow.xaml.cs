using System;
using Northwind.Wpf.Infrastructure;
using Northwind.Wpf.ViewModels;

namespace Northwind.Wpf.Views
{
   public partial class CustomerWindow : ICustomerView
   {
      public CustomerWindow(CustomerViewModel viewModel)
      {
         DataContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
         InitializeComponent();
      }
   }
}