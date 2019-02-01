using System.Threading.Tasks;
using NAct;

namespace NActTest
{
   /// <summary>
   /// Интерфейс, методы которого способны работать в контексте акторов
   /// </summary>
   /// <typeparam name="T">Параметр типа</typeparam>
   public interface IRndGenerator<T> : IActor
   {
      Task<T> GetNextNumber();
   }
}