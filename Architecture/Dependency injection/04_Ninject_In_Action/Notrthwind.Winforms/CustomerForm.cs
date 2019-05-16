using System;
using System.Windows.Forms;
using Northwind.Core;

namespace Notrthwind.Winforms
{
   public partial class CustomerForm : Form
   {
      private readonly ICustomerRepository _repository;

      public CustomerForm(ICustomerRepository repository)
      {
         _repository = repository ?? throw new ArgumentNullException(nameof(repository));
         InitializeComponent();
         customerBindingSource.Add(new Customer());
      }

      private void OnSave(object sender, EventArgs e)
      {
         customerBindingSource.EndEdit();
         var customer = customerBindingSource.Current as Customer;
         _repository.Add(customer);
      }
   }
}