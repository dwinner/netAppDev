using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

public class Service : IService
{
    public void DoSomething()
    {
        // some code
    }
}

[ServiceContract]
public interface IService
{
    [OperationContract]
    void DoSomething();
}
