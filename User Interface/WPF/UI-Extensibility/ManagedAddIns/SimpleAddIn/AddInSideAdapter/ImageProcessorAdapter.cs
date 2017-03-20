using System.AddIn.Pipeline;
using AddInView;
using Contract;
using JetBrains.Annotations;

namespace AddInSideAdapter
{
   [AddInAdapter]
   [UsedImplicitly]
   public class ImageProcessorViewToContractAdapter : ContractBase, IImageProcessorContract
   {
      private readonly ImageProcessorAddInView _view;

      public ImageProcessorViewToContractAdapter(ImageProcessorAddInView view)
      {
         _view = view;
      }

      public byte[] ProcessImageBytes(byte[] pixels)
      {
         return _view.ProcessImageBytes(pixels);
      }
   }
}