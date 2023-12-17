using FakeItEasy;

namespace PassingArgumentsToMethods;

[TestFixture]
public class WhenSendingEmailToAllCustomers
{
   [SetUp]
   public void Given()
   {
      emailSender = A.Fake<ISendEmail>();
      customer = new Customer { Email = "customer@email.com" };

      var customerRepository = A.Fake<ICustomerRepository>();
      A.CallTo(() => customerRepository.GetAllCustomers()).Returns(new List<Customer> { customer });

      var sut = new CustomerService(emailSender, customerRepository);
      sut.SendEmailToAllCustomers();
   }

   private ISendEmail emailSender;
   private Customer customer;

   [Test]
   public void SendsEmail()
   {
      A.CallTo(() => emailSender.SendMail("acompany@somewhere.com", customer.Email, "subject", "body"))
         .MustHaveHappened(1, Times.Exactly);
   }
}