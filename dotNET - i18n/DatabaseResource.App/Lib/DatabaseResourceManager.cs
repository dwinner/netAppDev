using System.Collections.Generic;
using System.Globalization;
using System.Resources;

namespace DatabaseResource.App.Lib
{
   public sealed class DatabaseResourceManager : ResourceManager
   {
      private readonly string _connectionString;

      private readonly Dictionary<string, DatabaseResourceSet> _resourceSets =
         new Dictionary<string, DatabaseResourceSet>();

      public DatabaseResourceManager(string connectionString)
      {
         _connectionString = connectionString;
      }

      protected override ResourceSet InternalGetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
      {
         DatabaseResourceSet resourceSet;
         if (_resourceSets.ContainsKey(culture.Name))
         {
            resourceSet = _resourceSets[culture.Name];
         }
         else
         {
            resourceSet = new DatabaseResourceSet(_connectionString, culture);
            _resourceSets.Add(culture.Name, resourceSet);
         }

         return resourceSet;
      }
   }
}