using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Android.Util;
using SatelliteMovingApp.Lib.Model;
using static System.Environment;
using static System.IO.File;
using static System.IO.Path;
using static System.Linq.Enumerable;

namespace SatelliteMovingApp.Lib.Utils
{
   public sealed class SatelliteSettings
   {
      private static SatelliteSettings _satelliteSettings;
      private static readonly string _Root;

      static SatelliteSettings()
      {
         _Root = GetFolderPath(SpecialFolder.Personal);
      }

      private SatelliteSettings()
      {
      }

      public static SatelliteSettings Impl => _satelliteSettings ?? (_satelliteSettings = new SatelliteSettings());

      public IList<Satellite> Read(string settingFile)
      {
         var settingPath = Combine(_Root, settingFile);
         if (Exists(settingPath))
         {
            var xmlSerializer = GetXmlSerializer();
            using (var fileIns = OpenRead(settingPath))
            {
               try
               {
                  var satellites = xmlSerializer.Deserialize(fileIns) as SatelliteCollection;
                  return satellites?.Satellites ?? Empty<Satellite>().ToArray();
               }
               catch (XmlException xmlEx)
               {
                  Log.Warn(GetType().Name, xmlEx.Message, xmlEx);
                  return Empty<Satellite>().ToList();
               }
            }
         }

         Create(settingPath);
         return Empty<Satellite>().ToList();
      }

      public void Write(string settingFile, IList<Satellite> satellites)
      {
         var settingPath = Combine(_Root, settingFile);
         if (!Exists(settingPath))
            Create(settingPath);

         var xmlSerializer = GetXmlSerializer();
         using (var fileOs = OpenWrite(settingPath))
         {
            fileOs.SetLength(0);
            try
            {
               var satelliteCollection = new SatelliteCollection(satellites.ToArray());
               xmlSerializer.Serialize(fileOs, satelliteCollection);
            }
            catch (InvalidOperationException invalidOpEx)
            {
               Log.Error(GetType().Name, invalidOpEx.Message, invalidOpEx);
               throw;
            }
         }
      }

      private XmlSerializer GetXmlSerializer()
      {
         try
         {
            var xmlAttributes = new XmlAttributes();
            xmlAttributes.XmlElements.Add(new XmlElementAttribute(nameof(Satellite), typeof(Satellite)));
            var xmlAttributeOverrides = new XmlAttributeOverrides();
            xmlAttributeOverrides.Add(typeof(SatelliteCollection), nameof(SatelliteCollection), xmlAttributes);
            var xmlSerializer = new XmlSerializer(typeof(SatelliteCollection), xmlAttributeOverrides);
            return xmlSerializer;
         }
         catch (InvalidOperationException invalidOpEx)
         {
            Log.Error(GetType().Name, invalidOpEx.Message, invalidOpEx);
            throw;
         }
      }
   }
}