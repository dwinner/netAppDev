using System;
using System.Collections.Generic;
using System.Text;

namespace HowToCSharp.Ch02.VertexDemo
{
   internal class Program
   {
      private static void Main(string[] args)
      {
         var v = new Vertex3D(1.0, 2.0, 3.0);
         var v2 = new Vertex3D(4.0, 5.0, 6.0);
         var v3 = new Vertex3D(7.0, 8.0, 9.0);

         Console.WriteLine("ToString(): " + v);
         Console.WriteLine("ToString() with custom formatting: " + v.ToString("[x:X, y:Y]", null));

         var formatter = new TypeFormatter();
         var sb = new StringBuilder();
         sb.AppendFormat(formatter, "Custom formatter: {0:(X Y)}; {1:[X, Y, Z]}", v, v2);

         Console.WriteLine(sb.ToString());

         v.Id = 1;
         v2.Id = 2;
         v3.Id = 3;

         var vertices = new List<Vertex3D> {v3, v2, v};

         PrintList("Before sort:", vertices);

         vertices.Sort();

         PrintList("After sort:", vertices);

         v[1] = 13.0;
         Console.WriteLine("Change Y to 13 via index: " + v);
         v["Y"] = 2.0;
         Console.WriteLine("Change Y to 2 via string: " + v);

         var sum = v + v2;
         Console.WriteLine("{0} + {1} = {2}", v, v2, v + v2);

         var vd = new Vertex3D(1.5, 2.5, 3.5);
         var vi = (Vertex3I) vd;

         Console.WriteLine("vi = (Vertex3i)vd;: vd: {0}, vi: {1}", vd, vi);

         vd = vi;
         Console.WriteLine("vd = vi: vd: {0}, vi: {1}", vd, vi);

         var vn = new Vertex3D(1, 2, 3) {Id = 3};
         //ok
         vn.Id = null; //ok
         try
         {
            Console.WriteLine("ID: {0}", vn.Id.Value); //throws
         }
         catch (InvalidOperationException)
         {
            Console.WriteLine("Oops--you can't get a null value!");
         }

         if (vn.Id.HasValue)
         {
            Console.WriteLine("ID: {0}", vn.Id.Value);
         }

         Console.ReadLine();
      }

      private static void PrintList(string message, List<Vertex3D> vertices)
      {
         Console.WriteLine(message);
         foreach (var v in vertices)
         {
            Console.WriteLine(v.ToString());
         }
      }
   }
}