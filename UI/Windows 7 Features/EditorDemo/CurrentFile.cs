using System.Diagnostics;
using System.IO;

namespace EditorDemo
{
   public class CurrentFile
   {
      public bool IsDirty { get; set; }
      public string Content { get; set; }

      public void Load(string aFilename)
      {
         App.Source.TraceEvent(TraceEventType.Verbose, 0, string.Format("Begin load {0}", aFilename));

         Content = File.ReadAllText(aFilename);
         IsDirty = false;

         App.Source.TraceEvent(TraceEventType.Verbose, 0, string.Format("End load {0}", aFilename));
      }

      public void Save(string aFilename)
      {
         App.Source.TraceEvent(TraceEventType.Verbose, 0, string.Format("Begin save {0}", aFilename));

         File.WriteAllText(aFilename, Content);
         IsDirty = true;

         App.Source.TraceEvent(TraceEventType.Verbose, 0, string.Format("End save {0}", aFilename));
      }
   }
}