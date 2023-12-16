using FakeItEasy;

namespace MHH_BasicUsage;

[TestFixture]
public class WhenSendingEmailToAllCustomers
{
   [SetUp]
   public void Given()
   {
      _emailSender = A.Fake<ISendEmail>();

      var customerRepository = A.Fake<ICustomerRepository>();
      A.CallTo(() => customerRepository.GetAllCustomers()).Returns(new List<Customer> { new() });

      var sut = new CustomerService(_emailSender, customerRepository);
      sut.SendEmailToAllCustomers();
   }

   private ISendEmail _emailSender = null!;

   [Test]
   public void SendsEmail()
   {
      A.CallTo(() => _emailSender.SendMail()).MustHaveHappened();
   }
}