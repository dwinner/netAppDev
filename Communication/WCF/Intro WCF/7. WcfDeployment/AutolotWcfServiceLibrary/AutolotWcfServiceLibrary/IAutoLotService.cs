using System.ServiceModel;

namespace AutolotWcfServiceLibrary
{   
   [ServiceContract]
   public interface IAutoLotService
   {
      [OperationContract]
      void InsertCar(int carId, string make, string color, string petName);

      [OperationContract(Name = "InsertCarWithDetails")]
      void InsertCar(InventoryRecord aCar);

      [OperationContract]
      InventoryRecord[] GetInventory();
   }   
}
