using FakeItEasy;

namespace DealingWithObject;

[TestFixture]
public class WhenSendingEmailToPreferredCustomers
{
   [SetUp]
   public void Given()
   {
      _emailSender = A.Fake<ISendEmail>();
      _customers = new List<Customer> { new() { Email = "customer1@email.com", IsPreferred = true } };
      _customerRepository = A.Fake<ICustomerRepository>();
      A.CallTo(() => _customerRepository.GetAllCustomers()).Returns(_customers);
      var sut = new CustomerService(_emailSender, _customerRepository);
      sut.SendEmailToPreferredCustomers();
   }

   private List<Customer> _customers = null!;
   private ISendEmail _emailSender = null!;
   private ICustomerRepository _customerRepository = null!;

   [Test]
   public void SendsEmail()
   {
      A.CallTo(() =>
            _emailSender.SendMail(A<Email>.That.Matches(x =>
               (x.EmailType as PreferredCustomerEmail).Email == _customers[0].Email)))
         .MustHaveHappened(1, Times.Exactly);
   }
}