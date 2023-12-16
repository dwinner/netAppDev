namespace Exceptions;

public class CustomerService(ISendEmail emailSender, ICustomerRepository customerRepository)
{
   public void SendEmailToAllCustomers()
   {
      var customers = customerRepository.GetAllCustomers();
      try
      {
         emailSender.SendMail(customers);
      }
      catch (BadCustomerEmailException ex)
      {
         //do something here like write to a log file, etc...
      }
   }
}