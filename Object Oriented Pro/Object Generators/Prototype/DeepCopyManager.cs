using System;

namespace Prototype
{
   public sealed class DeepCopyManager<T>
      // where T : ISerializable
   {
      static DeepCopyManager()
      {
         if (!typeof (T).IsSerializable)
         {
            throw new NotSupportedException(
               $"Non serializable type: {typeof (T).FullName}");
         }
      }

      public T DeepCopy(T aTypeObj) => UniversalCopyUtility<T>.DeepCopy(aTypeObj);
   }
}