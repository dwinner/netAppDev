using System.ServiceModel;

namespace RentalInterface
{
   [ServiceContract]
   public interface IRental
   {
      [OperationContract]
      string RegisterCarRental(RentalRegistration rentalRegistration);

      [OperationContract]
      void RegisterCarRentalAsPaid(string rentalId);

      [OperationContract]
      void StartCarRental(string rentalId);

      [OperationContract]
      void StopCarRental(string rentalId);

      [OperationContract]
      RentalRegistration GetRentalRegistration(string rentalId);
   }
}