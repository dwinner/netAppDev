using System;
using System.Windows.Forms;
using Northwind.Core;

namespace Notrthwind.Winforms
{
   public partial class MainForm : Form
   {
      private readonly IFormFactory _formFactory;
      private readonly ICustomerRepository _repository;

      public MainForm(ICustomerRepository repository, IFormFactory formFactory)
      {
         _repository = repository ?? throw new ArgumentNullException(nameof(repository));
         _formFactory = formFactory ?? throw new ArgumentNullException(nameof(formFactory));
         InitializeComponent();
      }

      protected override void OnLoad(EventArgs e)
      {
         base.OnLoad(e);
         LoadCustomers();
      }

      private void LoadCustomers() => customerBindingSource.DataSource = _repository.GetAll();

      private void OnCreate(object sender, EventArgs e)
      {
         var customerForm = _formFactory.Create<CustomerForm>();
         if (customerForm.ShowDialog(this) == DialogResult.OK)
         {
            LoadCustomers();
         }
      }
   }
}