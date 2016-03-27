using System;
using System.Reflection;

namespace Singleton
{
   public static class ReflectionSingleton<T>
      where T : class
   {
      private static volatile T _instance;

      public static T Instance
      {
         get
         {
            if (_instance != null)
            {
               return _instance;
            }
            lock (new object())
            {
               if (_instance != null)
               {
                  return _instance;
               }
               ConstructorInfo constructorInfo;
               try
               {
                  constructorInfo = typeof (T).GetConstructor(
                     BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[0], null);
               }
               catch (Exception exception)
               {
                  throw new Exception("DefaultMessage", exception);
               }
               if (constructorInfo == null || constructorInfo.IsAssembly)
               {
                  throw new Exception("DefaultMessage");
               }
               _instance = (T)constructorInfo.Invoke(null);
            }
            return _instance;
         }
      }
   }
}
