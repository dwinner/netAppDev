using System.AddIn.Contract;
using System.AddIn.Pipeline;
using System.IO;

namespace AddInSideAdapter
{
   [AddInAdapter]
   public class ImageProcessorViewToContractAdapter : ContractBase, Contract.IImageProcessorContract
   {
      private AddInView.ImageProcessorAddInView view;

      public ImageProcessorViewToContractAdapter(AddInView.ImageProcessorAddInView view)
      {
         this.view = view;
      }

      public INativeHandleContract GetVisual(Stream imageStream)
      {
         return FrameworkElementAdapters.ViewToContractAdapter(view.GetVisual(imageStream));
      }
   }
}

