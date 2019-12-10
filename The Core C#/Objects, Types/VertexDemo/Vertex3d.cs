using System;
using System.Globalization;
using System.Text;

namespace HowToCSharp.Ch02.VertexDemo
{
   internal struct Vertex3D : IFormattable, IEquatable<Vertex3D>, IComparable<Vertex3D>
   {
      private const double Epsilon = 10E-6;
      public int? Id { get; set; }

      public double X { get; set; }

      public double Y { get; set; }

      public double Z { get; set; }

      public double this[int index]
      {
         get
         {
            switch (index)
            {
               case 0: return X;
               case 1: return Y;
               case 2: return Z;
               default: throw new ArgumentOutOfRangeException("index", "Only indexes 0-2 valid!");
            }
         }
         set
         {
            switch (index)
            {
               case 0:
                  X = value;
                  break;
               case 1:
                  Y = value;
                  break;
               case 2:
                  Z = value;
                  break;
               default: throw new ArgumentOutOfRangeException("index", "Only indexes 0-2 valid!");
            }
         }
      }

      public double this[string dimension]
      {
         get
         {
            switch (dimension.ToUpper())
            {
               case "X": return X;
               case "Y": return Y;
               case "Z": return Z;
               default: throw new ArgumentOutOfRangeException("dimension", "Only dimensions X, Y, and Z are valid!");
            }
         }
         set
         {
            switch (dimension.ToUpper())
            {
               case "X":
                  X = value;
                  break;
               case "Y":
                  Y = value;
                  break;
               case "Z":
                  Z = value;
                  break;
               default: throw new ArgumentOutOfRangeException("dimension", "Only dimensions X, Y, and Z are valid!");
            }
         }
      }


      public Vertex3D(double x, double y, double z)
         : this()
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
            var formatter = formatProvider.GetFormat(GetType())
               as ICustomFormatter;
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

      public override bool Equals(object obj)
      {
         if (obj == null)
         {
            return false;
         }

         if (obj.GetType() != GetType())
         {
            return false;
         }

         return Equals((Vertex3D) obj);
      }

      public bool Equals(Vertex3D other) =>
         Math.Abs(X - other.X) < Epsilon
         && Math.Abs(Y - other.Y) < Epsilon
         && Math.Abs(Z - other.Z) < Epsilon;

      public override int GetHashCode() =>
         (((int) X ^ (int) Z) << 16) |
         (((int) Y ^ (int) Z) & 0x0000FFFF);

      public int CompareTo(Vertex3D other) =>
         Id < other.Id
            ? -1
            : Id == other.Id
               ? 0
               : 1;

      public static Vertex3D operator +(Vertex3D a, Vertex3D b) => new Vertex3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

      public static bool operator ==(Vertex3D a, Vertex3D b) => a.Equals(b);

      public static bool operator !=(Vertex3D a, Vertex3D b) => !a.Equals(b);

      public static explicit operator Vertex3I(Vertex3D vertex) =>
         new Vertex3I((int) vertex.X, (int) vertex.Y, (int) vertex.Z);
   }
}