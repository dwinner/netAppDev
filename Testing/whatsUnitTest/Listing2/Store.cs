namespace Listing2;

public class Store : IStore
{
   private readonly Dictionary<ProductType, int> _inventory = new();

   public bool HasEnoughInventory(ProductType productType, int quantity) => GetInventory(productType) >= quantity;

   public void RemoveInventory(ProductType productType, int quantity)
   {
      if (!HasEnoughInventory(productType, quantity))
      {
         throw new Exception("Not enough inventory");
      }

      _inventory[productType] -= quantity;
   }

   public void AddInventory(ProductType productType, int quantity)
   {
      if (_inventory.ContainsKey(productType))
      {
         _inventory[productType] += quantity;
      }
      else
      {
         _inventory.Add(productType, quantity);
      }
   }

   public int GetInventory(ProductType productType)
   {
      var productExists = _inventory.TryGetValue(productType, out var remaining);
      return productExists ? remaining : 0;
   }
}