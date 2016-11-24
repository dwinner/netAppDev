using System.Windows;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Mathematics;

namespace BallWorld.ViewModel
{
    public delegate void UpdateEventHandler<T>(T model, Vector2 position, float rotation); 

    public class BodyModelHelper<T>
    {
        private UpdateEventHandler<T> updateEventHandler;

        private T model;
        public T Model
        {
            get { return model; }
        }

        private Body body;
        public Body Body
        {
            get { return body; }
        }

        public BodyModelHelper(T model, Body body, UpdateEventHandler<T> updateEventHandler)
        {
            this.model = model;
            this.body = body;
            this.updateEventHandler = updateEventHandler;
        }

        public void Update()
        {
            updateEventHandler(model, body.Position, body.Rotation);
        }
    }
}