using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace HQOrderEntryServiceInterface
{
    [ServiceContract]
    [ServiceKnownType(typeof(HQOrderEntryServiceInterface.HQOrderEntry))]
    public interface IOrderEntryService
    {
        [OperationContract(IsOneWay = true, Action = "*")]
        void SendOrderEntry(System.ServiceModel.MsmqIntegration.MsmqMessage
            <HQOrderEntryServiceInterface.HQOrderEntry> orderEntry);
    }
}
