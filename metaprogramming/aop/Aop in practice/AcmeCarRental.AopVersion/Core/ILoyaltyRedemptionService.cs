using AcmeCarRental.AopVersion.Data;

namespace AcmeCarRental.AopVersion.Core
{
   public interface ILoyaltyRedemptionService
   {
      void Redeem(Invoice invoice, int numberOfDays);
   }
}