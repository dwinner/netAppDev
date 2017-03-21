using ProgramVerificationSystems.SelfTester.Model.CommonTypes;
using ProgramVerificationSystems.SelfTester.Model.PVSStudio;
using System;
using System.Data;
using System.IO;
using System.Linq;

namespace ProgramVerificationSystems.PVSStudio
{
   public class PVSDiff
   {
      private const string MissingMark = " - MISSING IN CURRENT";
      private const string AdditionsMark = " - ADDITIONAL IN CURRENT";
      private const string EtalonMark = " - ETALON VERSION; ";
      private const string CurrentMark = " - CURRENT VERSION;";
      private const string FalseAlarmMark = " - FALSE ALARM INCONSISTENCY; ";
      private const string LevelMark = " - LEVEL INCONSISTENCY; OLD: ";
      private const string NewMark = "; NEW: ";

      public PVSDiff()
      {
         Output = null;
      }

      public ComparisonOutput Output { get; private set; }

      public DataSet GetDiff(string oldLogName, string newLogName)
      {
         Output = CompareLogs(oldLogName, newLogName);
         if (!Output.Succeeded)
         {
            DataTable errors = new DataTable(),
               solPath = new DataTable();
            var log = new DataSet();

            DataTableUtils.CreatePvsDataTable(errors, solPath, errors.DefaultView);

            MergeMessages(Output, errors);

            //FIXME: Compare old and new solPath's.
            //FIXME: Find cleaner way to get it.
            log.ReadXml(newLogName);
            int tblIdx = 1;

            log.Tables.RemoveAt(tblIdx);

            log.Tables.Add(errors);
            return log;
         }

         return null;
      }

      public static string CreateDummyEtalon(string corePlogFile)
      {
         var dummyEtalon = new DataSet();
         dummyEtalon.ReadXml(corePlogFile);
         var messageTable = dummyEtalon.Tables[DataTableNames.MessageTableName];
         var messages = messageTable.Rows.Cast<DataRow>().ToArray();
         Array.ForEach(messages, row => messageTable.Rows.Remove(row));
         var dummyEtalonLog = Path.GetTempFileName();
         dummyEtalon.WriteXml(dummyEtalonLog, XmlWriteMode.WriteSchema);

         return dummyEtalonLog;
      }

      private static void CreatePluginDataTable(DataTable errors, DataTable solPath)
      {
         DataTableUtils.CreatePvsDataTable(errors, solPath, errors.DefaultView);
      }

      private void Mark(DataRow error, string mark)
      {
         const int pluginTableIdx = TableIndexes.Message;
         error[pluginTableIdx] = error[pluginTableIdx] + mark;
      }

      private DataTable GetComparisionList(DataSet pvs1, DataSet pvs2, string mark)
      {
         DataTable result = new DataTable(),
            solPath = new DataTable();

         CreatePluginDataTable(result, solPath);
         GenerateComparisonDataTable(pvs1, pvs2, mark, result);

         return result;
      }

      private void GenerateComparisonDataTable(DataSet firstPluginLog, DataSet secondPluginLog, string mark,
         DataTable result)
      {
         foreach (DataRow pvsDataRow in firstPluginLog.Tables[1].Rows)
         {
            var pvsPkValues = new OrderedPluginMap()
               .RetrieveKeyValues(pvsDataRow.ItemArray);

            if (!secondPluginLog.Tables[1].Rows.Contains(pvsPkValues))
            {
               var newRow = result.NewRow();
               newRow.ItemArray = pvsDataRow.ItemArray;
               Mark(newRow, mark);
               try
               {
                  result.Rows.Add(newRow);
               }
               catch (ConstraintException)
               {
               }
            }
         }
      }

      private static void MergeMessages(ComparisonOutput comp,
         DataTable result)
      {
         result.Merge(comp.Missings);
         result.Merge(comp.Additionals);
         result.Merge(comp.Modifies);

         for (var i = 0; i < result.Rows.Count; ++i)
         {
            result.Rows[i][TableIndexes.Order] = i + 1;
         }
      }

      private ComparisonOutput CompareLogs(string oldlog, string newlog)
      {
         var result = new ComparisonOutput { Succeeded = true };

         DataSet oldTable = new DataSet(), newTable = new DataSet();
         oldTable.ReadXml(oldlog);
         newTable.ReadXml(newlog);

         const int tableIndex = 1;

         var newTableExtendedKeys = PluginDiffUtilities.PvsExtendedPrimaryKeysPvsMode;
         var oldTableExtendedKey = PluginDiffUtilities.PvsExtendedPrimaryKeysPvsMode;

         PluginDiffUtilities.SetNewPk(newTable.Tables[tableIndex], newTableExtendedKeys);
         PluginDiffUtilities.SetNewPk(oldTable.Tables[1], oldTableExtendedKey);

         var missings = GetComparisionList(oldTable, newTable, MissingMark);
         var additions = GetComparisionList(newTable, oldTable, AdditionsMark);
         var modifies = MergeDoubleMessagesInList(missings, additions);

         result.Modifies = modifies;
         result.Missings = missings;
         result.Additionals = additions;

         if (result.Additionals.Rows.Count > 0 || result.Missings.Rows.Count > 0 || result.Modifies.Rows.Count > 0)
            result.Succeeded = false;

         return result;
      }

