using System;
using System.Globalization;
using System.Text;

namespace HowToCSharp.Ch02.VertexDemo
{
   internal struct Vertex3I
   {
      public int Id { get; set; }

      public int X { get; set; }

      public int Y { get; set; }

      public int Z { get; set; }

      public Vertex3I(int x, int y, int z) : this()
      {
         X = x;
         Y = y;
         Z = z;

         Id = 0;
      }

      public override string ToString() => ToString("G", null);

      public string ToString(string format, IFormatProvider formatProvider)
      {
         if (format == null)
         {
            format = "G";
         }

         if (formatProvider != null)
         {
            var formatter = formatProvider.GetFormat(GetType()) as ICustomFormatter;
            if (formatter != null)
            {
               return formatter.Format(format, this, formatProvider);
            }
         }

         if (format == "G")
         {
            return string.Format("({0}, {1}, {2})", X, Y, Z);
         }

         var stringBuilder = new StringBuilder();
         var sourceIndex = 0;

         while (sourceIndex < format.Length)
         {
            switch (format[sourceIndex])
            {
               case 'X':
                  stringBuilder.Append(X.ToString(CultureInfo.InvariantCulture));
                  break;
               case 'Y':
                  stringBuilder.Append(Y.ToString(CultureInfo.InvariantCulture));
                  break;
               case 'Z':
                  stringBuilder.Append(Z.ToString(CultureInfo.InvariantCulture));
                  break;
               default:
                  stringBuilder.Append(format[sourceIndex]);
                  break;
            }

            sourceIndex++;
         }

         return stringBuilder.ToString();
      }

      public static implicit operator Vertex3D(Vertex3I vertex) => new Vertex3D(vertex.X, vertex.Y, vertex.Z);
   }
}