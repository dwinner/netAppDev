using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace RealTimeOrderTrackingCallBackContract
{
    [ServiceContract]
    public interface IOrderTracking
    {
        
        [OperationContract]
        void NewOrderForCountry(string countryID);
    }
}
