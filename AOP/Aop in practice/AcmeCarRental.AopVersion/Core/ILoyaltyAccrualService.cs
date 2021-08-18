using AcmeCarRental.AopVersion.Data;

namespace AcmeCarRental.AopVersion.Core
{
   public interface ILoyaltyAccrualService
   {
      void Accrue(RentalAgreement agreement);
   }
}