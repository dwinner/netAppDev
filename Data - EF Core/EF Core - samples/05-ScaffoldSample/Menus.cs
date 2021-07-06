namespace _05_ScaffoldSample
{
   public class Menus
   {
      public int MenuId { get; set; }
      public int MenuCardId { get; set; }
      public decimal Price { get; set; }
      public string Text { get; set; }

      public MenuCards MenuCard { get; set; }
   }
}