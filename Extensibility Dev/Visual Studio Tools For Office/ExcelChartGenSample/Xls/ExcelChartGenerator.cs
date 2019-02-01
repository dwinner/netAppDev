using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelChartGenSample.Poco;
using Microsoft.Office.Interop.Excel;
using ExcelApp = Microsoft.Office.Interop.Excel.Application;

namespace ExcelChartGenSample.Xls
{
   public sealed class ExcelChartGenerator
   {
      private readonly IList<AnalysisRunStats> _analysisRunStats;

      public ExcelChartGenerator(IEnumerable<AnalysisRunStats> analysisRunStats)
      {
         _analysisRunStats = new List<AnalysisRunStats>();
         foreach (var analysisRunStat in analysisRunStats)
         {
            _analysisRunStats.Add(analysisRunStat);
         }
      }

      #region Статические поля и методы

      private static bool IsExcelInstalled
      {
         get { return Type.GetTypeFromProgID("Excel.Application") != null; }
      }

      #endregion

      public bool Render(out string errorMessage)
      {
         if (!IsExcelInstalled)
         {
            errorMessage = "Excel isn't installed on your PC";
            return false;
         }

         var timestamps = GetSortedTimestamps();
         return Render(timestamps, out errorMessage);
      }

      private bool Render(DateTime[] timestamps, out string errorMessage)
      {
         var statCriterion = GenUtils.GetEnumValues<StatCriteria>();

         try
         {
            var excelHelper = new ExcelHelper();

            foreach (var statCriteria in statCriterion)
            {
               switch (statCriteria)
               {
                  case StatCriteria.ByAnalyzerType:
                     var typeStatMap = GetAnalyzerTypeStatMap(timestamps);
                     var typeAdaptData = TransformToMatrix(timestamps, typeStatMap);
                     excelHelper.AddChart(
                         "By analyzer type", "Analyzer chart", "Stat by analyzer type", typeAdaptData);
                     break;

                  case StatCriteria.ByErrorCode:
                     var codeStatMap = GetErrorCodeStatMap(timestamps);
                     var codeAdaptData = TransformToMatrix(timestamps, codeStatMap);
                     excelHelper.AddChart(
                         "By error code", "Code chart", "Stat by error code", codeAdaptData);
                     break;

                  default:
                     goto case StatCriteria.ByAnalyzerType;
               }
            }

            errorMessage = string.Empty;
            return true;
         }
         catch (Exception commonEx)
         {
            errorMessage = commonEx.Message;
            return false;
         }
      }

      private DateTime[] GetSortedTimestamps()
      {
         var dateTimes = _analysisRunStats.Select(analysisRunStat => analysisRunStat.CreationStamp).ToList();
         dateTimes.Sort(Comparer<DateTime>.Default);
         return dateTimes.ToArray();
      }

      private Dictionary<AnalyzerType, int[]> GetAnalyzerTypeStatMap(DateTime[] timestamps)
      {
         var statMap = new Dictionary<AnalyzerType, int[]>();

         foreach (var runStat in _analysisRunStats)
         {
            var currentDate = runStat.CreationStamp;
            var dtIndex = Array.FindIndex(timestamps, time => time == currentDate);

            foreach (var codeStatistic in runStat.CodeStatistics)
            {
               var analyzerType = codeStatistic.Key;
               if (!statMap.ContainsKey(analyzerType))
               {
                  statMap[analyzerType] = new int[timestamps.Length];
               }

               var total = codeStatistic.Value.Sum(stat => stat.Value.MessagesCount);
               statMap[analyzerType][dtIndex] = total;
            }
         }

         return statMap;
      }

      private Dictionary<string, int[]> GetErrorCodeStatMap(DateTime[] timestamps)
      {
         var statMap = new Dictionary<string, int[]>();

         foreach (var runStat in _analysisRunStats)
         {
            var currentDate = runStat.CreationStamp;
            var dtIndex = Array.FindIndex(timestamps, time => time == currentDate);

            foreach (var codeStatistic in runStat.CodeStatistics)
            {
               foreach (var analyzerCodeStats in codeStatistic.Value)
               {
                  var errorCode = analyzerCodeStats.Key.ToUpper();
                  if (!statMap.ContainsKey(errorCode))
                  {
                     statMap[errorCode] = new int[timestamps.Length];
                  }

                  statMap[errorCode][dtIndex] += analyzerCodeStats.Value.MessagesCount;
               }
            }
         }

         return statMap;
      }

