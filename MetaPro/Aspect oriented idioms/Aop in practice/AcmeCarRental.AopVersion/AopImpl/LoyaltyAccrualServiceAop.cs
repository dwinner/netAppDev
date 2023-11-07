using System;
using AcmeCarRental.AopVersion.Aspects;
using AcmeCarRental.AopVersion.Core;
using AcmeCarRental.AopVersion.Data;

namespace AcmeCarRental.AopVersion.AopImpl
{
   public class LoyaltyAccrualServiceAop : ILoyaltyAccrualService
   {
      private readonly ILoyaltyDataService _dataService;

      public LoyaltyAccrualServiceAop(ILoyaltyDataService service)
      {
         _dataService = service;
      }

      [ValidateAspect]
      [ExceptionAspect]
      [LoggingAspect]
      [TransactionWrapperAspect]
      public void Accrue(RentalAgreement agreement)
      {
         var rentalTime = agreement.EndDate.Subtract(agreement.StartDate);
         var days = (int)Math.Floor(rentalTime.TotalDays);
         var pointsPerDay = 1;
         if (agreement.Vehicle.Size >= Size.Luxury)
            pointsPerDay = 2;
         var pts = days * pointsPerDay;
         _dataService.AddPoints(agreement.Customer.Id, pts);
      }
   }
}