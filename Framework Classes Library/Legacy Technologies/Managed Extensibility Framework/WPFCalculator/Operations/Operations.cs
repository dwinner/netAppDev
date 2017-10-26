using System;
using System.ComponentModel.Composition;
using CalculatorUtils;

namespace Operations
{
   public class Operations
   {
      [SpeedExport("Add", typeof(Func<double, double, double>), Speed = Speed.Fast)]
      public double Add(double x, double y)
      {
         return x + y;
      }

      [Export("Subtract", typeof(Func<double, double, double>))]
      public double Subtract(double x, double y)
      {
         return x - y;
      }
   }
}