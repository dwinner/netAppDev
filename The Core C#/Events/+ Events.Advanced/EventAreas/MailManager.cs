using System;

namespace EventAreas
{
   internal sealed class MailManager
   {
      public event EventHandler<NewMailEventArgs> NewMail;

      /// <exception cref="Exception">A delegate callback throws an exception.</exception>
      private void OnNewMail(NewMailEventArgs e)
      {
         e.Raise(this, ref NewMail);
      }

      /// <exception cref="Exception">A delegate callback throws an exception.</exception>
      public void SimulateNewMail(string from, string to, string subject)
      {
         var e = new NewMailEventArgs(from, to, subject);
         OnNewMail(e);
      }
   }
}