using System.AddIn.Pipeline;
using Contract;
using HostView;

namespace HostSideAdapter
{
   public class HostObjectViewToContractHostAdapter : ContractBase, IHostObjectContract
   {
      private readonly HostObject _view;

      public HostObjectViewToContractHostAdapter(HostObject view)
      {
         _view = view;
      }

      public void ReportProgress(int progressPercent)
      {
         _view.ReportProgress(progressPercent);
      }
   }
}