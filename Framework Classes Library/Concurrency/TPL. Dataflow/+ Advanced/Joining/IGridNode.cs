using System.ServiceModel;
using System.Threading.Tasks;

namespace Joining
{
   [ServiceContract]
   public interface IGridNode<in T>
   {
      [OperationContract]
      Task InvokeAsync(T workItem);
   }
}