using FakeItEasy;

namespace ThatMatches;

[TestFixture]
public class WhenSendingEmailToAllCustomers
{
   [SetUp]
   public void Given()
   {
      emailSender = A.Fake<ISendEmail>();
      var customerRepository = A.Fake<ICustomerRepository>();
      A.CallTo(() => customerRepository.GetAllCustomers())
         .Returns(new List<Customer> { new() { Email = customersEmail } });
      var sut = new CustomerService(emailSender, customerRepository);
      sut.SendEmailToAllCustomers();
   }

   private ISendEmail emailSender;
   private const string customersEmail = "somecustomer@somewhere.com";

   [Test]
   public void SendsEmail()
   {
      A.CallTo(() => emailSender.SendMail(A<Email>.That.Matches(email => email.To == customersEmail)))
         .MustHaveHappened(1, Times.Exactly);
   }
}