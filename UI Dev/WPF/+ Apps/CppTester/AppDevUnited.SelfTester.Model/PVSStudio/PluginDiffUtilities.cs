using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using PvsDataColumnNames = AppDevUnited.SelfTester.Model.CommonTypes.DataColumnNames;
using PvsTableIndexes = AppDevUnited.SelfTester.Model.CommonTypes.TableIndexes;

namespace AppDevUnited.SelfTester.Model.PVSStudio
{
   /// <summary>
   ///    Утилиты для обработки полей в Plugin'ах
   /// </summary>
   public static class PluginDiffUtilities
   {
      public static readonly int[] PvsExtendedPrimaryKeysPvsMode =
      {
         PvsTableIndexes.Line,
         PvsTableIndexes.ErrorCode,
         PvsTableIndexes.File,
         PvsTableIndexes.Message,
         PvsTableIndexes.FalseAlarm,
         PvsTableIndexes.Level
      };

      public static readonly SortedDictionary<int, string> PvsIndexNameMap =
         new SortedDictionary<int, string>(Comparer<int>.Default)
         {
            {PvsTableIndexes.FavIcon, PvsDataColumnNames.ErrorListFavIcon},
            {PvsTableIndexes.Level, PvsDataColumnNames.ErrorListLevel},
            {PvsTableIndexes.Order, PvsDataColumnNames.ErrorListOrder},
            {PvsTableIndexes.ErrorCode, PvsDataColumnNames.ErrorListErrorCode},
            {PvsTableIndexes.Message, PvsDataColumnNames.ErrorListMessage},
            {PvsTableIndexes.Project, PvsDataColumnNames.ErrorListProject},
            {PvsTableIndexes.ShortFile, PvsDataColumnNames.ErrorListShortFile},
            {PvsTableIndexes.Line, PvsDataColumnNames.ErrorListLine},
            {PvsTableIndexes.FalseAlarm, PvsDataColumnNames.ErrorListFalseAlarm},
            {PvsTableIndexes.File, PvsDataColumnNames.ErrorListFile},
            {PvsTableIndexes.CodePrev, PvsDataColumnNames.ErrorListCodePrev},
            {PvsTableIndexes.CodeCurrent, PvsDataColumnNames.ErrorListCodeCurrent},
            {PvsTableIndexes.CodeNext, PvsDataColumnNames.ErrorListCodeNext},
            {PvsTableIndexes.Trial, PvsDataColumnNames.ErrorListTrial},
            {PvsTableIndexes.Analyzer, PvsDataColumnNames.ErrorListAnalyzer},
            {PvsTableIndexes.LineExtension, PvsDataColumnNames.ErrorListLineExtension}
         };

      /// <summary>
      ///    Нахождение индекса в словаре по имени
      /// </summary>
      /// <param name="aDictionary">Словарь для поиска</param>
      /// <param name="column">Имя столбца</param>
      /// <returns>Найденный индекс или -1, если ничего не найдено</returns>
      public static int FindIndexByColumn(IEnumerable<KeyValuePair<int, string>> aDictionary, string column)
      {
         var index = -1;
         foreach (var indexNamePair in aDictionary.Where(indexNamePair => indexNamePair.Value == column))
         {
            index = indexNamePair.Key;
            break;
         }

         return index;
      }

      public static T[] ValuesByIndexes<T>(IEnumerable<T> enumerable, int[] indexes)
      {
         var result = new T[indexes.Length];
         var copyIndexes = new int[indexes.Length];
         Array.Copy(indexes, copyIndexes, indexes.Length);
         Array.Sort(copyIndexes);

         int i = 0, j = 0;
         foreach (var value in enumerable)
         {
            if (j == copyIndexes[i])
            {
               result[i++] = value;
               if (i == copyIndexes.Length) break;
            }
            ++j;
         }

         return result;
      }

      public static void SetNewPk(DataTable table, int[] indexes)
      {
         var key = ValuesByIndexes(table.Columns.Cast<DataColumn>(), indexes);
         table.PrimaryKey = key;
      }
   }
}