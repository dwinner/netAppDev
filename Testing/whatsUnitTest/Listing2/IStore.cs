namespace Listing2;

public interface IStore
{
   bool HasEnoughInventory(ProductType productType, int quantity);
   void RemoveInventory(ProductType productType, int quantity);
   void AddInventory(ProductType productType, int quantity);
   int GetInventory(ProductType productType);
}