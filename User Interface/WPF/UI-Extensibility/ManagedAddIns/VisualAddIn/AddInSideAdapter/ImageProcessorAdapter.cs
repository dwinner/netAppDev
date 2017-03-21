using System.AddIn.Contract;
using System.AddIn.Pipeline;
using System.IO;
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

      public INativeHandleContract GetVisual(Stream imageStream)
      {
         return FrameworkElementAdapters.ViewToContractAdapter(_view.GetVisual(imageStream));
      }
   }
}