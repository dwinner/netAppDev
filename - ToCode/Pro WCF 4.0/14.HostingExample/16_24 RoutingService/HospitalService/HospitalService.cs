using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HealthcareServiceLibrary;

namespace Hospital
{
    class HospitalService : IHospitalService, ILabService
    {
        void IHospitalService.Save(SomeData sd)
        {
            Console.WriteLine("HospitalService has saved some data...");
        }

        #region ILabService Members

        public void Save(SomeOtherData data)
        {
            Console.WriteLine("HospitalService acts as LabServiceBackup...");
        }

        #endregion
    }
}
