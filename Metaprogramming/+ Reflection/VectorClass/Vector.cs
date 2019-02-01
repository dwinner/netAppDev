using System;
using System.Collections;
using System.Globalization;
using System.Text;
using WhatsNewAttributes;
// ReSharper disable HeapView.ObjectAllocation.Evident
// ReSharper disable UnthrowableException

[assembly: SupportsWhatsNew]

namespace VectorClass
{
   [LastModified("14 Feb 2010", "IEnumerable interface implemented So Vector can now be treated as a collection")]
   [LastModified("10 Feb 2010",
      "IFormattable interface implemented So Vector now responds to format specifiers N and VE")]
   public class Vector : IFormattable, IEnumerable, IEquatable<Vector>
   {
      public Vector(double x, double y, double z)
      {
         X = x;
         Y = y;
         Z = z;
      }

      public Vector(Vector rhs)
      {
         X = rhs.X;
         Y = rhs.Y;
         Z = rhs.Z;
      }

      public double this[uint i]
      {
         get
         {
            switch (i)
            {
               case 0: return X;
               case 1: return Y;
               case 2: return Z;
               default: throw new IndexOutOfRangeException($"Attempt to retrieve Vector element{i}");
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
               default: throw new IndexOutOfRangeException($"Attempt to set Vector element{i}");
            }
         }
      }

      public double X { get; private set; }

      public double Y { get; private set; }

      public double Z { get; private set; }

      [LastModified("14 Feb 2010", "Method added in order to provide collection support")]
      public IEnumerator GetEnumerator() => new VectorEnumerator(this);

      public bool Equals(Vector other)
      {
         if (ReferenceEquals(null, other))
            return false;

         if (ReferenceEquals(this, other))
            return true;

         return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
      }

      [LastModified("10 Feb 2010", "Method added in order to provide formatting support")]
      public string ToString(string format, IFormatProvider formatProvider)
      {
         if (format == null)
            return ToString();
         var formatUpper = format.ToUpper();
         switch (formatUpper)
         {
            case "N":
               return $"|| {Norm()} ||";

            case "VE":
               return $"( {X:E}, {Y:E}, {Z:E} )";

            case "IJK":
               var sb = new StringBuilder(X.ToString(CultureInfo.InvariantCulture), 30);
               sb.Append(" i + ");
               sb.Append(Y.ToString(CultureInfo.InvariantCulture));
               sb.Append(" j + ");
               sb.Append(Z.ToString(CultureInfo.InvariantCulture));
               sb.Append(" k");
               return sb.ToString();
            default:
               return ToString();
         }
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj))
            return false;

         if (ReferenceEquals(this, obj))
            return true;

         return obj.GetType() == GetType() && Equals((Vector) obj);
      }

      public override int GetHashCode()
      {
         unchecked
         {
            // ReSharper disable once NonReadonlyFieldInGetHashCode
            var hashCode = X.GetHashCode();
            // ReSharper disable once NonReadonlyFieldInGetHashCode
            hashCode = (hashCode * 397) ^ Y.GetHashCode();
            // ReSharper disable once NonReadonlyFieldInGetHashCode
            hashCode = (hashCode * 397) ^ Z.GetHashCode();

            return hashCode;
         }
      }

      public override string ToString() => $"( {X} , {Y} , {Z} )";

      public static bool operator ==(Vector lhs, Vector rhs) =>
         rhs != null && lhs != null && Math.Abs(lhs.X - rhs.X) < double.Epsilon &&
         Math.Abs(lhs.Y - rhs.Y) < double.Epsilon && Math.Abs(lhs.Z - rhs.Z) < double.Epsilon;

      public static bool operator !=(Vector lhs, Vector rhs) => !(lhs == rhs);

      public static Vector operator +(Vector lhs, Vector rhs)
      {
         var result = new Vector(lhs);
         result.X += rhs.X;
         result.Y += rhs.Y;
         result.Z += rhs.Z;

         return result;
      }

      public static Vector operator *(double lhs, Vector rhs) => new Vector(lhs * rhs.X, lhs * rhs.Y, lhs * rhs.Z);

      public static Vector operator *(Vector lhs, double rhs) => rhs * lhs;

      public static double operator *(Vector lhs, Vector rhs) => lhs.X * rhs.X + lhs.Y + rhs.Y + lhs.Z * rhs.Z;

      public double Norm() => X * X + Y * Y + Z * Z;

      #region Enumerator class

      [LastModified("14 Feb 2010", "Class created as part of collection support for Vector")]
      private class VectorEnumerator : IEnumerator
      {
         private readonly Vector _theVector;
         private int _location;

         public VectorEnumerator(Vector theVector)
         {
            _theVector = theVector;
            _location = -1;
         }

         public bool MoveNext() => ++_location <= 2;

         public object Current
         {
            get
            {
               if (_location < 0 || _location > 2)
                  throw new InvalidOperationException(
                     "The enumerator is either before the first element or after the last element of the Vector");

               return _theVector[(uint) _location];
            }
         }

         public void Reset()
         {
            _location = -1;
         }
      }

      #endregion
   }
}