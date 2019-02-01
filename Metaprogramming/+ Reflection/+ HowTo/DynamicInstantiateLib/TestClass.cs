namespace DynamicInstantiateLib
{
   public class TestClass
   {
      public int Add(int a, int b)
      {
         return a + b;
      }

      public string CombineStrings<T>(T a, T b)
      {
         return string.Format("{0}, {1}", a, b);
      }
   }
}
