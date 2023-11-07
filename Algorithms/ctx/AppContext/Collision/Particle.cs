namespace AppContext.Collision;

/// <summary>
///    The <see cref="Particle" /> class represents a particle moving in the unit box,
///    with a given position, velocity, radius, and mass. Methods are provided
///    for moving the particle and for predicting and resolvling elastic
///    collisions with vertical walls, horizontal walls, and other particles.
///    This data type is mutable because the position and velocity change.
/// </summary>
public sealed class Particle
{
   private const double Inf = double.PositiveInfinity;
   private readonly double _mass; // mass
   private readonly double _radius; // radius
   private double _xPos;
   private double _xVelocity;
   private double _yPos;
   private double _yVelocity;

   /// <summary>
   ///    Initializes a particle with the specified position, velocity, radius, mass.
   /// </summary>
   /// <param name="xPos"><em>x</em>-coordinate of position</param>
   /// <param name="yPos"><em>y</em>-coordinate of position</param>
   /// <param name="xVelocity"><em>x</em>-coordinate of velocity</param>
   /// <param name="yVelocity">vy <em>y</em>-coordinate of velocity</param>
   /// <param name="radius">the radius</param>
   /// <param name="mass">the mass</param>
   public Particle(double xPos, double yPos, double xVelocity, double yVelocity, double radius, double mass)
   {
      _xPos = xPos;
      _yPos = yPos;
      _xVelocity = xVelocity;
      _yVelocity = yVelocity;
      _radius = radius;
      _mass = mass;
   }

   /// <summary>
   ///    Returns the number of collisions involving this particle with
   ///    vertical walls, horizontal walls, or other particles.
   ///    <remarks>
   ///       This is equal to the number of calls to <seealso cref="BounceOff" />,
   ///       <seealso cref="BounceOffVerticalWall" />, and
   ///       <seealso cref="BounceOffHorizontalWall" />.
   ///    </remarks>
   /// </summary>
   public int CollisionCount { get; private set; }

   /// <summary>
   ///    Moves this particle in a straight line (based on its velocity) for the specified amount of time.
   /// </summary>
   /// <param name="deltaTime">The amount of time</param>
   public void Move(double deltaTime)
   {
      _xPos += _xVelocity * deltaTime;
      _yPos += _yVelocity * deltaTime;
   }

   /// <summary>
   ///    Returns the amount of time for this particle to collide with the specified particle, assuming no interening
   ///    collisions.
   /// </summary>
   /// <param name="otherParticle">The other particle</param>
   /// <returns>
   ///    The amount of time for this particle to collide with the specified particle, assuming no interening
   ///    collisions; <see cref="double.PositiveInfinity" /> if the particles will not collide
   /// </returns>
   public double GetTimeToHit(Particle otherParticle)
   {
      if (this == otherParticle)
      {
         return Inf;
      }

      var deltaXPos = otherParticle._xPos - _xPos;
      var deltaYPos = otherParticle._yPos - _yPos;
      var deltaXVelocity = otherParticle._xVelocity - _xVelocity;
      var delatYVelocity = otherParticle._yVelocity - _yVelocity;
      var deltaRadius = deltaXPos * deltaXVelocity + deltaYPos * delatYVelocity;
      if (deltaRadius > 0)
      {
         return Inf;
      }

      var deltaVelocity = deltaXVelocity * deltaXVelocity + delatYVelocity * delatYVelocity;
      if (deltaVelocity == 0)
      {
         return Inf;
      }

      var deltaPos = deltaXPos * deltaXPos + deltaYPos * deltaYPos;
      var sigma = _radius + otherParticle._radius;
      var totalSq = deltaRadius * deltaRadius - deltaVelocity * (deltaPos - sigma * sigma);
      if (totalSq < 0)
      {
         return Inf;
      }

      return -(deltaRadius + Math.Sqrt(totalSq)) / deltaVelocity;
   }

