using System;
using System.Xml.Serialization;

namespace SatelliteMovingApp.Lib.Model
{
   [Serializable]
   [XmlInclude(typeof(Satellite))]
   public sealed class SatelliteCollection
   {
      public SatelliteCollection(Satellite[] satellites)
      {
         Satellites = satellites;
      }

      public SatelliteCollection()
      {
      }

      [XmlArrayItem(nameof(Satellite), typeof(Satellite))]
      public Satellite[] Satellites { get; set; }
   }
}