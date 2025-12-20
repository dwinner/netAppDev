namespace RefStructInterfaces;

internal ref struct Shape : IShape
{
   public void Draw()
   {
      Console.WriteLine("Draw some shape");
   }
}