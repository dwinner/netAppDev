using FakeItEasy;

namespace IsInstanceOf;

[TestFixture]
public class WhenCreatingACustomer
{
   [SetUp]
   public void Given()
   {
      _bus = A.Fake<IBus>();
      var sut = new CustomerService(_bus);
      sut.CreateCustomer("FirstName", "LastName", "Email");
   }

   private IBus _bus = null!;

   [Test]
   public void SendsCreateCustomer()
   {
      A.CallTo(() => _bus.Send(A<object>.That.IsInstanceOf(typeof(CreateCustomer))))
         .MustHaveHappened(1, Times.Exactly);
   }
}