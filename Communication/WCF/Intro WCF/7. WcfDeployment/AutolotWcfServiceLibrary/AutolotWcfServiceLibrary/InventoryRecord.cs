using System.Runtime.Serialization;

namespace AutolotWcfServiceLibrary
{
   [DataContract]
   public class InventoryRecord
   {
      [DataMember]
      public int CarId;
      
      [DataMember]
      public string Make;
      
      [DataMember]
      public string Color;
      
      [DataMember]
      public string PetName;
   }   
}
