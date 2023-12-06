namespace Listing2;

public class Customer
{
   public bool Purchase(IStore store, ProductType productType, int quantity)
   {
      if (!store.HasEnoughInventory(productType, quantity))
      {
         return false;
      }

      store.RemoveInventory(productType, quantity);

      return true;
   }
}