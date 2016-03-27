using System;
using System.Globalization;
using System.Resources;

namespace DatabaseResource.App.Lib
{
   public sealed class DatabaseResourceSet : ResourceSet
   {
      internal DatabaseResourceSet(string connectionString, CultureInfo culture)
         : base(new DatabaseResourceReader(connectionString, culture))
      {
      }

      public override Type GetDefaultReader()
      {
         return typeof(DatabaseResourceReader);
      }
   }
}