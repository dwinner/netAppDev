using System.AddIn.Pipeline;
using System.IO;
using System.Windows;

namespace HostSideAdapter
{
   [HostAdapter]
   public class ImageProcessorContractToViewHostAdapter : HostView.ImageProcessorHostView
   {
      private Contract.IImageProcessorContract contract;
      private ContractHandle contractHandle;

      public ImageProcessorContractToViewHostAdapter(Contract.IImageProcessorContract contract)
      {
         this.contract = contract;
         contractHandle = new ContractHandle(contract);
      }

      public override FrameworkElement GetVisual(Stream imageStream)
      {
         return FrameworkElementAdapters.ContractToViewAdapter(contract.GetVisual(imageStream));
      }
   }
}
