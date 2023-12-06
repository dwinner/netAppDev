using Moq;

namespace UsingMocks4;

public class CustomerTests
{
   [Fact]
   public void Purchase_succeeds_when_enough_inventory()
   {
      var storeMock = new Mock<IStore>();
      storeMock
         .Setup(x => x.HasEnoughInventory(Product.Shampoo, 5))
         .Returns(true);
      var customer = new Customer();

      var success = customer.Purchase(storeMock.Object, Product.Shampoo, 5);

      Assert.True(success);
      storeMock.Verify(
         x => x.RemoveInventory(Product.Shampoo, 5),
         Times.Once);
   }
}