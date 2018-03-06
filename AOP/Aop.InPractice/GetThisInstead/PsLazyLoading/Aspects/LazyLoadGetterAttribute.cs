using System;
using PostSharp.Aspects;

namespace PsLazyLoading.Aspects
{
   [Serializable]
   public sealed class LazyLoadGetterAttribute : LocationInterceptionAspect
   {
      private readonly object _syncRoot = new object();
      private volatile object _backingField;

      public override void OnGetValue(LocationInterceptionArgs args)
      {
         if (_backingField == null)
         {
            lock (_syncRoot)
            {
               if (_backingField == null)
               {
                  args.ProceedGetValue();
                  _backingField = args.Value;
               }
            }
         }

         args.Value = _backingField;
      }
   }
}