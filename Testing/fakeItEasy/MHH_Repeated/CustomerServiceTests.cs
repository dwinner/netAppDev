using FakeItEasy;

namespace MHH_Repeated;

[TestFixture]
public class WhenSendingEmailToOneCustomer
{
   [SetUp]
   public void Given()
   {
      _emailSender = A.Fake<ISendEmail>();

      var customerRepository = A.Fake<ICustomerRepository>();
      A.CallTo(() => customerRepository.GetAllCustomers()).Returns(new List<Customer> { new(), new() });

      var sut = new CustomerService(_emailSender, customerRepository);
      sut.SendEmailToAllCustomers();
   }

   private ISendEmail _emailSender = null!;

   [Test]
   public void SendsEmail()
   {
      A.CallTo(() => _emailSender.SendMail()).MustHaveHappened(2, Times.Exactly);
   }
}

[TestFixture]
public class WhenSendingEmailToTwoCustomers
{
   [SetUp]
   public void Given()
   {
      _emailSender = A.Fake<ISendEmail>();

      var customerRepository = A.Fake<ICustomerRepository>();
      A.CallTo(() => customerRepository.GetAllCustomers())
         .Returns(new List<Customer> { new(), new() });

      var sut = new CustomerService(_emailSender, customerRepository);
      sut.SendEmailToAllCustomers();
   }

   private ISendEmail _emailSender = null!;

   [Test]
   public void SendsEmail()
   {
      A.CallTo(() => _emailSender.SendMail()).MustHaveHappened(2, Times.Exactly);
   }
}

[TestFixture]
public class WhenSendingEmailToAllCustomers
{
   [SetUp]
   public void Given()
   {
      _emailSender = A.Fake<ISendEmail>();
      _customers = new List<Customer> { new(), new() };

      var customerRepository = A.Fake<ICustomerRepository>();
      A.CallTo(() => customerRepository.GetAllCustomers()).Returns(_customers);

      var sut = new CustomerService(_emailSender, customerRepository);
      sut.SendEmailToAllCustomers();
   }

   private ISendEmail _emailSender = null!;
   private List<Customer> _customers = null!;

   [Test]
   public void SendsTwoEmails()
   {
      A.CallTo(() => _emailSender.SendMail()).MustHaveHappened(_customers.Count, Times.Exactly);
   }
}