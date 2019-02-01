using System;
using System.Threading;
using CalculatorUtils;

namespace Operations
{
   public class Operations2
   {
      [SpeedExport("Add", typeof(Func<double, double, double>), Speed = Speed.Slow)]
      public double Add(double x, double y)
      {
         Thread.Sleep(3000);
         return x + y;
      }
   }
}