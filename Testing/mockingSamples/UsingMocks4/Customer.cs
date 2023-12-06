namespace UsingMocks4;

public class Customer
{
   public string Email { get; set; }

   public bool Purchase(IStore store, Product product, int quantity)
   {
      if (!store.HasEnoughInventory(product, quantity))
      {
         return false;
      }

      store.RemoveInventory(product, quantity);

      return true;
   }
}