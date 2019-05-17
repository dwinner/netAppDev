using System;
using System.Windows.Input;
using Northwind.Core;
using Northwind.Wpf.Infrastructure;

namespace Northwind.Wpf.ViewModels
{
   public class CustomerViewModel : ViewModel
   {
      private readonly ICustomerRepository _repository;
      private bool? _dialogResult;

      public CustomerViewModel(ICustomerRepository repository, ICommandFactory commandFactory)
      {
         if (commandFactory == null)
         {
            throw new ArgumentNullException(nameof(commandFactory));
         }

         _repository = repository ?? throw new ArgumentNullException(nameof(repository));
         SaveCommand = commandFactory.CreateCommand(Save);
         Customer = new Customer();
      }

      public Customer Customer { get; }

      public ICommand SaveCommand { get; }

      public bool? DialogResult
      {
         get => _dialogResult;
         set
         {
            _dialogResult = value;
            OnPropertyChanged();
         }
      }

      public void Save(object paramer)
      {
         _repository.Add(Customer);
         DialogResult = true;
      }
   }
}