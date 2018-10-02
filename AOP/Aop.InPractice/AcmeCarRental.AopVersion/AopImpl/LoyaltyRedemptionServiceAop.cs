using AcmeCarRental.AopVersion.Aspects;
using AcmeCarRental.AopVersion.Core;
using AcmeCarRental.AopVersion.Data;

namespace AcmeCarRental.AopVersion.AopImpl
{
   public class LoyaltyRedemptionServiceAop : ILoyaltyRedemptionService
   {
      private readonly ILoyaltyDataService _dataService;

      public LoyaltyRedemptionServiceAop(ILoyaltyDataService service)
      {
         _dataService = service;
      }

      [ValidateAspect]
      [ExceptionAspect]
      [LoggingAspect]
      [TransactionWrapperAspect]
      public void Redeem(Invoice invoice, int numberOfDays)
      {
         var pointsPerDay = 10;
         if (invoice.Vehicle.Size >= Size.Luxury)
            pointsPerDay = 15;
         var points = numberOfDays * pointsPerDay;
         _dataService.SubtractPoints(invoice.Customer.Id, points);
         invoice.Discount = numberOfDays * invoice.CostPerDay;
      }
   }
}