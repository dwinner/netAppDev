using System;
using System.Reflection;

namespace CerCodeSample
{
   internal sealed class DangerType
   {
      static DangerType()
      {
         var declaringType = MethodBase.GetCurrentMethod().DeclaringType;
         if (declaringType != null)
            Console.WriteLine("cctor called for type: {0}", declaringType.Name);
      }

      public void ForceError(bool force = false)
      {
         if (force)
         {
            throw new Exception("Oops");
         }
      }
   }
}