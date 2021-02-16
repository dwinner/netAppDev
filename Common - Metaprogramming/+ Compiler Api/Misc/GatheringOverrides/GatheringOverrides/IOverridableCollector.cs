using System.Collections.Generic;

namespace GatheringOverrides
{
   /// <summary>
   ///    Gathering interface to collect members being accessable to override
   /// </summary>
   public interface IOverridableCollector
   {
      /// <summary>
      ///    Gather properties to be overriden
      /// </summary>
      /// <param name="filter">Collection filter for type hierarchy</param>
      /// <param name="indentation">Indentation string for formatting by hand</param>
      /// <returns>Properties to be overriden</returns>
      IEnumerable<MemberEntry> GatherProperties(TypeHierarchyFilter filter, string indentation);

      /// <summary>
      ///    Gather methods to be overriden
      /// </summary>
      /// <param name="filter">Collection filter for type hierarchy</param>
      /// <param name="indentation">Indentation string for formatting by hand</param>
      /// <returns>Methods to be overriden</returns>
      IEnumerable<MemberEntry> GatherMethods(TypeHierarchyFilter filter, string indentation);
   }
}