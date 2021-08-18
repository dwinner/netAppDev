// Реализация описателя привязки

using System;
using System.Reflection;
using Lib;

namespace CustomBinderSample
{
   internal static class Program
   {
      private static void Main()
      {
         object o = new SomeType();
         var t = typeof (SomeType);
         var binder = new SimpleBinder();
         const BindingFlags bf = BindingFlags.Public | BindingFlags.Instance;

         short b = 5;

         // Calls Void v(Int32)
         t.InvokeMember("v", bf | BindingFlags.InvokeMethod, binder, o, new object[] {b});

         // Calls Void m(Int32)
         t.InvokeMember("m", bf | BindingFlags.InvokeMethod, binder, o, new object[] {1});

         // Calls Void m(System.Object)
         t.InvokeMember("m", bf | BindingFlags.InvokeMethod, binder, o, new[] {new object()});

         // Calls Void m(Double)
         t.InvokeMember("m", bf | BindingFlags.InvokeMethod, binder, o, new object[] {1.4});

         // Calls Void m(SomeType)
         t.InvokeMember("m", bf | BindingFlags.InvokeMethod, binder, o, new[] {o});

         // Calls Void m(System.Object) since m(String) is private
         t.InvokeMember("m", bf | BindingFlags.InvokeMethod, binder, o, new object[] {"string"});

         // Calls Void m(System.String) since NonPublic is specified
         t.InvokeMember("m", bf | BindingFlags.NonPublic | BindingFlags.InvokeMethod, binder, o, new object[] {"string"});

         try
         {
            // Throws because there is no public method which takes exactly a string 
            t.InvokeMember("m", bf | BindingFlags.InvokeMethod | BindingFlags.ExactBinding, binder, o,
               new object[] {"string"});
         }
         catch (MissingMethodException)
         {
            Console.WriteLine("Invocation failed on m(String), bad binding flags - ExactBinding too restrictive");
         }

         try
         {
            // Throws because there is no method g which takes only 2 args 
            t.InvokeMember("g", bf | BindingFlags.InvokeMethod, binder, o, new object[] {1, "string"});
         }
         catch (MissingMethodException)
         {
            Console.WriteLine("Invocation failed on g(int, Object, String), wrong number of args");
         }

         // Calls Void g(Int32, System.Object, System.String)
         t.InvokeMember("g", bf | BindingFlags.InvokeMethod, binder, o, new object[] {1, "string", "string"});

         // Calls Void h()
         t.InvokeMember("h", bf | BindingFlags.NonPublic | BindingFlags.InvokeMethod, binder, o, new object[] {});

         try
         {
            // Throws because BindingFlags.Static has not been specified 
            t.InvokeMember("s", bf | BindingFlags.InvokeMethod, binder, o, new object[] {});
         }
         catch (MissingMethodException)
         {
            Console.WriteLine("Invocation failed on static s(), bad binding flags - need Static");
         }

         // Calls Void s()
         t.InvokeMember("s", bf | BindingFlags.InvokeMethod | BindingFlags.Static, binder, o, new object[] {});

         // Calls Void m(Int32, Double)
         t.InvokeMember("m", bf | BindingFlags.InvokeMethod, binder, o, new object[] {1, 1});

         Console.WriteLine("Press <Enter> to exit.");
         Console.ReadLine();
      }
   }
}