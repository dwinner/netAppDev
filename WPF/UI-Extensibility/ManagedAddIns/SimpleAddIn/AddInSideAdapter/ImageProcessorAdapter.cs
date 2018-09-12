using System.AddIn.Pipeline;
using AddInView;
using Contract;

namespace AddInSideAdapter
{
   [AddInAdapter]
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