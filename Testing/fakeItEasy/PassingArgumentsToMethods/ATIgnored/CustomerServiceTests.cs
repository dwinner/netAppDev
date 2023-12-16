using FakeItEasy;

namespace PassingArgumentsToMethods.ATIgnored;

[TestFixture]
public class WhenSendingEmailToCustomersAndNoCustomersExist
{
   [SetUp]
   public void Given()
   {
      emailSender = A.Fake<ISendEmail>();
      var sut = new CustomerService(emailSender, A.Fake<ICustomerRepository>());
      sut.SendEmailToAllCustomers();
   }

   private ISendEmail emailSender;

   [Test]
   public void DoesNotSendEmail()
   {
      A.CallTo(() => emailSender.SendMail(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored, A<string>.Ignored))
         .MustNotHaveHappened();
   }
}