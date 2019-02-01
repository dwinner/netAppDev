// Архитектурная проверка имени свойства

using System;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using PostSharp.Reflection;

namespace CompileTimeValidateExample
{
   internal static class Program
   {
      [MyAspect]
      internal static string Horse { get; private set; }

      private static void Main()
      {
         Horse = "Matthew D. Groves";
         Console.WriteLine(Horse);
      }
   }

   [Serializable]
   public class MyAspect : LocationInterceptionAspect
   {
      public override bool CompileTimeValidate(LocationInfo locationInfo)
      {
         if (locationInfo.Name != nameof(Program.Horse))
         {
            Message.Write(locationInfo, SeverityType.Error, "MYERRORCODE01", "Location name must be 'horse'");
            return false;
         }

         return true;
      }

      public override void OnGetValue(LocationInterceptionArgs args)
      {
         Console.WriteLine("Property 'getter' was used");
         args.ProceedGetValue();
      }
   }
}