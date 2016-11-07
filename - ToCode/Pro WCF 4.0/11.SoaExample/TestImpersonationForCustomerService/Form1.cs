using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.ServiceModel;

namespace TestImpersonationForCustomerService
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CustomerInterface.ICustomer channel;

            ChannelFactory<CustomerInterface.ICustomer> channelFactory;
            channelFactory = new ChannelFactory<CustomerInterface.ICustomer>(new WSHttpBinding(), new EndpointAddress("http://localhost:9876/CustomerService"));
            channelFactory.Credentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
            channel = channelFactory.CreateChannel();

            channel.RegisterCustomer(new CustomerInterface.Customer() { CustomerName = "Test" });
        }
    }
}
