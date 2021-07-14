using System.Collections.Generic;

namespace _07_RelationUsingConventions
{
   public class User
   {
      public int UserId { get; set; }

      public string Name { get; set; }

      public List<Book> AuthoredBooks { get; set; }
   }
}