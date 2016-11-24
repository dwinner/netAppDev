using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using System.ServiceModel;

using System.Security.Principal;

using RentalInterface;

namespace RentalService
{
    public class RentalServiceImplementation : IRental
    {
        #region IRental Members

        [OperationBehavior(TransactionAutoComplete = true, TransactionScopeRequired = true)]
        public string RegisterCarRental(RentalRegistration rentalRegistration)
        {
            Console.WriteLine("RegisterCarRental");

            if (rentalRegistration == null)
            {
                RentalRegisterFault fault;
                fault = new RentalRegisterFault();
                fault.FaultID = 1;
                fault.FaultDescription = "Input is not valid, got null value";
                throw new FaultException<RentalRegisterFault>(fault, "");
            }

            try
            {
                using (DataClassesRentalDataContext ctx = new DataClassesRentalDataContext())
                {
                    Rental rentalToInsert;
                    rentalToInsert = new Rental();
                    rentalToInsert.CustomerID = rentalRegistration.CustomerID;
                    rentalToInsert.CarID = rentalRegistration.CarID;
                    rentalToInsert.Comments = rentalRegistration.Comments;
                    ctx.Rentals.InsertOnSubmit(rentalToInsert);

                    //throw new DivideByZeroException();

                    ctx.SubmitChanges();
                    Console.WriteLine(">SubmitChanges RegisterCarRental ");
                    return "OK";
                }
            }
            catch (Exception ex)
            {
                RentalRegisterFault fault;
                fault = new RentalRegisterFault();
                fault.FaultID = 123;
                fault.FaultDescription = "An error occured while inserting the rental registration : " + ex.Message;
                throw new FaultException<RentalRegisterFault>(fault, "");
            }
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Required)]
        public void RegisterCarRentalAsPayed(string rentalID)
        {
            Console.WriteLine("RegisterCarRentalAsPayed " + rentalID);
            Console.WriteLine("  WindowsIdentity : {0} ", WindowsIdentity.GetCurrent().Name);
            //Console.WriteLine("  ServiceSecurityContext : {0} ", ServiceSecurityContext.Current.WindowsIdentity.Name);
        }

        public void StartCarRental(string rentalID, string locationID)
        {
            throw new NotImplementedException();
        }

        public void StopCarRental(string rentalID, string locationID)
        {
            throw new NotImplementedException();
        }

        public RentalRegistration GetRentalRegistration(string rentalID)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
