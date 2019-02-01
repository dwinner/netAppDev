using System.ComponentModel;
using System.Xml.Serialization;

namespace ExcelChartGenSample.Poco
{
   public enum AnalyzerType
   {
      [Description("Fail")]
      [XmlEnum(Name = "Fail")]
      Unknown = 0,

      [Description("64 bit (64)")]
      [XmlEnum(Name = "64")]
      Viva64 = 1,

      [Description("General Analysis (GA)")]
      [XmlEnum(Name = "GA")]
      General = 4,

      [Description("Optimization (OP)")]
      [XmlEnum(Name = "OP")]
      Optimization = 5,

      [Description("Customer Specific (CS)")]
      [XmlEnum(Name = "CS")]
      CustomerSpecific = 6
   }
}