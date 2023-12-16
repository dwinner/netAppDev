using FakeItEasy;

namespace FakingTheSut;

[TestFixture]
public class WhenSendingPromotionalEmail
{
   [SetUp]
   public void Given()
   {
      _customers = new List<Customer>
      {
         new() { Email = "customer1@email.com" },
         new() { Email = "customer2@email.com" }
      };

      var customerRepository = A.Fake<ICustomerRepository>();
      A.CallTo(() => customerRepository.GetAllCustomersWithOrderTotalsOfOneHundredOrGreater()).Returns(_customers);

      _emailSender = A.Fake<ISendEmail>();

      var sut = A.Fake<AdminEmailService>(x =>
         x.WithArgumentsForConstructor(() => new AdminEmailService(customerRepository, _emailSender)));
      A.CallTo(sut).Where(x => x.Method.Name == "GetFromEmailAddress")
         .WithReturnType<string>().Returns(TheConfiguredFromAddress);

      sut.SendPromotionalEmail(EmailSubject, EmailBody);
   }

   private ISendEmail _emailSender = null!;
   private List<Customer> _customers = null!;
   private const string EmailSubject = "EmailSubject";
   private const string EmailBody = "EmailBody";
   private const string TheConfiguredFromAddress = "someone@somecompany.com";

   [Test]
   public void SendsTheCorrectAmountOfTimes()
   {
      A.CallTo(() => _emailSender.SendMail(TheConfiguredFromAddress, A<string>._, EmailSubject, EmailBody))
         .MustHaveHappened(2, Times.Exactly);
   }

   [Test]
   public void SendsTheCorrectAmountOfTimesBetter()
   {
      A.CallTo(() => _emailSender.SendMail(TheConfiguredFromAddress, A<string>._, EmailSubject, EmailBody))
         .MustHaveHappened(_customers.Count(), Times.Exactly);
   }

   [Test]
   public void SendsToCorrectCustomers()
   {
      A.CallTo(() => _emailSender.SendMail(TheConfiguredFromAddress, _customers[0].Email, EmailSubject, EmailBody))
         .MustHaveHappened(1, Times.Exactly);
      A.CallTo(() => _emailSender.SendMail(TheConfiguredFromAddress, _customers[1].Email, EmailSubject, EmailBody))
         .MustHaveHappened(1, Times.Exactly);
   }

   [Test]
   public void SendsToCorrectCustomersBetter()
   {
      foreach (var customer in _customers)
      {
         A.CallTo(() => _emailSender.SendMail(TheConfiguredFromAddress, customer.Email, EmailSubject, EmailBody))
            .MustHaveHappened(1, Times.Exactly);
      }
   }
}