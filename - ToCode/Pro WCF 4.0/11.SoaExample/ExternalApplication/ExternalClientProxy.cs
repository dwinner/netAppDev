using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace ExternalApplication
{
    class ExternalClientProxy : ClientBase<ExternalInterface.IExternalInterface>, ExternalInterface.IExternalInterface
    {
        public ExternalClientProxy()
            : base("ExternalEndpoint")
        {

        }
        public void SubmitRentalContract(ExternalInterface.RentalContract rentalContract)
        {
            Channel.SubmitRentalContract(rentalContract);
        }
    }
}
