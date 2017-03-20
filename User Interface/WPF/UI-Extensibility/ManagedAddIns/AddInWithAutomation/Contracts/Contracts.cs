using System.AddIn.Contract;
using System.AddIn.Pipeline;

namespace Contract
{
   [AddInContract]
   public interface IImageProcessorContract : IContract
   {
      byte[] ProcessImageBytes(byte[] pixels);

      void Initialize(IHostObjectContract hostObj);
   }

   public interface IHostObjectContract : IContract
   {
      void ReportProgress(int progressPercent);
   }
}