   /// <summary>
   ///    Returns the amount of time for this particle to collide with a vertical wall, assuming no interening collisions.
   /// </summary>
   /// <returns>
   ///    The amount of time for this particle to collide with a vertical wall, assuming no interening collisions;
   ///    <see cref="double.PositiveInfinity" /> if the particle will not collide with a vertical wall
   /// </returns>
   public double GetTimeToHitVerticalWall() => _xVelocity switch
      {
         > 0 => (1.0 - _xPos - _radius) / _xVelocity,
         < 0 => (_radius - _xPos) / _xVelocity,
         _ => Inf
      };

   /// <summary>
   ///    Returns the amount of time for this particle to collide with a horizontal wall, assuming no interening collisions.
   /// </summary>
   /// <returns>
   ///    The amount of time for this particle to collide with a horizontal wall, assuming no interening collisions;
   ///    <see cref="double.PositiveInfinity" /> if the particle will not collide with a horizontal wall
   /// </returns>
   public double GetTimeToHitHorizontalWall() => _yVelocity switch
      {
         > 0 => (1.0 - _yPos - _radius) / _yVelocity,
         < 0 => (_radius - _yPos) / _yVelocity,
         _ => Inf
      };

   /// <summary>
   ///    Updates the velocities of this particle and the specified particle according
   ///    to the laws of elastic collision. Assumes that the particles are colliding
   ///    at this instant.
   /// </summary>
   /// <param name="otherParticle">The other particle</param>
   public void BounceOff(Particle otherParticle)
   {
      var deltaX = otherParticle._xPos - _xPos;
      var deltaY = otherParticle._yPos - _yPos;
      var deltaXVel = otherParticle._xVelocity - _xVelocity;
      var deltaYVel = otherParticle._yVelocity - _yVelocity;
      var dotProduct = deltaX * deltaXVel + deltaY * deltaYVel; // dv dot dr
      var distance = _radius + otherParticle._radius; // distance between particle centers at collison

      // magnitude of normal force
      var magnitude = 2 * _mass * otherParticle._mass * dotProduct / ((_mass + otherParticle._mass) * distance);

      // normal force, and in x and y directions
      var xForce = magnitude * deltaX / distance;
      var yForce = magnitude * deltaY / distance;

      // update velocities according to normal force
      _xVelocity += xForce / _mass;
      _yVelocity += yForce / _mass;
      otherParticle._xVelocity -= xForce / otherParticle._mass;
      otherParticle._yVelocity -= yForce / otherParticle._mass;

      // update collision counts
      CollisionCount++;
      otherParticle.CollisionCount++;
   }

   /// <summary>
   ///    Updates the velocity of this particle upon collision with a vertical
   ///    wall (by reflecting the velocity in the <em>x</em>-direction).
   ///    Assumes that the particle is colliding with a vertical wall at this instant.
   /// </summary>
   public void BounceOffVerticalWall()
   {
      _xVelocity = -_xVelocity;
      CollisionCount++;
   }

   /// <summary>
   ///    Updates the velocity of this particle upon collision with a horizontal
   ///    wall (by reflecting the velocity in the <em>y</em>-direction).
   ///    Assumes that the particle is colliding with a horizontal wall at this instant.
   /// </summary>
   public void BounceOffHorizontalWall()
   {
      _yVelocity = -_yVelocity;
      CollisionCount++;
   }

   /// <summary>
   ///    Returns the kinetic energy of this particle.
   ///    The kinetic energy is given by the formula 1/2 <em>m</em> <em>v</em><sup>2</sup>,
   ///    where <em>m</em> is the mass of this particle and <em>v</em> is its velocity.
   /// </summary>
   /// <returns>The kinetic energy of this particle</returns>
   public double KineticEnergy() => 0.5 * _mass * (_xVelocity * _xVelocity + _yVelocity * _yVelocity);

   public override string ToString() =>
      $"{nameof(_xPos)}: {_xPos}, {nameof(_xVelocity)}: {_xVelocity}, {nameof(_yPos)}: {_yPos}, {nameof(_yVelocity)}: {_yVelocity}, {nameof(CollisionCount)}: {CollisionCount}";
}