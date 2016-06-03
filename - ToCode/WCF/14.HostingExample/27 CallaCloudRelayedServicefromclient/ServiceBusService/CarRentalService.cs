using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Wrox.CarRentalService.Contracts;

namespace Wrox.CarRentalService.Implementations.Europe
{
    public class CarRentalService : ICarRentalService
    {
        public double CalculatePrice(DateTime pickupDate, DateTime returnDate, string pickupLocation, string vehiclePreference)
        {
            // insert service logic (call to NextDouble() is only for the purpose of this sample)
            return new Random().NextDouble();
        }
    }
}
