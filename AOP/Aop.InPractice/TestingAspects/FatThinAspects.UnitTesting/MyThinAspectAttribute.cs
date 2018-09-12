using PostSharp.Aspects;
using System;
using System.Reflection;

namespace FatThinAspects.UnitTesting
{
   [Serializable]
   public sealed class MyThinAspectAttribute : OnMethodBoundaryAspect
   {
      private IMyCrossCuttingConcern _concern;

      public override void RuntimeInitialize(MethodBase method)
      {
         if (!AspectSettings.On) return;
         _concern = StructureMapServiceLocator.DefaultImpl.GetInstance<IMyCrossCuttingConcern>();
      }

      public override void OnEntry(MethodExecutionArgs args)
      {
         if (!AspectSettings.On) return;
         _concern.BeforeMethod("before");
      }

      public override void OnSuccess(MethodExecutionArgs args)
      {
         if (!AspectSettings.On) return;
         _concern.AfterMethod("after");
      }
   }
}
