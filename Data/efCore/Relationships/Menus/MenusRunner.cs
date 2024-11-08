﻿using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class MenusRunner
{
   private readonly MenusContext _menusContext;

   public MenusRunner(MenusContext menusContext) => _menusContext = menusContext;

   public async Task CreateTheDatabaseAsync()
   {
      await _menusContext.Database.MigrateAsync();
   }

   public async Task DeleteTheDatabaseAsync()
   {
      await _menusContext.Database.EnsureDeletedAsync();
   }
}