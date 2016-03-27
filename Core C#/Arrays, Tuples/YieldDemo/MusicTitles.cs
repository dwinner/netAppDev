using System.Collections.Generic;

namespace YieldDemo
{
   public class MusicTitles
   {
      private readonly string[] _names = { "Tubular Bells", "Hergest Ridge", "Ommadawn", "Platinum" };

      /// <summary>
      /// Стандартный итератор
      /// </summary>
      /// <returns>Стандартный итератор</returns>
      public IEnumerator<string> GetEnumerator()
      {
         // for (int i = 0; i < _names.Length; i++) yield return _names[i];         
         return ((IEnumerable<string>) _names).GetEnumerator();
      }

      /// <summary>
      /// Обратный итератор
      /// </summary>
      /// <returns>Обратный итератор</returns>
      public IEnumerable<string> Reverse()
      {
         for (int i = _names.Length - 1; i >= 0; i--)
         {
            yield return _names[i];
         }
      }

      /// <summary>
      /// Итератор для подмножества элементов
      /// </summary>
      /// <param name="index">Начальный индекс</param>
      /// <param name="length">Смещение</param>
      /// <returns>Итератор для подмножества элементов</returns>
      public IEnumerable<string> Subset(int index, int length)
      {
         for (int i = index; i < index + length; i++)
         {
            yield return _names[i];
         }
      }
   }
}