using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Troopers.View
{
    class GameObjectView
    {
       // protected SpriteBatch spriteBatch;
        private Texture2D _gameObjectTexture;

        public Texture2D GameObjectTexture
        {
            get { return _gameObjectTexture; }
            set { _gameObjectTexture = value; }
        }
        protected Rectangle DestinationRectangle;
        protected Camera Camera;

        public GameObjectView(Camera camera)
        {
            Camera = camera;

          //  this.spriteBatch = new SpriteBatch(graphicsDevice);

         //   _gameObjectTexture = contentTexture; //content.Load<Texture2D>(contentName);
            
        }

        protected void Draw(SpriteBatch spriteBatch, Rectangle transformedDestinationTriangle)
        {
            DestinationRectangle = transformedDestinationTriangle;
            spriteBatch.Draw(_gameObjectTexture, DestinationRectangle, Color.White);
            
        }

        protected void Draw(SpriteBatch spriteBatch)
        {
            TransformDesinationRectangle();

            spriteBatch.Draw(_gameObjectTexture, DestinationRectangle, Color.White);

        }

        private void TransformDesinationRectangle()
        {
            DestinationRectangle.X = (int)Camera.TransformX(DestinationRectangle.X);
            DestinationRectangle.Y = (int)Camera.TransformY(DestinationRectangle.Y);
            DestinationRectangle.Width = (int)Camera.Scale(DestinationRectangle.Width);
            DestinationRectangle.Height = (int)Camera.Scale(DestinationRectangle.Height);
        }

        internal void Draw(SpriteBatch spriteBatch, float logicalX, float logicalY, int width, int height)
        {
            DestinationRectangle = new Rectangle((int)Camera.TransformX(logicalX), (int)Camera.TransformY(logicalY), width, height);
            spriteBatch.Draw(_gameObjectTexture, DestinationRectangle, Color.White);
        }
    }
}
