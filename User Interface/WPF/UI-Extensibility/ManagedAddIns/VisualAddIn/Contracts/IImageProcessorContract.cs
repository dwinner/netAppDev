using System.AddIn.Contract;
using System.AddIn.Pipeline;
using System.IO;

namespace Contract
{
   [AddInContract]
   public interface IImageProcessorContract : IContract
   {
      INativeHandleContract GetVisual(Stream imageStream);
   }
}
