using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using FarseerGames.FarseerPhysics.Mathematics;
using BallWorld.ViewModel;

namespace BallWorld.ViewModel
{
    public class BallViewModel : ViewModelBase
    {
        private Vector2D position = new Vector2D(0f, 0f);
        public Vector2D Position
        {
            get
            {
                return position;
            }

            set
            {
                if (position != value)
                {
                    position.Vector = value.Vector;
                    normalPosition.Vector = new Vector2(value.X - radius, value.Y - radius);
                }
            }
        }

        private Vector2D normalPosition = new Vector2D(0f, 0f);
        public Vector2D NormalPosition
        {
            get
            {
                return normalPosition;
            }
        }

        private Vector2D velocity = new Vector2D(0f, 0f);
        public Vector2D Velocity
        {
            get
            {
                return velocity;
            }

            set
            {
                if (velocity != value)
                    velocity.Vector = value.Vector;
            }
        }

        private float radius;
        public float Radius
        {
            get { return radius; }
        }

        public float Diameter
        {
            get { return radius*2; }
        }

        private Color color;
        public Color Color
        {
            get { return color; }
        }

        public BallViewModel(Color color, float radius, Vector2D position, Vector2D velocity)
        {
            this.color = color;
            this.radius = radius;
            this.Position = position;
            this.Velocity = velocity;
        }
    }
}
