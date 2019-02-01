using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;

namespace _08_ConstrainedExecutionRegions
{
   public static class ListExtensions
   {
      public static List<T> Clone<T>(this List<T> listToClone)
      {
         T[] cloneArray = new T[listToClone.Count];
         listToClone.CopyTo(cloneArray);
         return new List<T>(cloneArray);
      }
   }

   public static class Swapper
   {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
      public static void SwapList<T>(ref T first, ref T second, ref T temp)
         where T : class
      {
         temp = first;
         first = second;
         second = temp;
         temp = null;
      }
   }
}
