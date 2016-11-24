using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace HQOrderEntryServiceInterface
{
    [ServiceContract(CallbackContract = typeof(RealTimeOrderTrackingCallBackContract.IOrderTracking))]
    public interface ISubscribeToOrderTrackingInfo
    {
        [OperationContract]
        void Subscribe();

        [OperationContract]
        void UnSubscribe();
    }
}
