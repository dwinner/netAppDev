using System;
using System.Collections.Generic;
using System.Windows.Input;
using Northwind.Core;
using Northwind.Wpf.Infrastructure;

namespace Northwind.Wpf.ViewModels
{
   public class MainViewModel : ViewModel
   {
      private readonly ICustomerRepository _repository;
      private readonly IViewFactory _viewFactory;

      public MainViewModel(ICustomerRepository repository, IViewFactory viewFactory, ICommandFactory commandFactory)
      {
         if (commandFactory == null)
         {
            throw new ArgumentNullException(nameof(commandFactory));
         }

         _repository = repository ?? throw new ArgumentNullException(nameof(repository));
         _viewFactory = viewFactory ?? throw new ArgumentNullException(nameof(viewFactory));

         CreateCustomerCommand = commandFactory.CreateCommand(CreateCustomer);
      }

      public IEnumerable<Customer> Customers => _repository.GetAll();

      public ICommand CreateCustomerCommand { get; }

      private void CreateCustomer(object param)
      {
         var customerView = _viewFactory.CreateView<ICustomerView>();
         if (customerView.ShowDialog() == true)
         {
            // Refresh the list
            OnPropertyChanged(nameof(Customers));
         }
      }
   }
}