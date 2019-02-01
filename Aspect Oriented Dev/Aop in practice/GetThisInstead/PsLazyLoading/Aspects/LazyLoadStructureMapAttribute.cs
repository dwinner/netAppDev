using System;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;
using StructureMap;

namespace PsLazyLoading.Aspects
{
   [Serializable]
   public sealed class LazyLoadStructureMapAttribute : LocationInterceptionAspect
   {
      private volatile object _backingField;
      private readonly object _syncRoot=new object();

      public override void OnGetValue(LocationInterceptionArgs args)
      {
         if (_backingField == null)
         {
            lock (_syncRoot)
            {
               if (_backingField == null)
               {
                  var locationType = args.Location.PropertyInfo.PropertyType;                  
                  _backingField = ObjectFactory.GetInstance(locationType);
               }
            }
         }

         args.Value = _backingField;
      }
   }
}