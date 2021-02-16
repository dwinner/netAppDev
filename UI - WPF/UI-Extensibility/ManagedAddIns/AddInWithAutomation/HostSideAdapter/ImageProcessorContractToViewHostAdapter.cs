using System.AddIn.Pipeline;
using Contract;
using HostView;
using JetBrains.Annotations;

namespace HostSideAdapter
{
   [HostAdapter]
   [UsedImplicitly]
   public class ImageProcessorContractToViewHostAdapter : ImageProcessorHostView
   {
      private readonly IImageProcessorContract _contract;
      [UsedImplicitly] private ContractHandle _contractHandle;

      public ImageProcessorContractToViewHostAdapter(IImageProcessorContract contract)
      {
         _contract = contract;
         _contractHandle = new ContractHandle(contract);
      }

      public override byte[] ProcessImageBytes(byte[] pixels)
      {
         return _contract.ProcessImageBytes(pixels);
      }

      public override void Initialize(HostObject host)
      {
         var hostAdapter = new HostObjectViewToContractHostAdapter(host);
         _contract.Initialize(hostAdapter);
      }
   }
}