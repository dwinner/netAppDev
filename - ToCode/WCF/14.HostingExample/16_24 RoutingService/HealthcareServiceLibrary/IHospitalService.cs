using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace HealthcareServiceLibrary
{
    [ServiceContract]
    public interface IHospitalService
    {
        [OperationContract(Action = "http://healthcare/hospital")]
        void Save(SomeData sd);
    }
}
