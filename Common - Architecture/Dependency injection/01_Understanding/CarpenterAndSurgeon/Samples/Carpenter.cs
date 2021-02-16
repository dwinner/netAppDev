namespace Samples
{
   internal class Carpenter
   {
      private readonly Saw _saw = new Saw();

      private void MakeChair() => _saw.Cut();
   }
}