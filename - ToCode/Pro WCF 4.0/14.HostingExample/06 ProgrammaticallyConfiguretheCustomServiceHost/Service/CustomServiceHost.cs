using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Service
{
    public class CustomServiceHost : ServiceHost
    {
        public CustomServiceHost(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            this.Opening += new EventHandler(CustomServiceHost_Opening);
        }

        void CustomServiceHost_Opening(object sender, EventArgs e)
        {
            MyCustomBehavior behavior = this.Description.Behaviors.Find<MyCustomBehavior>();
            if (behavior == null)
            {
                this.Description.Behaviors.Add(new MyCustomBehavior());
            }
        }
    }
}