      private static DataTable MergeDoubleMessagesInList(DataTable missings, DataTable additions)
      {
         DataTable modifies = new DataTable(),
            solPath = new DataTable();

         DataTableUtils.CreatePvsDataTable(modifies, solPath, modifies.DefaultView);

         for (var i = additions.Rows.Count - 1; i >= 0; i--)
         {
            //walking additions from the end
            for (var j = missings.Rows.Count - 1; j >= 0; j--)
            {
               //for each addition, walking missings from the end until the first match

               //For different Error Messages
               DataRow additionalRow = additions.Rows[i], missingRow = missings.Rows[j];
               if (
                  additionalRow[TableIndexes.ErrorCode].Equals(missingRow[TableIndexes.ErrorCode])
                  && additionalRow[TableIndexes.Line].Equals(missingRow[TableIndexes.Line])
                  &&
                  additionalRow[TableIndexes.File].ToString()
                     .Equals(missingRow[TableIndexes.File].ToString(), StringComparison.InvariantCultureIgnoreCase)
                  && additionalRow[TableIndexes.FalseAlarm].Equals(missingRow[TableIndexes.FalseAlarm])
                  && additionalRow[TableIndexes.Level].Equals(missingRow[TableIndexes.Level])
                  //&& Additions[i].ProjectName.Equals(Missings[j].ProjectName))                    
                  && !additionalRow[TableIndexes.Message].Equals(missingRow[TableIndexes.Message])
                  )
               {
                  missingRow[TableIndexes.Message] =
                     missingRow[TableIndexes.Message].ToString().Replace(MissingMark, EtalonMark)
                     +
                     additionalRow[TableIndexes.Message].ToString().Replace(AdditionsMark, CurrentMark);
                  modifies.ImportRow(missingRow);
                  additions.Rows.RemoveAt(i);
                  missings.Rows.RemoveAt(j);
                  break; //break from j loop on the first match and go for the next i loop item
               }

               //for different FalseAlarm Marks
               if (
                  additionalRow[TableIndexes.ErrorCode].Equals(missingRow[TableIndexes.ErrorCode])
                  && additionalRow[TableIndexes.Line].Equals(missingRow[TableIndexes.Line])
                  &&
                  additionalRow[TableIndexes.File].ToString()
                     .Equals(missingRow[TableIndexes.File].ToString(), StringComparison.InvariantCultureIgnoreCase)
                  && string.Equals(additionalRow[TableIndexes.Message].ToString().Replace(AdditionsMark, string.Empty),
                     missingRow[TableIndexes.Message].ToString().Replace(MissingMark, string.Empty))
                  && additionalRow[TableIndexes.Level].Equals(missingRow[TableIndexes.Level])
                  //&& Additions[i].ProjectName.Equals(Missings[j].ProjectName)
                  && !additionalRow[TableIndexes.FalseAlarm].Equals(missingRow[TableIndexes.FalseAlarm])
                  )
               {
                  missingRow[TableIndexes.Message] =
                     missingRow[TableIndexes.Message].ToString().Replace(MissingMark, FalseAlarmMark);
                  modifies.ImportRow(missingRow);
                  additions.Rows.RemoveAt(i);
                  missings.Rows.RemoveAt(j);
                  break; //break from j loop on the first match and go for the next i loop item
               }

               //for different levels
               if (
                  additionalRow[TableIndexes.ErrorCode].Equals(missingRow[TableIndexes.ErrorCode])
                  && additionalRow[TableIndexes.Line].Equals(missingRow[TableIndexes.Line])
                  &&
                  additionalRow[TableIndexes.File].ToString()
                     .Equals(missingRow[TableIndexes.File].ToString(), StringComparison.InvariantCultureIgnoreCase)
                  && string.Equals(additionalRow[TableIndexes.Message].ToString().Replace(AdditionsMark, string.Empty),
                     missingRow[TableIndexes.Message].ToString().Replace(MissingMark, string.Empty))
                  && additionalRow[TableIndexes.FalseAlarm].Equals(missingRow[TableIndexes.FalseAlarm])
                  //&& Additions[i].ProjectName.Equals(Missings[j].ProjectName)
                  && !additionalRow[TableIndexes.Level].Equals(missingRow[TableIndexes.Level])
                  )
               {
                  missingRow[TableIndexes.Message] =
                     missingRow[TableIndexes.Message].ToString()
                        .Replace(MissingMark, LevelMark + missingRow[TableIndexes.Level] +
                                              NewMark + additionalRow[TableIndexes.Level]);
                  modifies.ImportRow(missingRow);
                  additions.Rows.RemoveAt(i);
                  missings.Rows.RemoveAt(j);
                  break; //break from j loop on the first match and go for the next i loop item
               }
            }
         }

         return modifies;
      }

      public class ComparisonOutput
      {
         public DataTable Additionals;
         public DataTable Missings;
         public DataTable Modifies;
         public bool Succeeded;
      }
   }
}