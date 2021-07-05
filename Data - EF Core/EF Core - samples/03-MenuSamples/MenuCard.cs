using System.Collections.Generic;

namespace _03_MenuSamples
{
   public class MenuCard
   {
      public int MenuCardId { get; set; }

      public string Title { get; set; }

      public List<Menu> Menus { get; } = new();

      public override string ToString() => Title;
   }
}