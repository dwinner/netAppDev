using System;
using System.Collections.Generic;
using System.Data;
using AutolotLibrary;

namespace AutolotWcfServiceLibrary
{
   public class InventoryDalAdapter : IDisposable
   {
      private readonly InventoryDal _inventoryDal;

      public InventoryDalAdapter(string aConnectionString)
      {
         _inventoryDal = new InventoryDal();
         _inventoryDal.OpenConnection(aConnectionString);
      }

      public void InsertAuto(int id, string make, string color, string petName)
      {
         _inventoryDal.InsertAuto(id, make, color, petName);
      }

      public void InsertAuto(NewCar car)
      {
         _inventoryDal.InsertAuto(car);
      }

      public void DeleteCar(int carId)
      {
         _inventoryDal.DeleteCar(carId);
      }

      public void UpdateCarPetName(int carId, string newPetName)
      {
         _inventoryDal.UpdateCarPetName(carId, newPetName);
      }

      public List<NewCar> GetAllInventoryAsList()
      {
         return _inventoryDal.GetAllInventoryAsList();
      }

      public DataTable GetAllInventoryAsDataTable()
      {
         return _inventoryDal.GetAllInventoryAsDataTable();
      }

      public string LookUpPetName(int carId)
      {
         return _inventoryDal.LookUpPetName(carId);
      }

      public void ProcessCreditRisk(int custId, bool throwEx)
      {
         _inventoryDal.ProcessCreditRisk(custId, throwEx);
      }

      public void Dispose()
      {
         if (_inventoryDal != null)
         {
            _inventoryDal.CloseConnection();
         }
      }
   }
}
