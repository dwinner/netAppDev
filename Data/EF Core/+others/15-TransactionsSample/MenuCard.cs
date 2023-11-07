using System.Collections.Generic;

namespace _15_TransactionsSample
{
   public class MenuCard
   {
      public int MenuCardId { get; set; }

      public string Title { get; set; }

      public List<Menu> Menus { get; } = new();

      public override string ToString() => Title;
   }
}