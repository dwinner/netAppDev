using System;

namespace AcmeCarRental.AopVersion.Data
{
   public interface ILoyaltyDataService
   {
      void AddPoints(Guid customerId, int points);
      void SubtractPoints(Guid customerId, int points);
   }
}