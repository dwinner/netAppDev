// Аспекты вызовов, свойств, событий, исключений

using System;

namespace MethodBoundarySample
{
   internal static class Program
   {
      private static void Main()
      {
         var customer = new Customer { Name = "Denny", Address = "Revolution 7-93" };
         var customerService = new CustomerService();
         customerService.CustomerSuccessfullySaved += CustomerService_CustomerSuccessfullySaved;         
         customerService.Save(customer);
         customerService.CustomerSuccessfullySaved -= CustomerService_CustomerSuccessfullySaved;

         try
         {
            Console.WriteLine(customer.Company);
            customerService.FetchAll();
         }
         catch (ApplicationException appEx)
         {
            Console.WriteLine(appEx.Message);
         }
      }

      private static void CustomerService_CustomerSuccessfullySaved(object sender, EventArgs e)
      {
         Console.WriteLine("Customer successfully saved!");
      }
   }
}