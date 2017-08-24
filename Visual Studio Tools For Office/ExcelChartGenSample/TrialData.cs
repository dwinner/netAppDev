using System;
using System.Collections.Generic;
using ExcelChartGenSample.Poco;

namespace ExcelChartGenSample
{
   public static class TrialData
   {
      private const int LimitationValue = 5000;
      private static readonly Random RandomGenerator = new Random();

      private static readonly string[] ErrorCodes =
        {
            "V114",
            "V202",
            "V204",
            "V205",
            "V220",
            "V221",
            "V301",
            "V501",
            "V502",
            "V503",
            "V505",
            "V506",
            "V507",
            "V508",
            "V509",
            "V510",
            "V511",
            "V512",
            "V517",
            "V519",
            "V521",
            "V522",
            "V523",
            "V524",
            "V527",
            "V529",
            "V530",
            "V531",
            "V535",
            "V537",
            "V541",
            "V542",
            "V543",
            "V545",
            "V546",
            "V547",
            "V549",
            "V554",
            "V555",
            "V556",
            "V557",
            "V559",
            "V560",
            "V561",
            "V563",
            "V564",
            "V567",
            "V568",
            "V570",
            "V571",
            "V572",
            "V573",
            "V575",
            "V576",
            "V579",
            "V580",
            "V581",
            "V583",
            "V586",
            "V590",
            "V591",
            "V592",
            "V593",
            "V595",
            "V597",
            "V598",
            "V599",
            "V600",
            "V601",
            "V602",
            "V603",
            "V605",
            "V606",
            "V607",
            "V610",
            "V611",
            "V612",
            "V614",
            "V616",
            "V617",
            "V618",
            "V621",
            "V622",
            "V627",
            "V628",
            "V629",
            "V636",
            "V637",
            "V638",
            "V640",
            "V646",
            "V648",
            "V649",
            "V654",
            "V656",
            "V663",
            "V665",
            "V666",
            "V668",
            "V670",
            "V672",
            "V673",
            "V674",
            "V677",
            "V678",
            "V682",
            "V683",
            "V686",
            "V688",
            "V689",
            "V690",
            "V695",
            "V697",
            "V701",
            "V703",
            "V704",
            "V707",
            "V709",
            "V711",
            "V712",
            "V713",
            "V716",
            "V717",
            "V719",
            "V720",
            "V721"
        };

      public static IEnumerable<AnalysisRunStats> GetTestData()
      {
         var analysisRunStats = new List<AnalysisRunStats>();

         var monthAgo = DateTime.Now.Subtract(TimeSpan.FromDays(30));
         while (monthAgo.CompareTo(DateTime.Now) < 0)
         {
            analysisRunStats.Add(new AnalysisRunStats(monthAgo));
            monthAgo = monthAgo.AddDays(1);
         }

         var analyzerTypes = GetAnalyzerTypes();
         foreach (var analysisRunStat in analysisRunStats)
         {
            foreach (var analyzerType in analyzerTypes)
            {
               analysisRunStat.CodeStatistics[analyzerType] = GenerateRandomValues();
            }
         }

         return analysisRunStats;
      }

      private static SerializableDictionary<string, AnalyzerCodeStats> GenerateRandomValues()
      {
         var codeStats = new SerializableDictionary<string, AnalyzerCodeStats>();
         var maxRangeIndex = ErrorCodes.Length - 1;
         var randomErrorCodeCount = RandomGenerator.Next(maxRangeIndex);

         for (var i = 0; i < randomErrorCodeCount; i++)
         {
            var errorCode = ErrorCodes[RandomGenerator.Next(maxRangeIndex)];
            if (!codeStats.ContainsKey(errorCode))
            {
               codeStats.Add(errorCode, GenerateRandomStats());
            }
         }

         return codeStats;
      }

      private static AnalyzerCodeStats GenerateRandomStats()
      {
         return new AnalyzerCodeStats
         {
            MessagesCount = RandomGenerator.Next(LimitationValue),
            SuppressedMessagesCount = RandomGenerator.Next(LimitationValue)
         };
      }

      private static AnalyzerType[] GetAnalyzerTypes()
      {
         return (AnalyzerType[])Enum.GetValues(typeof(AnalyzerType));
      }
   }
}