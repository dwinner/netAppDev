using System.Collections;
using System.Collections.Generic;

namespace Observer
{
   public class Simulator : IEnumerable<string>
   {
      private readonly string[] _moves = {"5", "3", "1", "6", "7"};

      public IEnumerator<string> GetEnumerator()
         => ((IEnumerable<string>) _moves).GetEnumerator();

      IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
   }
}