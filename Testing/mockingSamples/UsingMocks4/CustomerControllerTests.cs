using Moq;

namespace UsingMocks4;

public class CustomerControllerTests
{
   [Fact(Skip = "Concept illustration only")]
   public void Successful_purchase()
   {
      var mock = new Mock<IEmailGateway>();
      var sut = new CustomerController(mock.Object);

      var isSuccess = sut.Purchase(
         1, 2, 5);

      Assert.True(isSuccess);
      mock.Verify(
         x => x.SendReceipt(
            "customer@email.com", "Shampoo", 5),
         Times.Once);
   }
}