using System.ServiceModel;

namespace ExternalInterface
{
   [ServiceContract]
   public interface IExternalInterface
   {
      [OperationContract]
      void SubmitRentalContract(RentalContract rentalContract);
   }
}