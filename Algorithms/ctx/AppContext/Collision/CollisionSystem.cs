using Sorting.Algs.PrioQ;

namespace AppContext.Collision;

/// <summary>
///    The <see cref="CollisionSystem" /> class represents a collection of particles
///    moving in the unit box, according to the laws of elastic collision.
///    This event-based simulation relies on a priority queue.
/// </summary>
public sealed class CollisionSystem
{
   private const double Hz = 0.5; // number of redraw events per clock tick
   private readonly Particle[] _particles; // the array of particles
   private double _clockTime; // simulation clock time
   private MinPrioQueue<Event> _priorityQueue = null!; // the priority queue

   public CollisionSystem(Particle[] particles) => _particles = particles;

   // updates priority queue with all new events for particle a
   private void Predict(Particle? particle, double limit)
   {
      if (particle == null)
      {
         return;
      }

      // particle-particle collisions
      foreach (var current in _particles)
      {
         var timeToHit = particle.GetTimeToHit(current);
         if (_clockTime + timeToHit <= limit)
         {
            _priorityQueue.Insert(new Event(_clockTime + timeToHit, particle, current));
         }
      }

      // particle-wall collisions
      var deltaX = particle.GetTimeToHitVerticalWall();
      var deltaY = particle.GetTimeToHitHorizontalWall();
      if (_clockTime + deltaX <= limit)
      {
         _priorityQueue.Insert(new Event(_clockTime + deltaX, particle, null));
      }

      if (_clockTime + deltaY <= limit)
      {
         _priorityQueue.Insert(new Event(_clockTime + deltaY, null, particle));
      }
   }

   public event EventHandler<ParticleChangedEventArgs> ParticleChangedEvent = null!;

   // redraw all particles
   private void Redraw(double limit)
   {
      foreach (var particle in _particles)
      {
         var args = new ParticleChangedEventArgs(particle);
         OnParticleChangedEvent(args);
      }

      if (_clockTime < limit)
      {
         _priorityQueue.Insert(new Event(_clockTime + 1.0 / Hz, null, null));
      }
   }

   /// <summary>
   ///    Simulates the system of particles for the specified amount of time.
   /// </summary>
   /// <param name="limit">The amount of time</param>
   public void Simulate(double limit)
   {
      // initialize PQ with collision events and redraw event
      _priorityQueue = new MinPrioQueue<Event>();
      Array.ForEach(_particles, particle => Predict(particle, limit));
      _priorityQueue.Insert(new Event(0, null, null)); // redraw event

      // the main event-driven simulation loop
      while (!_priorityQueue.IsEmpty)
      {
         // get impending event, discard if invalidated
         var @event = _priorityQueue.DelMin();
         if (!@event.IsValid)
         {
            continue;
         }

         var particle1 = @event.Particle1;
         var particle2 = @event.Particle2;

         // physical collision, so update positions, and then simulation clock
         Array.ForEach(_particles, particle => particle.Move(@event.Time - _clockTime));
         _clockTime = @event.Time;

         // process event
         ProcessEvent(particle1, particle2, limit);

         // update the priority queue with new collisions involving a or b
         Predict(particle1, limit);
         Predict(particle2, limit);
      }
   }

   private void ProcessEvent(Particle? particle1, Particle? particle2, double limit)
   {
      if (particle1 != null && particle2 != null)
      {
         // particle-particle collision
         particle1.BounceOff(particle2);
      }
      else if (particle1 != null && particle2 == null)
      {
         // particle-wall collision
         particle1.BounceOffVerticalWall();
      }
      else
      {
         switch (particle1)
         {
            case null when particle2 != null:
               // particle-wall collision
               particle2.BounceOffHorizontalWall();
               break;
            case null when particle2 == null:
               Redraw(limit); // redraw event
               break;
         }
      }
   }

   private void OnParticleChangedEvent(ParticleChangedEventArgs e) => ParticleChangedEvent(this, e);

   /// <summary>
   ///    An event during a particle collision simulation. Each event contains
   ///    the time at which it will occur (assuming no supervening actions)
   ///    and the particles a and b involved.
   ///    <remarks>
   ///       - a and b both null: redraw event
   ///       - a null, b not null: collision with vertical wall
   ///       - a not null, b null: collision with horizontal wall
   ///       - a and b both not null: binary collision between a and b
   ///    </remarks>
   /// </summary>
   private sealed class Event : IComparable<Event>
   {
      private const int DefaultCollisionCount = -1;
      private readonly int _particle1CollisionCount; // collision counts at event creation
      private readonly int _particle2CollisionCount; // collision counts at event creation

      // create a new event to occur at time t involving a and b
      public Event(double t, Particle? particle1, Particle? particle2)
      {
         Time = t;
         Particle1 = particle1;
         Particle2 = particle2;
         _particle1CollisionCount = particle1?.CollisionCount
                                    ?? DefaultCollisionCount;
         _particle2CollisionCount = particle2?.CollisionCount
                                    ?? DefaultCollisionCount;
      }

      public double Time { get; }

      public Particle? Particle1 { get; }

      public Particle? Particle2 { get; }

      /// <summary>
      ///    Has any collision occurred between when event was created and now?
      /// </summary>
      public bool IsValid =>
         (Particle1 == null || Particle1.CollisionCount == _particle1CollisionCount) &&
         (Particle2 == null || Particle2.CollisionCount == _particle2CollisionCount);

      public int CompareTo(Event? other) => Time.CompareTo(other!.Time);
   }
}