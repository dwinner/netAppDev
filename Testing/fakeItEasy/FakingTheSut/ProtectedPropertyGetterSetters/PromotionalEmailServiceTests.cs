using FakeItEasy;

namespace FakingTheSut.ProtectedPropertyGetterSetters;

public class WhenSendingPromotionalEmail
{
   private const string Subject = "Subject";
   private const string Body = "Body";
   private const string FromAddress = "fromAddress";
   private List<Customer> _customers = null!;
   private ISendEmail _emailSender = null!;

   [SetUp]
   public void Given()
   {
      _customers = new List<Customer>
      {
         new() { Email = "customer1@email.com" },
         new() { Email = "customer2@email.com" }
      };

      _emailSender = A.Fake<ISendEmail>();
      var customerRepository = A.Fake<ICustomerRepository>();
      A.CallTo(() => customerRepository.GetAllCustomersWithOrderTotalsOfOneHundredOrGreater()).Returns(_customers);

      var sut = A.Fake<PromotionalEmailService>(x =>
         x.WithArgumentsForConstructor(() => new PromotionalEmailService(customerRepository, _emailSender)));
      A.CallTo(sut).Where(x => x.Method.Name == "get_FromEmailAddress")
         .WithReturnType<string>().Returns(FromAddress);
      sut.SendEmail(Subject, Body);
   }

   [Test]
   public void SendsTheCorrectAmountOfTimes()
   {
      A.CallTo(() => _emailSender.SendMail(FromAddress, A<string>._, Subject, Body))
         .MustHaveHappened(_customers.Count, Times.Exactly);
   }

   [Test]
   public void SendsToCorrectCustomers()
   {
      foreach (var customer in _customers)
      {
         A.CallTo(() => _emailSender.SendMail(FromAddress, customer.Email, Subject, Body))
            .MustHaveHappened(1, Times.Exactly);
      }
   }
}