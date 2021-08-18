using System.AddIn.Pipeline;
using System.IO;
using System.Windows;
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

      public override FrameworkElement GetVisual(Stream imageStream)
      {
         return FrameworkElementAdapters.ContractToViewAdapter(_contract.GetVisual(imageStream));
      }
   }
}