using System;

namespace RoadmapAnalyzer.Tests.Api
{
   [AttributeUsage(AttributeTargets.Method, Inherited = false)]
   public sealed class CaseAttribute : Attribute
   {
      public CaseAttribute(string description)
      {
         Description = description;
      }

      public string Description { get; }
   }
}