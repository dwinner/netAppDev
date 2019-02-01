using System;
using AcmeCarRental.AopVersion.Data;

namespace AcmeCarRental.AopVersion.Refactor
{
   public class LoyaltyRedemptionServiceRefactored : ILoyaltyRedemptionService
   {
      private readonly IExceptionHandler _exceptionHandler;
      private readonly ILoyaltyDataService _loyaltyDataService;
      private readonly ITransactionManager _transactionManager;

      public LoyaltyRedemptionServiceRefactored(ILoyaltyDataService service, IExceptionHandler exceptionHandler,
         ITransactionManager transactionManager)
      {
         _loyaltyDataService = service;
         _exceptionHandler = exceptionHandler;
         _transactionManager = transactionManager;
      }

      public void Redeem(Invoice invoice, int numberOfDays)
      {
         // defensive programming
         if (invoice == null) throw new ArgumentNullException(nameof(invoice));
         if (numberOfDays <= 0) throw new ArgumentException("", nameof(numberOfDays));

         // logging
         Console.WriteLine("Redeem: {0}", DateTime.Now);
         Console.WriteLine("Invoice: {0}", invoice.Id);

         _exceptionHandler.Wrapper(() =>
         {
            _transactionManager.Wrapper(() =>
            {
               var pointsPerDay = 10;
               if (invoice.Vehicle.Size >= Size.Luxury)
                  pointsPerDay = 15;
               var points = numberOfDays * pointsPerDay;
               _loyaltyDataService.SubtractPoints(invoice.Customer.Id, points);
               invoice.Discount = numberOfDays * invoice.CostPerDay;

               // logging
               Console.WriteLine("Redeem complete: {0}", DateTime.Now);
            });
         });
      }
   }
}