using System.AddIn.Pipeline;
using Contract;
using HostView;

namespace HostSideAdapter
{
   [HostAdapter]
   public class ImageProcessorContractToViewHostAdapter : ImageProcessorHostView
   {
      private readonly IImageProcessorContract _contract;
      private ContractHandle _contractHandle;

      public ImageProcessorContractToViewHostAdapter(IImageProcessorContract contract)
      {
         _contract = contract;
         _contractHandle = new ContractHandle(contract);
      }

      public override byte[] ProcessImageBytes(byte[] pixels)
      {
         return _contract.ProcessImageBytes(pixels);
      }
   }
}