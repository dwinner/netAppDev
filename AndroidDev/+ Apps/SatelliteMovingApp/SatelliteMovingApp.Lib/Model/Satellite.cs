using System;
using System.Xml.Serialization;
using Math = Java.Lang.Math;

namespace SatelliteMovingApp.Lib.Model
{
   /// <summary>
   ///    Класс-модель для параметров спутника
   /// </summary>
   [XmlRoot(nameof(Satellite))]
   [Serializable]
   public sealed class Satellite
      : IEquatable<Satellite>
   {
      private const long SerialVersionUid = -1101121520854880773L;
      private const long DefaultRoundingTime = 0;
      private const float DefaultDistance = .0F;
      private const float DefaultAngle = .0F;

      public Satellite(long roundingTime = DefaultRoundingTime, float distance = DefaultDistance,
         float angle = DefaultAngle)
      {
         RoundingTime = roundingTime;
         Distance = distance;
         Angle = angle;
      }

      public Satellite()
      {         
      }

      /// <summary>
      ///    Время, в миллисекундах, за которое спутник делает один оборот вокруг земли
      /// </summary>
      [XmlElement]
      public long RoundingTime { get; set; }

      /// <summary>
      ///    Расстояние спутника, в dp, от центра земли
      /// </summary>
      [XmlElement]
      public float Distance { get; set; }

      /// <summary>
      ///    Начальное положение спутника на орбите, в радианах
      /// </summary>
      [XmlElement]
      public float Angle { get; set; }

      [XmlIgnore]
      public static float RandomAngle => (float) (2 * Math.Pi * Math.Random());

      #region IEquatable<Satellite>, System.Object

      public bool Equals(Satellite other)
         =>
            !ReferenceEquals(null, other) &&
            (ReferenceEquals(this, other) ||
             RoundingTime == other.RoundingTime && Distance.Equals(other.Distance) && Angle.Equals(other.Angle));

      public override string ToString()
         => $"{nameof(RoundingTime)}: {RoundingTime}, {nameof(Distance)}: {Distance}, {nameof(Angle)}: {Angle}";

      public override bool Equals(object obj)
         => !ReferenceEquals(null, obj) && (ReferenceEquals(this, obj) || obj is Satellite && Equals((Satellite) obj));

      public override int GetHashCode()
      {
         unchecked
         {
            var hashCode = RoundingTime.GetHashCode();
            hashCode = (hashCode * 397) ^ Distance.GetHashCode();
            hashCode = (hashCode * 397) ^ Angle.GetHashCode();
            return hashCode;
         }
      }

      public static bool operator ==(Satellite left, Satellite right) => Equals(left, right);

      public static bool operator !=(Satellite left, Satellite right) => !Equals(left, right);

      #endregion
   }
}