namespace AopXaml
{
   using PostSharp.Patterns.Collections;
   using PostSharp.Patterns.Recording;
   
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();

         // Register our custom operation formatter.
         RecordingServices.OperationFormatter = new MyOperationFormatter(RecordingServices.OperationFormatter);

         // Create initial data.
         var customerViewModel = new CustomerViewModel
         {
            Customer = new CustomerModel
            {
               FirstName = "Jan",
               LastName = "Novak",
               Addresses = new AdvisableCollection <AddressModel>
               {
                  new AddressModel
                  {
                     Line1 = "Saldova 1G",
                     Town = "Prague"
                  },
                  new AddressModel
                  {
                     Line1 = "Tyrsova 25",
                     Town = "Brno"
                  },
                  new AddressModel
                  {
                     Line1 = "Pivorarka 154",
                     Town = "Pilsen"
                  }
               }
            }
         };

         customerViewModel.Customer.PrincipalAddress = customerViewModel.Customer.Addresses[0];

         // Clear the initialization steps from the recorder.
         RecordingServices.DefaultRecorder.Clear();

         DataContext = customerViewModel;
      }
   }
}