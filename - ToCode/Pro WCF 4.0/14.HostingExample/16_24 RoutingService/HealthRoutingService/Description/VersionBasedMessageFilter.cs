using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel;

namespace HealthRoutingService.Description
{
    class VersionBasedMessageFilter : MessageFilter
    {
        private string EnvelopeVersion;

        public VersionBasedMessageFilter(object filterData)
        {            
            this.EnvelopeVersion = filterData as string;
        }

        public override bool Match(System.ServiceModel.Channels.Message message)
        {
            return this.InnerMatch(message);
        }

        public override bool Match(System.ServiceModel.Channels.MessageBuffer buffer)
        {
            bool response;
            Message message = buffer.CreateMessage();
            try
            {
                response = this.InnerMatch(message);
            }
            finally
            {
                message.Close();
            }

            return (response);
        }

        private bool InnerMatch(System.ServiceModel.Channels.Message message)
        {
            return (message.Version.Envelope.ToString().Contains(this.EnvelopeVersion));
        }
    }
}
