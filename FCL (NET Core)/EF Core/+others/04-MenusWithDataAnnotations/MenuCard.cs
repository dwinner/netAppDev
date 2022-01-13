using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _04_MenusWithDataAnnotations
{
   [Table("MenuCards", Schema = "mc")]
   public class MenuCard
   {
      public int MenuCardId { get; set; }

      [MaxLength(120)]
      public string Title { get; set; }

      public List<Menu> Menus { get; } = new();

      public override string ToString() => Title;
   }
}