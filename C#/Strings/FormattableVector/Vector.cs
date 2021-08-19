using System;
using System.Globalization;
using System.Text;

namespace FormattableVector
{
   public struct Vector : IFormattable, IEquatable<Vector>
   {
      private const double Epsilon = 1E-7;

      public double X { get; private set; }

      public double Y { get; private set; }

      public double Z { get; private set; }

      public Vector(double x, double y, double z)
         : this()
      {
         X = x;
         Y = y;
         Z = z;
      }

      public Vector(Vector aVector)
         : this()
      {
         X = aVector.X;
         Y = aVector.Y;
         Z = aVector.Z;
      }

      public bool Equals(Vector other)
      {
         return this == other;
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj))
            return false;
         return obj is Vector && Equals((Vector)obj);
      }

      public override int GetHashCode()
      {
         unchecked
         {
            int hashCode = X.GetHashCode();
            hashCode = (hashCode * 397) ^ Y.GetHashCode();
            hashCode = (hashCode * 397) ^ Z.GetHashCode();
            return hashCode;
         }
      }

      public static bool operator ==(Vector lhs, Vector rhs)
      {
         return Math.Abs(lhs.X - rhs.X) < Epsilon && Math.Abs(lhs.Y - rhs.Y) < Epsilon &&
                Math.Abs(lhs.Z - rhs.Z) < Epsilon;
      }

      public static bool operator !=(Vector lhs, Vector rhs)
      {
         return !(lhs == rhs);
      }

      public static Vector operator +(Vector lhs, Vector rhs)
      {
         var resultVector = new Vector(lhs);
         resultVector.X += rhs.X;
         resultVector.Y += rhs.Y;
         resultVector.Z += rhs.Z;
         return resultVector;
      }

      public static Vector operator *(double lhs, Vector rhs)
      {
         return new Vector(lhs * rhs.X, lhs * rhs.Y, lhs * rhs.Z);
      }

      public static Vector operator *(Vector lhs, double rhs)
      {
         return rhs * lhs;
      }

      public static double operator *(Vector lhs, Vector rhs)
      {
         return lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z;
      }

      public double Norm()
      {
         return X * X + Y * Y + Z * Z;
      }

      public double this[uint i]
      {
         get
         {
            switch (i)
            {
               case 0:
                  return X;
               case 1:
                  return Y;
               case 2:
                  return Z;
               default:
                  throw new IndexOutOfRangeException(string.Format("Attempt to retrieve Vector element{0}", i));
            }
         }
         set
         {
            switch (i)
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
               default:
                  throw new IndexOutOfRangeException(string.Format("Attempt to set Vector element{0}", i));
            }
         }
      }

      public override string ToString()
      {
         return string.Format("( {0} , {1} , {2} )", X, Y, Z);
      }

      public string ToString(string format, IFormatProvider formatProvider)
      {
         if (format == null)
            return ToString();
         switch (format.ToUpper())
         {
            case "N":
               return string.Format("|| {0} ||", Norm());
            case "VE":
               return string.Format("( {0:E}, {1:E}, {2:E} )", X, Y, Z);
            case "IJK":
               var stringBuilder = new StringBuilder(X.ToString(CultureInfo.InvariantCulture), 30);
               return
                  stringBuilder.Append(" i + ")
                     .Append(Y.ToString(CultureInfo.InvariantCulture))
                     .Append(" j + ")
                     .Append(Z.ToString(CultureInfo.InvariantCulture))
                     .Append(" k")
                     .ToString();
            default:
               return ToString();
         }
      }
   }
}