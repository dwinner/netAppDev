using System.Runtime.Serialization;

namespace KnownTypes
{
   [DataContract]
   public class Message
   {
      public Message()
      {
         Data = "Unknown";
      }

      [DataMember]
      public string Data { get; set; }
   }
}