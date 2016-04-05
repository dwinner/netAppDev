// Критический объект финализации

using System;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Windows.Forms;

namespace _03_CriticalFinalizerIObjSample
{
   internal static class Program
   {
      private static void Main()
      {
         var dangerType = new DangerType();
         var bulletProofType = new BulletProofType();

         Console.WriteLine("Press enter to fail fast");
         Console.ReadLine();

         //Environment.FailFast("Fail fast");
      }
   }

   internal class DangerType
   {
      ~DangerType()
      {
         Debug.WriteLine("Not bullet proof");
         MessageBox.Show("Not bullet proof");
      }
   }

   internal class BulletProofType : CriticalFinalizerObject
   {
      ~BulletProofType()
      {
         Debug.WriteLine("Bullet proof");
         MessageBox.Show("Bullet proof");
      }
   }
}