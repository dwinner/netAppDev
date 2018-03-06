using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace SqlSamplesUsingAdventureWorks
{
   /// <summary>
   ///    UDT-אדדנודאע
   /// </summary>
   [Serializable]
   [SqlUserDefinedAggregate(Format.Native)]
   public struct SampleSum
   {
      private int _sum;

      public void Init()
      {
         _sum = 0;
      }

      public void Accumulate(SqlInt32 value)
      {
         _sum += value.Value;
      }

      public void Merge(SampleSum sampleSumGroup)
      {
         _sum += sampleSumGroup._sum;
      }

      public SqlInt32 Terminate()
      {
         return new SqlInt32(_sum);
      }
   }
}