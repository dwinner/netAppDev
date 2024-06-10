using System;

namespace Samples.ServiceLocator
{
   internal static class Manual
   {
      public static T Locate<T>(params object[] args) => (T) Activator.CreateInstance(typeof(T), args);
   }
}