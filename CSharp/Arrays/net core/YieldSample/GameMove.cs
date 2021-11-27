using System;
using System.Collections;
using System.Collections.Generic;

public class GameMoves
{
   private const int MaxMoves = 9;
   private readonly IEnumerator _circle;
   private readonly IEnumerator _cross;
   private int _move;

   public GameMoves()
   {
      _cross = Cross();
      _circle = Circle();
   }

   public IEnumerator<IEnumerator> Cross()
   {
      while (true)
      {
         Console.WriteLine($"Cross, move {_move}");
         if (++_move >= MaxMoves)
         {
            yield break;
         }

         yield return _circle;
      }
   }

   public IEnumerator<IEnumerator> Circle()
   {
      while (true)
      {
         Console.WriteLine($"Circle, move {_move}");
         if (++_move >= MaxMoves)
         {
            yield break;
         }

         yield return _cross;
      }
   }
}