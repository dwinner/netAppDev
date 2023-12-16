using FakeItEasy;

namespace Exceptions;

public class WhenSendingEmailToAllCustomersAndThereIsAnException
{
   [SetUp]
   public void Given()
   {
      var customerRepository = A.Fake<ICustomerRepository>();
      var customers = new List<Customer> { new() { EmailAddress = "someone@somewhere.com" } };
      A.CallTo(() => customerRepository.GetAllCustomers()).Returns(customers);

      var emailSender = A.Fake<ISendEmail>();
      A.CallTo(() => emailSender.SendMail(customers)).Throws(new BadCustomerEmailException());

      var sut = new CustomerService(emailSender, customerRepository);
      sut.SendEmailToAllCustomers();
   }
}