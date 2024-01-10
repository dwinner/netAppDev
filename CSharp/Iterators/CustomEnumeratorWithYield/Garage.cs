using System.Collections;

namespace CustomEnumeratorWithYield
{
   // Garage contains a set of Car objects.
   public class Garage : IEnumerable
   {
      private readonly Car[] carArray = new Car[4];

      // Fill with some Car objects upon startup.
      public Garage()
      {
         carArray[0] = new Car("Rusty", 30);
         carArray[1] = new Car("Clunker", 55);
         carArray[2] = new Car("Zippy", 30);
         carArray[3] = new Car("Fred", 30);
      }

      #region Iterator methods

      public IEnumerator GetEnumerator()
      {
         foreach (var c in carArray)
         {
            yield return c;
         }
      }

      public IEnumerable GetTheCars(bool returnRevesed)
      {
         // Return the items in reverse.
         if (returnRevesed)
         {
            for (var i = carArray.Length; i != 0; i--)
            {
               yield return carArray[i - 1];
            }
         }
         else
         {
            // Return the items as placed in the array.
            foreach (var c in carArray)
            {
               yield return c;
            }
         }
      }

      #endregion
   }
}