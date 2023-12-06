using System;
using PostSharp.Aspects;

namespace PsLazyLoading.Aspects
{
   [Serializable]
   public sealed class LazyLoadActivatorAttribute : LocationInterceptionAspect
   {
      private volatile object _backingField;
      private readonly object _syncRoot = new object();

      public override void OnGetValue(LocationInterceptionArgs args)
      {
         if (_backingField == null)
         {
            lock (_syncRoot)
            {
               if (_backingField == null)
               {
                  _backingField = Activator.CreateInstance(args.Location.LocationType);
               }
            }
         }

         args.Value = _backingField;
      }
   }
}