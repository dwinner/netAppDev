namespace Listing1;

public class CustomerTests
{
   [Fact]
   public void Purchase_succeeds_when_enough_inventory()
   {
      // Arrange
      var store = new Store();
      store.AddInventory(ProductType.Shampoo, 10);
      var customer = new Customer();

      // Act
      var success = customer.Purchase(store, ProductType.Shampoo, 5);

      // Assert
      Assert.True(success);
      Assert.Equal(5, store.GetInventory(ProductType.Shampoo));
   }

   [Fact]
   public void Purchase_fails_when_not_enough_inventory()
   {
      // Arrange
      var store = new Store();
      store.AddInventory(ProductType.Shampoo, 10);
      var customer = new Customer();

      // Act
      var success = customer.Purchase(store, ProductType.Shampoo, 15);

      // Assert
      Assert.False(success);
      Assert.Equal(10, store.GetInventory(ProductType.Shampoo));
   }
}