using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace SqlTypes
{
   [Serializable]
   [SqlUserDefinedType(Format.Native)]
   public struct SqlCoordinate : INullable
   {
      private readonly int _latitude;
      private readonly int _longitude;

      public SqlCoordinate(int longitude, int latitude)
         : this()
      {
         _longitude = longitude;
         _latitude = latitude;
         IsNull = false;
      }

      public SqlCoordinate(int longitudeDegrees, int longitudeMinutes, int longitudeSeconds, int latitudeDegrees,
         int latitudeMinutes, int latitudeSeconds, Orientation orientation)
         : this()
      {
         IsNull = false;
         _longitude = longitudeSeconds + 60 * longitudeMinutes + 3600 * longitudeDegrees;
         _latitude = latitudeSeconds + 60 * latitudeMinutes + 3600 * latitudeDegrees;

         switch (orientation)
         {
            case Orientation.SouthWest:
               _longitude = -_longitude;
               _latitude = -_latitude;
               break;
            case Orientation.SouthEast:
               _longitude = -_longitude;
               break;
            case Orientation.NorthWest:
               _latitude = -_latitude;
               break;
         }
      }

      public static SqlCoordinate Null
      {
         get { return new SqlCoordinate { IsNull = true }; }
      }

      public bool IsNull { get; private set; }

      public override string ToString()
      {
         if (IsNull) return null;

         char northSouth = _longitude > 0 ? 'N' : 'S';
         char eastWest = _latitude > 0 ? 'E' : 'W';

         int longitudeDegrees = Math.Abs(_longitude) / 3600;
         int remainingSeconds = Math.Abs(_longitude) % 3600;
         int longitudeMinutes = remainingSeconds / 60;
         int longitudeSeconds = remainingSeconds % 60;

         int latitudeDegrees = Math.Abs(_latitude) / 3600;
         remainingSeconds = Math.Abs(_latitude) % 3600;
         int latitudeMinutes = remainingSeconds / 60;
         int latitudeSeconds = remainingSeconds % 60;

         return string.Format("{0}˚{1}'{2}\"{3},{4}˚{5}'{6}\"{7}",
            longitudeDegrees, longitudeMinutes, longitudeSeconds,
            northSouth, latitudeDegrees, latitudeMinutes,
            latitudeSeconds, eastWest);
      }

      public static SqlCoordinate Parse(SqlString sqlString)
      {
         if (sqlString.IsNull)
            return Null;

         try
         {
            string[] coordinates = sqlString.Value.Split(',');
            char[] separators = { '°', '\'', '\"' };
            string[] longitudeVals = coordinates[0].Split(separators);
            string[] latitudeVals = coordinates[1].Split(separators);

            if (longitudeVals.Length != 4 && latitudeVals.Length != 4)
               throw new ArgumentException(
                  "Argument has a wrong syntax. This syntax is required: 37\u02da47\'0\"N,122\u02da26\'0\"W");

            Orientation orientation;
            if (longitudeVals[3] == "N" && latitudeVals[3] == "E")
               orientation = Orientation.NorthEast;
            else if (longitudeVals[3] == "S" && latitudeVals[3] == "W")
               orientation = Orientation.SouthWest;
            else if (longitudeVals[3] == "S" && latitudeVals[3] == "E")
               orientation = Orientation.SouthEast;
            else
               orientation = Orientation.NorthWest;

            return new SqlCoordinate(
               int.Parse(longitudeVals[0]), int.Parse(longitudeVals[1]),
               int.Parse(longitudeVals[2]),
               int.Parse(latitudeVals[0]), int.Parse(latitudeVals[1]),
               int.Parse(latitudeVals[2]), orientation);
         }
         catch (FormatException ex)
         {
            throw new ArgumentException(
               "Argument has a wrong syntax. This syntax is required: 37\u02da47\'0\"N,122\u02da26\'0\"W", ex.Message);
         }
      }
   }
}