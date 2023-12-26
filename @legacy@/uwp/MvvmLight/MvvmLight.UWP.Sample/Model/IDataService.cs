using System.Threading.Tasks;

namespace MvvmLight.UWP.Sample.Model
{
   public interface IDataService
   {
      Task<DataItem> GetData();
   }
}