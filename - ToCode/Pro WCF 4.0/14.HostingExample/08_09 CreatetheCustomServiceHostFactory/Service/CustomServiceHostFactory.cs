using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Activation;
using System.ServiceModel;
using Service;

public class CustomServiceHostFactory : ServiceHostFactory
{
    public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
    {
        return new CustomServiceHost(Type.GetType(constructorString), baseAddresses);
    }
    protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
    {
        return new CustomServiceHost(serviceType, baseAddresses);
    }
}