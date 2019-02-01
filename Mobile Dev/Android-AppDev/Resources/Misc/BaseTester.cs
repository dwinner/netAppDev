using Android.Content;
using Resources.Interfaces;

namespace Resources.Misc
{
   public class BaseTester
   {
      public BaseTester(IReportBack reportTo, Context context)
      {
         ReportTo = reportTo;
         Context = context;
      }

      public IReportBack ReportTo { get; set; }

      public Context Context { get; set; }
   }
}