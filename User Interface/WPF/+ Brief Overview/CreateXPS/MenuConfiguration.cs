using System;
using System.Collections.Generic;

namespace Wrox.ProCSharp.Documents
{
   [Serializable]
   public class MenuConfiguration
   {
      public MenuConfiguration()
      {
         MenuConfig = new List<MenuConfigDay>();
      }

      public List<MenuConfigDay> MenuConfig { get; private set; }
   }
}