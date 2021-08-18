using System.ServiceModel;

namespace KnownTypes
{
   [ServiceBehavior]
   public class MessageProcessor : IMessageProcessor
   {
      [OperationBehavior]
      public string Process(Message fruit) => fruit.Data;
   }
}