using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace _15_ContextualPartitioner
{
   public class ContextPartitioner : Partitioner<WorkItem>
   {
      private readonly WorkItem[] _dataItems; // ����� ������ ��� ���������
      private readonly int _targetSum; // ����� �������� ��� ������ ���������
      private readonly EnumerableSource _enumSource; // ������ ��� �������� ��������������
      private readonly object _lockObj = new object(); // ��� �������������� ����� � ��������
      private const long SharedStartIndex = 0; // ������ "�� ��������" �������

      public ContextPartitioner(WorkItem[] data, int target)
      {
         _dataItems = data;
         _targetSum = target;
         _enumSource = new EnumerableSource(this);
      }

      // NOTE: ������������ ��������� ������ ���� �������� ��� ������������ ������ Foreach
      public override bool SupportsDynamicPartitions { get { return true; } }

      public override IEnumerable<WorkItem> GetDynamicPartitions()
      {
         return _enumSource;
      }

      public override IList<IEnumerator<WorkItem>> GetPartitions(int partitionCount)
      {
         IList<IEnumerator<WorkItem>> partitionsList = new List<IEnumerator<WorkItem>>();
         var enumObj = GetDynamicPartitions(); // �������� �������������, ������� ���������� ��������� �����������
         // ������� ����������� ���-�� ���������
         for (var i = 0; i < partitionCount; i++)
         {
            // ReSharper disable PossibleMultipleEnumeration
            partitionsList.Add(enumObj.GetEnumerator());
            // ReSharper restore PossibleMultipleEnumeration
         }

         return partitionsList;
      }

      private Tuple<long, long> GetNextChunk()
      {
         Tuple<long, long> result;
         lock (_lockObj)
         {
            if (SharedStartIndex < _dataItems.Length) // ��������, ���� ��� �������� ��� ���������
            {
               var sum = 0;
               var endIndex = SharedStartIndex;
               while (endIndex < _dataItems.Length && sum < _targetSum)
               {
                  sum += _dataItems[endIndex++].WorkDuration;
               }

               result = Tuple.Create(SharedStartIndex, endIndex);
            }
            else // ������ ��� ��������� ������ �� ��������
            {
               result = Tuple.Create(-1L, -1L);
            }
         }

         return result;
      }

      private class EnumerableSource : IEnumerable<WorkItem>
      {
         private readonly ContextPartitioner _parentPartitioner;

         public EnumerableSource(ContextPartitioner parentPartitioner)
         {
            _parentPartitioner = parentPartitioner;
         }

         public IEnumerator<WorkItem> GetEnumerator()
         {
            return new ChunkEnumerator(_parentPartitioner).GetEnumerator();
         }

         IEnumerator IEnumerable.GetEnumerator()
         {
            return GetEnumerator();
         }
      }

      private class ChunkEnumerator
      {
         private readonly ContextPartitioner _parentPartitioner;

         public ChunkEnumerator(ContextPartitioner parentPartitioner)
         {
            _parentPartitioner = parentPartitioner;
         }

         public IEnumerator<WorkItem> GetEnumerator()
         {
            while (true)
            {
               var nextChunk = _parentPartitioner.GetNextChunk(); // �������� ������� ��� ���������� ���������
               if (nextChunk.Item1 == -1 && nextChunk.Item2 == -1)
               {
                  yield break;
               }

               for (var i = nextChunk.Item1; i < nextChunk.Item2; i++)
               {
                  yield return _parentPartitioner._dataItems[i];
               }
            }
         }
      }
   }
}