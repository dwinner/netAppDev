namespace Samples
{
   internal class Surgeon
   {
      private readonly Forceps _forceps;

      public Surgeon(Forceps forceps) => _forceps = forceps;

      public void Operate() => _forceps.Grab();
   }
}