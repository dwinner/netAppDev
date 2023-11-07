using System.ServiceModel;

namespace KnownTypes
{
   [ServiceContract]
   [ServiceKnownType(
      nameof(MessageProcessorKnownTypesProvider.GetMessageTypes),
      typeof(MessageProcessorKnownTypesProvider))]
   public interface IMessageProcessor
   {
      [OperationContract]
      string Process(Message fruit);
   }
}