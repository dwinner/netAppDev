namespace GatheringOverrides
{
   /// <summary>
   ///    Filter kind for base type hierarchy
   /// </summary>
   public enum TypeHierarchyFilter
   {
      /// <summary>
      ///    Include all types - base types and itself
      /// </summary>
      All,

      /// <summary>
      ///    Include all base types but itself
      /// </summary>
      ExcludeItselt,

      /// <summary>
      ///    Include only itself
      /// </summary>
      OnlyItself
   }
}