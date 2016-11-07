using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace HealthcareServiceLibrary
{
    [ServiceContract]
    public interface ILabService
    {
        [OperationContract(Action = "http://healthcare/lab")]
        void Save(SomeOtherData data);
    }
}
