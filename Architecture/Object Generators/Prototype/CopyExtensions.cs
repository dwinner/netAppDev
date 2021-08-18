namespace Prototype
{
   public static class CopyExtensions
   {
      public static object DeepCopy(this object @this)
         => UniversalCopyUtility<object>.DeepCopy(@this);
   }
}