      private static object[,] TransformToMatrix<T>(IList<DateTime> timestamps, Dictionary<T, int[]> statMap)
      {
         var excelData = new object[statMap.Count + 1, timestamps.Count + 1];
         excelData[0, 0] = "Plog date";
         for (var i = 0; i < timestamps.Count; i++)
         {
            excelData[0, i + 1] = timestamps[i].ToString("g");
         }

         var types = new List<T>(timestamps.Count);
         types.AddRange(statMap.Keys);
         for (var i = 0; i < types.Count; i++)
         {
            var genType = types[i];
            string columnLegend;
            if (genType.GetType().IsEnum)
            {
               var analyzerType = (AnalyzerType)Enum.Parse(typeof(AnalyzerType), genType.ToString());
               columnLegend = AnalyzerTypeUtils.GetShortName(analyzerType);
            }
            else
            {
               columnLegend = genType.ToString();
            }

            excelData[i + 1, 0] = columnLegend;
            var statValues = statMap[genType];
            for (var j = 0; j < statValues.Length; j++)
            {
               excelData[i + 1, j + 1] = statValues[j];
            }
         }

         return excelData;
      }

      #region Вспомогательные типы

      private enum StatCriteria
      {
         ByAnalyzerType,
         ByErrorCode
      }

      private sealed class ExcelHelper
      {
         private const string UpperLeftCell = "A1";
         private const string ExcelColumnLetters = "zabcdefghijklmnopqrstuvwxyz";
         private static readonly object MissingCap = Type.Missing;
         private readonly ExcelApp _app;

         public ExcelHelper()
         {
            _app = new ExcelApp { Visible = true, DisplayAlerts = true, DisplayFormulaBar = false };
         }

         public void AddChart(string sheetName, string chartName, string chartTitle, object[,] data)
         {
            var rowCount = data.GetLength(0);
            var columnCount = data.GetLength(1);

            var book = _app.Workbooks.Add(XlSheetType.xlWorksheet);
            var activeSheet = (Worksheet)_app.ActiveSheet;
            activeSheet.Name = sheetName;

            var chart = (Chart)book.Charts.Add(MissingCap, activeSheet, MissingCap, MissingCap);
            chart.Name = chartName;

            var endRowNumber = int.Parse(UpperLeftCell.Substring(1)) + rowCount - 1;
            var endColumnSequence = GetExcelColumnName(columnCount - 1);
            var lowerRightCell = String.Format("{0}{1}", endColumnSequence, endRowNumber);

            var range = activeSheet.Range[UpperLeftCell, lowerRightCell];
            range.Value2 = data;
            chart.ChartType = XlChartType.xlXYScatterLines;
            chart.SetSourceData(range, XlRowCol.xlRows);

            // Добавляем заголовок
            chart.HasTitle = true;
            chart.ChartTitle.Text = chartTitle;
            chart.HasLegend = true;
         }

         private static string GetExcelColumnName(int columnIndex)
         {
            var numberInLetters = new StringBuilder();
            columnIndex += 1;
            while (columnIndex > 0)
            {
               int modOf26;
               if (columnIndex <= 26)
               {
                  modOf26 = columnIndex;
                  numberInLetters.Insert(0, ExcelColumnLetters.Substring(modOf26, 1));
                  columnIndex = 0;
               }
               else
               {
                  modOf26 = columnIndex % 26;
                  var subtract = (modOf26 == 0) ? 26 : modOf26;
                  columnIndex = (columnIndex - subtract) / 26;
                  numberInLetters.Insert(0, ExcelColumnLetters.Substring(modOf26, 1));
               }
            }

            return numberInLetters.ToString().ToUpper();
         }
      }

      #endregion
   }

   public static class GenUtils
   {
      public static T[] GetEnumValues<T>()
      {
         return typeof(T).IsEnum ? (T[])Enum.GetValues(typeof(T)) : new T[0];
      }
   }
}