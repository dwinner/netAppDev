using System.Runtime.Serialization;
using System.ServiceModel;

namespace AutolotWcfService
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

   [DataContract]
   public class InventoryRecord
   {
      [DataMember] public int CarId;
      [DataMember] public string Make;
      [DataMember] public string Color;
      [DataMember] public string PetName;
   }
}
