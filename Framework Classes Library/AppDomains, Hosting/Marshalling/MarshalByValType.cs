using System;
using static System.Console;
using static System.Threading.Thread;

namespace Marshalling
{
   /// <summary>
   ///    Класс, допускающий продвижение по значению за счет сериализации
   /// </summary>
   [Serializable]
   public sealed class MarshalByValType
   {
      private DateTime _creationDate = DateTime.Now;

      public MarshalByValType()
      {
         WriteLine($"{nameof(GetType)} ctor running in {GetDomain().FriendlyName}, Created on {2:D}", _creationDate);
      }

      public override string ToString()
      {
         return _creationDate.ToLongDateString();
      }
   }
}