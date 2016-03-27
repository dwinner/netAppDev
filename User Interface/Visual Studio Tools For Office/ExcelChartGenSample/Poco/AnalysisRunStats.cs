using System;

namespace ExcelChartGenSample.Poco
{
   public class AnalysisRunStats
   {
      public readonly SerializableDictionary<AnalyzerType, SerializableDictionary<string, AnalyzerCodeStats>> CodeStatistics =
          new SerializableDictionary<AnalyzerType, SerializableDictionary<string, AnalyzerCodeStats>>();

      public DateTime CreationStamp;

      public AnalysisRunStats(DateTime stamp)
      {
         CreationStamp = stamp;
      }
   }
}