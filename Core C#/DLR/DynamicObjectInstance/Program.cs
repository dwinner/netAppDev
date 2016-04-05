/**
 * Объекты со специальным динамическим поведением
 */

using System;
using System.Dynamic;

namespace DynamicObjectInstance
{
   static class Program
   {
      static void Main()
      {
         dynamic dynamicType = new MyDynamicType();
         dynamicType.DoDefaultWork();
         dynamicType.DoWork();
         dynamicType.Value = 42;
         dynamicType.Count = 123;

         Console.ReadKey();
      }
   }

   public class MyDynamicType : DynamicObject
   {
      public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
      {
         result = null;
         Console.WriteLine("Динамический вызов {0}.{1}()", GetType(), binder.Name);
         return true;
      }

      public override bool TrySetMember(SetMemberBinder binder, object value)
      {
         Console.WriteLine("Динамическая установка свойства {0}.{1} to {2}", GetType(), binder.Name, value);
         return true;
      }

      public void DoDefaultWork()
      {
         Console.WriteLine("Выполнение работы по умолчанию");
      }
   }
}
