using System.Text;
using MoreLinq.Extensions;

var stringProps = typeof(string).GetProperties();
var builderProps = typeof(StringBuilder).GetProperties();

var query =
   from s in stringProps
   join b in builderProps
      on new
      {
         s.Name,
         s.PropertyType
      } equals new
      {
         b.Name,
         b.PropertyType
      }
   select new
   {
      s.Name,
      s.PropertyType,
      StringToken = s.MetadataToken,
      StringBuilderToken = b.MetadataToken
   };

query.ForEach(Console.WriteLine);