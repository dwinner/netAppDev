using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace ifElseSwitchLinking
{
   public sealed class CsvImporter // NOTE: ¬ариант с if-else дл€ потоков данных
   {
      private readonly TransformManyBlock<string, object[]> _databaseQueryBlock;

      public CsvImporter()
      {
         _databaseQueryBlock =
            new TransformManyBlock<string, object[]>((Func<string, IEnumerable<object[]>>) ExecuteQuery);

         var rowTGoLedgerBlock =
            new TransformManyBlock<object[], ILedgerEntry>(
               (Func<object[], IEnumerable<ILedgerEntry>>) MapDatabaseRowToObject);

         var debitBlock = new ActionBlock<ILedgerEntry>((Action<ILedgerEntry>) WriteDebitEntry);

         var creditBlock = new ActionBlock<ILedgerEntry>((Action<ILedgerEntry>) WriteCreditEntry);

         var unknownLedgerEntryBlock = new ActionBlock<ILedgerEntry>((Action<ILedgerEntry>) LogUnknownLedgerEntryType);

         _databaseQueryBlock.LinkTo(rowTGoLedgerBlock);

         #region If-Else при св€зывании блоков

         rowTGoLedgerBlock.LinkTo(debitBlock, le => le.IsDebit);
         rowTGoLedgerBlock.LinkTo(creditBlock, le => le.IsCredit);
         rowTGoLedgerBlock.LinkTo(unknownLedgerEntryBlock);

         #endregion

      }

      private void LogUnknownLedgerEntryType(ILedgerEntry obj)
      {
      }

      public void Export(string connectionString)
      {
         _databaseQueryBlock.Post(connectionString);
      }

      private IEnumerable<object[]> ExecuteQuery(string arg)
      {
         yield break;
      }

      private IEnumerable<ILedgerEntry> MapDatabaseRowToObject(object[] arg)
      {
         yield break;
      }

      private void WriteDebitEntry(ILedgerEntry debitEntry)
      {
      }

      private void WriteCreditEntry(ILedgerEntry creditEntry)
      {
      }
   }
}