using AcmeCarRental.AopVersion.Data;

namespace AcmeCarRental.AopVersion.Refactor
{
   public interface ILoyaltyRedemptionService
   {
      void Redeem(Invoice invoice, int numberOfDays);
   }
}