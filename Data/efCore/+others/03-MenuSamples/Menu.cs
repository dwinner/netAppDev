﻿namespace _03_MenuSamples
{
   public class Menu
   {
      public int MenuId { get; set; }

      public string Text { get; set; }

      public decimal Price { get; set; }

      public int MenuCardId { get; set; }

      public MenuCard MenuCard { get; set; }

      public override string ToString() => Text;
   }
}