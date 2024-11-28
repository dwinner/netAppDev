using System.Diagnostics.Tracing;

namespace EtlDemo
{
   internal class SourceConfig
   {
      public string Name { get; set; }

      public EventLevel Level { get; set; }

      public EventKeywords Keywords { get; set; }
   }
}