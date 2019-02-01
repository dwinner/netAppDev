using System;
using System.Collections;

namespace YieldDemo
{
   /// <summary>
   /// Крестики-Нолики
   /// </summary>
   public class GameMoves
   {
      private readonly IEnumerator _crossEnumerator;
      private readonly IEnumerator _circleEnumerator;

      private int _move = 0;
      private const int MaxMoves = 9;

      public GameMoves()
      {
         _crossEnumerator = Cross();
         _circleEnumerator = Circle();
      }

      public IEnumerator Circle()
      {
         while (true)
         {
            Console.WriteLine("Cross, move {0}", _move);
            if (++_move >= MaxMoves)
            {
               yield break;               
            }
            yield return _crossEnumerator;
         }
      }

      public IEnumerator Cross()
      {
         while (true)
         {
            Console.WriteLine("Cross, move {0}", _move);
            if (++_move >= MaxMoves)
            {
               yield break;
            }
            yield return _circleEnumerator;
         }
      }
   }
}