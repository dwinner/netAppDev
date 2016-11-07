using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace HQOrderEntryImplementation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SubscribeService : HQOrderEntryServiceInterface.ISubscribeToOrderTrackingInfo
    {

        List<RealTimeOrderTrackingCallBackContract.IOrderTracking> callBacks;

        public SubscribeService()
        {
            callBacks = new List<RealTimeOrderTrackingCallBackContract.IOrderTracking>();
        }
        public void Subscribe()
        {
            Console.WriteLine("**Someone Subscribed");
            callBacks.Add(System.ServiceModel.OperationContext.Current.GetCallbackChannel<RealTimeOrderTrackingCallBackContract.IOrderTracking>());
        }

        public void PublishOrderEntrySignal(string countryID)
        {
            foreach (var item in callBacks)
            {
                item.NewOrderForCountry(countryID);
            }
        }

        public void UnSubscribe()
        {
            Console.WriteLine("**Someone UnSubscribed");
            callBacks.Remove(System.ServiceModel.OperationContext.Current.GetCallbackChannel<RealTimeOrderTrackingCallBackContract.IOrderTracking>());
        }
    }
}
