using FakeItEasy;

namespace ReturnsNextFromSequence;

[TestFixture]
public class WhenSendingEmailToAllCustomers
{
   [SetUp]
   public void Given()
   {
      var customerRepository = A.Fake<ICustomerRepository>();
      A.CallTo(() => customerRepository.GetAllCustomers()).Returns(new List<Customer>
      {
         new() { Email = "customer1email@somewhere.com" },
         new() { Email = "customer2email@somewhere.com" }
      });

      var guidProvider = A.Fake<IProvideNewGuids>();
      A.CallTo(() => guidProvider.GenerateNewId()).ReturnsNextFromSequence(guid1, guid2);

      var emailSender = A.Fake<ISendEmail>();

      var sut = new CustomerService(emailSender, customerRepository, guidProvider);
      sut.SendEmailToAllCustomers();
   }

   private readonly Guid guid1 = Guid.NewGuid();
   private readonly Guid guid2 = Guid.NewGuid();
}