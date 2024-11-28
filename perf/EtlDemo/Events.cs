using System.Diagnostics.Tracing;

namespace EtlDemo
{
   [EventSource(Name = "EtlDemo")]
   internal sealed class Events : EventSource
   {
      internal const int ProcessingStartId = 1;
      internal const int ProcessingFinishId = 2;
      internal const int FoundPrimeId = 3;
      internal const int NullStringId = 4;
      public static readonly Events _Log = new Events();

      [Event(ProcessingStartId, Level = EventLevel.Informational, Keywords = Keywords.General)]
      public void ProcessingStart()
      {
         if (IsEnabled())
         {
            WriteEvent(ProcessingStartId);
         }
      }

      [Event(ProcessingFinishId, Level = EventLevel.Informational, Keywords = Keywords.General)]
      public void ProcessingFinish()
      {
         if (IsEnabled())
         {
            WriteEvent(ProcessingFinishId);
         }
      }

      [Event(FoundPrimeId, Level = EventLevel.Informational, Keywords = Keywords.PrimeOutput)]
      public void FoundPrime(long primeNumber)
      {
         if (IsEnabled())
         {
            WriteEvent(FoundPrimeId, primeNumber);
         }
      }

      [Event(NullStringId, Level = EventLevel.Informational, Keywords = Keywords.General)]
      public void NullString(string id)
      {
         if (IsEnabled())
         {
            WriteEvent(NullStringId, (string)null);
         }
      }

      public static class Keywords
      {
         public const EventKeywords General = (EventKeywords)1;
         public const EventKeywords PrimeOutput = (EventKeywords)2;
      }
   }
}