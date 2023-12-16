using FakeItEasy;

namespace OutAndRefParameters;

[TestFixture]
public class WhenSendingEmailToAllCustomers
{
   [SetUp]
   public void Given()
   {
      var customerRepository = A.Fake<ICustomerRepository>();
      var customers = new List<Customer> { new() { EmailAddress = "someone@somewhere.com" } };
      A.CallTo(() => customerRepository.GetAllCustomers(out customers)).AssignsOutAndRefParameters(customers);

      var sut = new CustomerService(A.Fake<ISendEmail>(), customerRepository);
      sut.SendEmailToAllCustomers();
   }
}