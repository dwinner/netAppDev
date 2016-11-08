using System;
using PostSharp.Aspects;

namespace MethodBoundarySample.Aspects
{
   [Serializable]
   public sealed class ObjectInitializationAspect : LocationInterceptionAspect
   {
      public override void OnGetValue(LocationInterceptionArgs args)
      {
         if (args.GetCurrentValue() == null)
         {
            Console.WriteLine("Property ({0}) not initialized", args.LocationFullName);
         }
      }
   }
}