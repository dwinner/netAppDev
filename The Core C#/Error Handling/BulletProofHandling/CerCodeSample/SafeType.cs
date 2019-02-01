using System;
using System.Reflection;
using System.Runtime.ConstrainedExecution;

namespace CerCodeSample
{
   internal sealed class SafeType
   {
      static SafeType()
      {
         var declaringType = MethodBase.GetCurrentMethod().DeclaringType;
         if (declaringType != null)
            Console.WriteLine("cctor called for type: {0}", declaringType.Name);
      }

      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
      public void ForceError(bool force = false)
      {
         if (force)
         {
            throw new Exception("Oops");
         }
      }
   }
}