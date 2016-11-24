using AcmeCarRental.AopVersion.Data;

namespace AcmeCarRental.AopVersion.Refactor
{
   public interface ILoyaltyAccrualService
   {
      void Accrue(RentalAgreement agreement);
   }
}