using System;
using static System.Console;
using static System.Threading.Thread;

namespace Marshalling
{
   /// <summary>
   ///    Тип, допускающий продвижение по ссылке через границы доменов
   /// </summary>
   public sealed class MarshalByRefType : MarshalByRefObject
   {
      public MarshalByRefType()
      {
         WriteLine("{0} ctor running in {1}", GetType(), GetDomain().FriendlyName);
      }

      public void SomeMethod()
      {
         WriteLine("Executing in {0}", GetDomain().FriendlyName);
      }

      public MarshalByValType MethodWithReturn()
      {
         WriteLine("Executing in {0}", GetDomain().FriendlyName);
         return new MarshalByValType();
      }

      public NonMarshalableType MethodArgAndReturn(string callingDomainName)
      {
         WriteLine("Calling from '{0}' to '{1}'", callingDomainName, GetDomain().FriendlyName);
         return new NonMarshalableType();
      }
   }
}