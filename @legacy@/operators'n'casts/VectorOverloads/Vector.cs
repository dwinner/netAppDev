using System;

namespace VectorOverloads
{
   public struct Vector : IEquatable<Vector>
   {
      public double X { get; set; }

      public double Y { get; set; }

      public double Z { get; set; }

      public Vector(double x, double y, double z)
         : this()
      {
         X = x;
         Y = y;
         Z = z;
      }

      public Vector(Vector otherVector)
         : this()
      {
         X = otherVector.X;
         Y = otherVector.Y;
         Z = otherVector.Z;
      }

      public override string ToString()
      {
         return string.Format("( {0} , {1} , {2} )", X, Y, Z);
      }

      public bool Equals(Vector other)
      {
         return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
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

      #region Перегруженные операторы

      public static bool operator ==(Vector left, Vector right)
      {
         return left.Equals(right);
      }

      public static bool operator !=(Vector left, Vector right)
      {
         return !left.Equals(right);
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

      public static Vector operator *(Vector lhs, Vector rhs)
      {
         return new Vector(lhs.Y * rhs.Z - lhs.Z * rhs.Y, lhs.Z * rhs.X - lhs.X * rhs.Z, lhs.X * rhs.Y - lhs.Y * rhs.X);
      }

      #endregion

   }
}