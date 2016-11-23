using System;
using System.Collections.Generic;
using BallWorld.PhysicsEngine;
using FarseerGames.FarseerPhysics.Factories;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Mathematics;
using System.Windows.Media;
using BallWorld.ViewModel;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics;

namespace BallWorld.PhysicsEngine
{
    public class BallWorldPhysicsEngine
    {
        private PhysicsEngineLoop physicsEngineLoop;
        private PhysicsEngine physicsEngine;
        private PhysicsSimulator physicsSimulator;

        public BallWorldPhysicsEngine()
        {
            physicsEngineLoop = new PhysicsEngineLoop();
            physicsEngineLoop.IsStartedChanged += IsStartedChangedEventHandler;

            physicsEngine = new PhysicsEngine(new Vector2(0f, 0f));
            physicsSimulator = physicsEngine.PhysicsSimulator;
            physicsEngine.SetLoop(physicsEngineLoop);
        }

        public void Start()
        {
            physicsEngineLoop.Start();
        }

        public void AddBorder(BorderViewModel border)
        {
            //use the body factory to create the physics body
            Body borderBody = BodyFactory.Instance.CreateRectangleBody(physicsSimulator, border.Width, border.Height, 50);
            borderBody.IsStatic = true;
            borderBody.Position = border.Position;

            //left border geometry
            Vector2 geometryOffset = new Vector2(-(border.Width * .5f - border.BorderWidth * .5f), 0);
            CreateBorderGeom(borderBody, border.BorderWidth, border.Height, geometryOffset);

            //right border geometry
            geometryOffset = new Vector2(border.Width * .5f - border.BorderWidth * .5f, 0);
            CreateBorderGeom(borderBody, border.BorderWidth, border.Height, geometryOffset);
            
            //top border geometry
            geometryOffset = new Vector2(0, -(border.Height * .5f - border.BorderWidth * .5f));
            CreateBorderGeom(borderBody, border.Width, border.BorderWidth, geometryOffset);

            //bottom border geometry
            geometryOffset = new Vector2(0, border.Height * .5f - border.BorderWidth * .5f);
            CreateBorderGeom(borderBody, border.Width, border.BorderWidth, geometryOffset);
        }

        private void CreateBorderGeom(Body borderBody, float width, float height, Vector2 geometryOffset)
        {
            Geom geom = GeomFactory.Instance.CreateRectangleGeom(
                physicsSimulator, borderBody, width, height, geometryOffset, 0);
            geom.RestitutionCoefficient = 1f;
            geom.FrictionCoefficient = 0f;
            geom.CollisionGroup = 100;
        }

        public void AddBall(BallViewModel ball)
        {
            float bodyMass = ball.Radius;
            Body body = BodyFactory.Instance.CreateCircleBody(ball.Radius, bodyMass);
            body.Position = ball.Position.Vector;
            body.LinearVelocity = ball.Velocity.Vector;

            BodyModelHelper<BallViewModel> helper = new BodyModelHelper<BallViewModel>(ball, body, 
                (UpdateEventHandler<BallViewModel>) delegate(BallViewModel ball1, Vector2 position, float rotation)
                {
                    ball1.Position = new Vector2D(position.X, position.Y);
                    ball1.Velocity = new Vector2D(body.LinearVelocity.X, body.LinearVelocity.Y);
                });

            body.Updated += delegate { helper.Update();  };

            Geom geom = GeomFactory.Instance.CreateCircleGeom(body, ball.Radius, 60, 25);
            geom.FrictionCoefficient = 0f;
            geom.RestitutionCoefficient = 1f;
            geom.OnCollision += OnCollision;

            physicsEngine.AddBody(body);
            physicsEngine.AddGeom(geom);
        }

        private bool OnCollision(Geom geom1, Geom geom2, ContactList contactList)
        {
            float geom1Speed = geom1.Body.LinearVelocity.Length();
            float geom2Speed = geom2.Body.LinearVelocity.Length();

            if (geom1Speed > 80)
            {
                float factor = 50 / geom1Speed;
                geom1.Body.LinearVelocity.X = geom1.Body.LinearVelocity.X * factor;
                geom1.Body.LinearVelocity.Y = geom1.Body.LinearVelocity.Y * factor;
            }

            if (geom2Speed > 80)
            {
                float factor = 50 / geom2Speed;
                geom2.Body.LinearVelocity.X = geom2.Body.LinearVelocity.Y * factor;
                geom2.Body.LinearVelocity.Y = geom2.Body.LinearVelocity.Y * factor;
            }
            
            return true;
        }

        private void IsStartedChangedEventHandler(bool isStarted)
        {
            if (isStarted)
                CompositionTarget.Rendering += physicsEngineLoop.RenderingEventHandler;
            else
                CompositionTarget.Rendering -= physicsEngineLoop.RenderingEventHandler;
        }
    }
}
