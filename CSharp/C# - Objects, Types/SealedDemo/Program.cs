namespace SealedDemo
{
   internal class ParentClass
   {
      public virtual void MyFunc()
      {
      }
   }

   internal class ChildClass : ParentClass
   {
      public sealed override void MyFunc()
      {
      }
   }

   internal class GrandChildClass : ChildClass
   {
      /*yields compile error
      public override void MyFunc()
      {
      }*/
   }

   internal class Program
   {
      private static void Main(string[] args)
      {
      }
   }
}