using System.Collections.Generic;

namespace _05_ScaffoldSample
{
   public class MenuCards
   {
      public MenuCards() => Menus = new HashSet<Menus>();

      public int MenuCardId { get; set; }
      public string Title { get; set; }

      public ICollection<Menus> Menus { get; set; }
   }
}