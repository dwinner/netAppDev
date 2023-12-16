using FakeItEasy;

namespace MustNotHaveHappened;

[TestFixture]
public class WhenSendingEmailToAllCustomersAndNoCustomersExist
{
   [SetUp]
   public void Given()
   {
      emailSender = A.Fake<ISendEmail>();

      var customerRepository = A.Fake<ICustomerRepository>();
      A.CallTo(() => customerRepository.GetAllCustomers()).Returns(new List<Customer>());

      var sut = new CustomerService(emailSender, customerRepository);
      sut.SendEmailToAllCustomers();
   }

   private ISendEmail emailSender;

   [Test]
   public void DoesNotSendAnyEmail()
   {
      A.CallTo(() => emailSender.SendMail()).MustNotHaveHappened();
   }
}