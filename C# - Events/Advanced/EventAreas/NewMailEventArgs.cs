using System;

namespace EventAreas
{
   internal class NewMailEventArgs : EventArgs
   {
      private readonly string _from;
      private readonly string _subject;
      private readonly string _to;

      public NewMailEventArgs(string @from, string to, string subject)
      {
         _from = @from;
         _to = to;
         _subject = subject;
      }

      public string From
      {
         get { return _from; }
      }

      public string To
      {
         get { return _to; }
      }

      public string Subject
      {
         get { return _subject; }
      }

      public override string ToString()
      {
         return string.Format("From: {0}, To: {1}, Subject: {2}", From, To, Subject);
      }
   }
}