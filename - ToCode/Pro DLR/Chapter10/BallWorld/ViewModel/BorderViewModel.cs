using FarseerGames.FarseerPhysics.Mathematics;

namespace BallWorld.ViewModel
{
    public class BorderViewModel
    {
        private readonly float borderWidth;
        private readonly float height;
        private readonly float width;
        private Vector2 position;

        public float BorderWidth { get { return borderWidth; } }
        public float Height { get { return height; } }
        public float Width { get { return width; } }
        public Vector2 Position { get { return position; } }

        public BorderViewModel(float width, float height, float borderWidth, Vector2 position)
        {
            this.width = width;
            this.height = height;
            this.borderWidth = borderWidth;
            this.position = position;
        }
    }
}