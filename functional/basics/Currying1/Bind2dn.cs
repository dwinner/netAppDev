namespace _09_Carrying
{
   public class Bind2dn
   {
      public delegate int BoundDelegate(int x);

      private readonly int _arg2;
      private readonly Operation _del;

      public Bind2dn(Operation del, int arg2)
      {
         _del = del;
         _arg2 = arg2;
      }

      public BoundDelegate Binder => arg1 => _del(arg1, _arg2);
   }
}