using ReflectionInterface;

namespace ReflectionExtension
{
   public class AddExtension : IExtension
   {
      public int Execute(int arg1, int arg2) => arg1 + arg2;
   }
}