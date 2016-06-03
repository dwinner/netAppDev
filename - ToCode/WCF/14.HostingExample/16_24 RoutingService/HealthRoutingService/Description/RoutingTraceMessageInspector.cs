using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;

namespace HealthRoutingService.Description
{
    class RoutingTraceMessageInspector : IClientMessageInspector
    {
        private System.ServiceModel.Description.ServiceEndpoint endpoint;

        public RoutingTraceMessageInspector(System.ServiceModel.Description.ServiceEndpoint endpoint)
        {
            this.endpoint = endpoint;
        }
        #region IClientMessageInspector Members

        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            Console.WriteLine("Reply from {0}:{1}", endpoint.Name, endpoint.Address.Uri.ToString());
        }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            Console.WriteLine();
            Console.WriteLine("Routed  to {0}:{1}", endpoint.Name, endpoint.Address.Uri.ToString());
            return null;
        }

        #endregion
    }
}
