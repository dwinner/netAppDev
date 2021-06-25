using System.Runtime.Serialization;

namespace KnownTypes
{
   [DataContract]
   public sealed class ApplicationClosedMessage : Message
   {
      public ApplicationClosedMessage(string machineName)
      {
         MachineName = machineName;
         Data = "Application has closed";
      }

      [DataMember]
      public string MachineName { get; set; }
   }
}