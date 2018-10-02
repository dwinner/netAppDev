/**
 * Модель программирования "Сначала Код"
 */

using System;

namespace CodeFirst
{
   internal class EntryPoint
   {
      private static void Main()
      {
         //CreateObjects();
         QueryData();
      }

      private static void QueryData()
      {
         using (var context = new MenuContext())
         {
            context.Configuration.LazyLoadingEnabled = false;
            foreach (MenuCard card in context.MenuCards.Include("Menus"))
            {
               Console.WriteLine("{0}", card.Text);
               foreach (Menu menu in card.Menus)
               {
                  Console.WriteLine("\t{0} {1:d}", menu.Text, menu.Day);
               }
            }
         }
      }

      private static void CreateObjects()
      {
         using (var menuContext = new MenuContext())
         {
            MenuCard card = menuContext.MenuCards.Create();
            card.Text = "Soups";
            menuContext.MenuCards.Add(card);

            Menu firstMenu = menuContext.Menus.Create();
            firstMenu.Text = "Baked Potato Soup";
            firstMenu.Price = 4.80M;
            firstMenu.Day = new DateTime(2012, 9, 20);
            firstMenu.MenuCard = card;

            menuContext.Menus.Add(firstMenu);

            Menu secondMenu = menuContext.Menus.Create();
            secondMenu.Text = "Cheddar Broccoli Soup";
            secondMenu.Price = 4.50M;
            secondMenu.Day = new DateTime(2012, 9, 21);
            secondMenu.MenuCard = card;

            menuContext.Menus.Add(secondMenu);

            try
            {
               menuContext.SaveChanges();
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.Message);
            }
         }
      }
   }
}