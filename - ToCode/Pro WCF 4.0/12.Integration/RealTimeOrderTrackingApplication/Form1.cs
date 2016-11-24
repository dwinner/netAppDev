using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.ServiceModel;

namespace RealTimeOrderTrackingApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CallBackImplementation callBack;
            callBack = new CallBackImplementation();
            callBack.formToReportTo = this;

            InstanceContext instanceContext = new InstanceContext(callBack);

            ChannelFactory<HQOrderEntryServiceInterface.ISubscribeToOrderTrackingInfo> cf = 
                new DuplexChannelFactory<HQOrderEntryServiceInterface.ISubscribeToOrderTrackingInfo>
                    (instanceContext, new System.ServiceModel.NetTcpBinding());
            HQOrderEntryServiceInterface.ISubscribeToOrderTrackingInfo subscriber = cf.CreateChannel(new EndpointAddress("net.tcp://localhost:9875"));
            subscriber.Subscribe();

            label1.Text = "Subscribed";
        }
    }
}
