using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;
using RentalInterface;

namespace RentalApplication
{
    public class RentalProxy : ClientBase<IRental>, IRental
    {

        public RentalProxy()
            : base("RentalServiceEndpoint")
        {

        }

        #region IRental Members

        public string RegisterCarRental(RentalRegistration rental)
        {
            return Channel.RegisterCarRental(rental);
        }

        public void RegisterCarRentalAsPayed(string rentalID)
        {
            Channel.RegisterCarRentalAsPayed(rentalID);
        }

        public void StartCarRental(string rentalID, string locationID)
        {
            Channel.StartCarRental(rentalID, locationID);
        }

        public void StopCarRental(string rentalID, string locationID)
        {
            Channel.StopCarRental(rentalID, locationID);
        }

        public RentalRegistration GetRentalRegistration(string rentalID)
        {
            return Channel.GetRentalRegistration(rentalID);
        }

        #endregion
    }
}
