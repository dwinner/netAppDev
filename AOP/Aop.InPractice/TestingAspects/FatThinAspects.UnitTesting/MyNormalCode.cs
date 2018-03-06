using System.Linq;

namespace FatThinAspects.UnitTesting
{
   public class MyNormalCode
   {
      [MyThinAspect]
      public string Reverse(string str)
      {
         return new string(str.Reverse().ToArray());
      }
   }
}