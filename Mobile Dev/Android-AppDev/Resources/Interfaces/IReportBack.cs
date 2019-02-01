namespace Resources.Interfaces
{
   /// <summary>
   ///    An interface implemented typically bu an activity
   ///    so that a worker class can report back on what happened
   /// </summary>
   public interface IReportBack
   {
      /// <summary>
      ///    Report back
      /// </summary>
      /// <param name="aTag">Tag</param>
      /// <param name="aMessage">Message</param>
      void ReportBack(string aTag, string aMessage);
   }
}