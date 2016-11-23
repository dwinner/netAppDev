using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerGames.FarseerPhysics.Mathematics;
using BallWorld.ViewModel;

namespace BallWorld.ViewModel
{
    public class Vector2D : ViewModelBase
    {
        private Vector2 vector;
        
        public Vector2D(float x, float y)
        {
            vector = new Vector2(x, y);
        }

        public Vector2 Vector
        {
            get
            {
                return vector;
            }

            set
            {
                if (vector != value)
                {
                    bool xChanged = false;
                    if (vector.X != value.X)
                        xChanged = true;

                    bool yChanged = false;
                    if (vector.Y != value.Y)
                        yChanged = true;

                    vector = value;
                    
                    if (xChanged)
                        FirePropertyChangedEvent("X");

                    if (yChanged)
                        FirePropertyChangedEvent("Y");
                }
            }
        }

        public float X
        {
            get
            {
                return vector.X;
            }
        }

        public float Y
        {
            get
            {
                return vector.Y;
            }
        }
    }
}
