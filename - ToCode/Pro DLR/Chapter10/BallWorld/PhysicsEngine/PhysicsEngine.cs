using System;
using System.Collections.Generic;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Dynamics.Springs;
using FarseerGames.FarseerPhysics.Factories;
using FarseerGames.FarseerPhysics.Mathematics;
using System.Diagnostics;

namespace BallWorld.PhysicsEngine
{
    public class PhysicsEngine 
    {
        private PhysicsEngineLoop physicsEngineLoop;
        private PhysicsSimulator physicsSimulator;
        
        public PhysicsSimulator PhysicsSimulator
        {
            get { return physicsSimulator; }
        }

        public PhysicsEngine(Vector2 gravity)
        {
            physicsSimulator = new PhysicsSimulator(gravity);
        }

        public void SetLoop(PhysicsEngineLoop loop)
        {
            if (this.physicsEngineLoop != null)
                this.physicsEngineLoop.LoopIteration -= Update;
            
            this.physicsEngineLoop = loop;

            if (this.physicsEngineLoop != null)
                this.physicsEngineLoop.LoopIteration += Update;
        }

        private void Update(TimeSpan span)
        {
            physicsSimulator.Update((float)span.TotalSeconds);
        }

        public void AddBody(Body body)
        {
            this.physicsSimulator.Add(body);
        }

        public void AddGeom(Geom geom)
        {
            this.physicsSimulator.Add(geom);
        }
    }
}