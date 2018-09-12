using System.AddIn.Contract;

namespace Contract
{
   public interface IHostObjectContract : IContract
   {
      void ReportProgress(int progressPercent);
   }
}