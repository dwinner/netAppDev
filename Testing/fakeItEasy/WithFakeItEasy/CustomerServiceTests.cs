namespace WithFakeItEasy;

[TestFixture]
public class WhenGettingCustomerByIdTests
{
   [SetUp]
   public void Given()
   {
      _customer = new Customer { Id = CustomerId };

      //create our Fake
      var aFakeCustomerRepository = A.Fake<ICustomerRepository>();

      //set expectations for the call to .GetCustomerBy
      A.CallTo(() => aFakeCustomerRepository.GetCustomerBy(CustomerId)).Returns(_customer);

      var sut = new CustomerService(aFakeCustomerRepository);
      _result = sut.GetCustomerByCustomerId(CustomerId);
   }

   private const int CustomerId = 1;
   private Customer? _result;
   private Customer? _customer;

   [Test]
   public void ReturnsTheCorrectId()
   {
      Assert.That(_result!.Id, Is.EqualTo(CustomerId));
   }
}