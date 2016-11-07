using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

using System.ServiceModel.MsmqIntegration;

namespace HQOrderEntryImplementation
{
    [ServiceContract]    
    public interface ISendInvalidOrder
    {
        [OperationContract(IsOneWay=true)]
        void SendInvalidOrder(MsmqMessage<HQOrderEntryServiceInterface.HQOrderEntry> invalidOrderEntry);
    }
}
