namespace AppContext.Collision;

public class ParticleChangedEventArgs : EventArgs
{
   public ParticleChangedEventArgs(Particle particle) => Particle = particle;

   public Particle Particle { get; private set; }
}