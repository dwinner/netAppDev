using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using HealthcareServiceLibrary;

namespace Lab
{
    class LabService : ILabService
    {
        void ILabService.Save(SomeOtherData data)
        {
            Console.WriteLine("LabService has saved some data...");
        }
    }
}
