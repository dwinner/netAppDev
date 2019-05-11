namespace Samples.ServiceLocator
{
   internal class Carpenter
   {
      private readonly Saw _saw = Manual.Locate<Saw>();

      private void MakeChair()
      {
         _saw.Cut();
      }
   }
}