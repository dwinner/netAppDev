/*
 * Pattern matching in switch stmt and local static functions
 */

using System;

namespace NetFxConsoleApp
{
   internal static class Program
   {
      private static void Main()
      {
         (Cylinder c, Sphere s, Pyramid p) shapeTuple = new(
            new Cylinder(2.2, 5.0),
            new Sphere(3),
            new Pyramid(.0, .0, .0));
         TotalObjectVolume(shapeTuple);
      }

      private static double TotalObjectVolume((Cylinder c, Sphere s, Pyramid p) volumeShapes)
      {
         var cylinderVol = CalculateVolume(volumeShapes.c);
         var sphereVol = CalculateVolume(volumeShapes.s);
         var pyramidVol = CalculateVolume(volumeShapes.p);

         return Math.Round(cylinderVol + sphereVol + pyramidVol, 2);

         // static local functions here
         static double CalculateVolume<T>(T volumeShape)
         {
            return volumeShape switch
            {
               Sphere {Radius: 0} => 0,
               Cylinder c => Math.PI * Math.Pow(c.Radius, 2) * c.Length,
               Sphere s => 4 * Math.PI * Math.Pow(s.Radius, 3) / 3,
               Pyramid p => p.BaseLength * p.BaseWidth * p.Height / 3,
               _ => throw new ArgumentException("Unrecognized object", nameof(volumeShape))
            };
         }
      }
   }


   #region General supporting code

   public class Cylinder
   {
      public Cylinder(double length, double radius)
      {
         Length = length;
         Radius = radius;
      }

      public double Length { get; }
      public double Radius { get; }
   }

   public class Sphere
   {
      public Sphere(double radius) => Radius = radius;

      public double Radius { get; }
   }

   public class Pyramid
   {
      public Pyramid(double baseLength, double baseWidth, double height)
      {
         BaseLength = baseLength;
         BaseWidth = baseWidth;
         Height = height;
      }

      public double BaseLength { get; }
      public double BaseWidth { get; }
      public double Height { get; }
   }

   #endregion
}