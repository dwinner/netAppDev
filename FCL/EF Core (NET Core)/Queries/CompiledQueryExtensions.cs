using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

internal static class CompiledQueryExtensions
{
   private static Func<MenusContext, string, IEnumerable<MenuItem>>? _menuItemsByText;

   private static Func<MenusContext, string, IAsyncEnumerable<MenuItem>>? _menuItemsByTextAsync;

   private static Func<MenusContext, string, IEnumerable<MenuItem>> CompileMenusByTextQuery()
      => EF.CompileQuery((MenusContext context, string text)
         => context.MenuItems.Where(m => m.Text == text));

   private static Func<MenusContext, string, IAsyncEnumerable<MenuItem>> CompileMenuItemsByTextAsyncQuery()
      => EF.CompileAsyncQuery((MenusContext context, string text)
         => context.MenuItems.Where(m => m.Text == text));

   public static IEnumerable<MenuItem> MenuItemsByText(this MenusContext menusContext, string text)
   {
      _menuItemsByText ??= CompileMenusByTextQuery();
      return _menuItemsByText(menusContext, text);
   }

   public static IAsyncEnumerable<MenuItem> MenuItemsByTextAsync(this MenusContext menusContext, string text)
   {
      _menuItemsByTextAsync ??= CompileMenuItemsByTextAsyncQuery();
      return _menuItemsByTextAsync(menusContext, text);
   }
}