using System;

namespace AccessingSolutionObjects
{
   public static class GuidList
   {
      /// <summary>
      ///    AccessingSlnObjectsPackage GUID string.
      /// </summary>
      public const string PackageGuidString = "205d0799-3825-4ca6-ab35-89f784db4b94";

      public const string TopMenuGroup = "329c05e3-bde7-4219-83c9-7de79ad9fc8b";

      public static readonly Guid TopMenuGroupGuid = Guid.Parse(TopMenuGroup);
   }
}