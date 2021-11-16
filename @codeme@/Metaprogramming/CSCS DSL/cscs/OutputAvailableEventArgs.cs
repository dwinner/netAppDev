using System;

namespace CsCsLang
{
   public class OutputAvailableEventArgs : EventArgs
   {
      public OutputAvailableEventArgs(string output) => Output = output;

      public string Output { get; set; }
   }
}