﻿namespace UsingMocks4;

public interface IStore
{
   bool HasEnoughInventory(Product product, int quantity);
   void RemoveInventory(Product product, int quantity);
   void AddInventory(Product product, int quantity);
   int GetInventory(Product product);
}