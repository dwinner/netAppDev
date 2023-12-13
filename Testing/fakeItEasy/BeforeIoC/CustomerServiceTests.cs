namespace BeforeIoC;

[TestFixture]
public class WhenGettingCustomerById
{
   [SetUp]
   public void Given()
   {
      var sut = new CustomerService();
      _result = sut.GetCustomerByCustomerId(CustomerId);
   }

   private const int CustomerId = 1;
   private Customer? _result;

   [Test]
   public void ReturnsTheCorrectId()
   {
      Assert.That(_result, Is.Not.Null);
      Assert.That(_result!.Id, Is.EqualTo(CustomerId));
   }
}