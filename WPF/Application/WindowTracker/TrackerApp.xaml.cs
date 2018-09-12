using System.Collections.Generic;

namespace App.WindowTracker
{
   public partial class TrackerApp
   {
      public TrackerApp()
      {
         Documents = new List<Document>();
      }

      public List<Document> Documents { get; private set; }
   }
}