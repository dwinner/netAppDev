using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CalculateReferenceIDService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CalculateReferenceIDService" in code, svc and config file together.
    public class CalculateReferenceIDService : ICalculateReferenceIDService
    {
        public int GetNewReferenceID()
        {
            return DateTime.Now.Millisecond;
        }
    }
}
