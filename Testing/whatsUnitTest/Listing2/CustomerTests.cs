using Moq;

namespace Listing2;

public class CustomerTests
{
   [Fact]
   public void Purchase_succeeds_when_enough_inventory()
   {
      // Arrange
      var storeMock = new Mock<IStore>();
      storeMock
         .Setup(x => x.HasEnoughInventory(ProductType.Shampoo, 5))
         .Returns(true);
      var customer = new Customer();

      // Act
      var success = customer.Purchase(storeMock.Object, ProductType.Shampoo, 5);

      // Assert
      Assert.True(success);
      storeMock.Verify(x => x.RemoveInventory(ProductType.Shampoo, 5), Times.Once);
   }

   [Fact]
   public void Purchase_fails_when_not_enough_inventory()
   {
      // Arrange
      var storeMock = new Mock<IStore>();
      storeMock
         .Setup(x => x.HasEnoughInventory(ProductType.Shampoo, 5))
         .Returns(false);
      var customer = new Customer();

      // Act
      var success = customer.Purchase(storeMock.Object, ProductType.Shampoo, 5);

      // Assert
      Assert.False(success);
      storeMock.Verify(x => x.RemoveInventory(ProductType.Shampoo, 5), Times.Never);
   }
}