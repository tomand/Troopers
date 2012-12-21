using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
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
        protected Rectangle destinationRectangle;
        protected Camera _camera;

        public GameObjectView(GraphicsDevice graphicsDevice, ContentManager content,  Camera camera)
        {
            _camera = camera;

          //  this.spriteBatch = new SpriteBatch(graphicsDevice);

         //   _gameObjectTexture = contentTexture; //content.Load<Texture2D>(contentName);
            
        }

        protected void Draw(SpriteBatch spriteBatch, Rectangle transformedDestinationTriangle)
        {
            destinationRectangle = transformedDestinationTriangle;
            spriteBatch.Draw(_gameObjectTexture, destinationRectangle, Color.White);
            
        }

        protected void Draw(SpriteBatch spriteBatch)
        {
            TransformDesinationRectangle();

            spriteBatch.Draw(_gameObjectTexture, destinationRectangle, Color.White);

        }

        private void TransformDesinationRectangle()
        {
            destinationRectangle.X = (int)_camera.TransformX(destinationRectangle.X);
            destinationRectangle.Y = (int)_camera.TransformY(destinationRectangle.Y);
            destinationRectangle.Width = (int)_camera.Scale(destinationRectangle.Width);
            destinationRectangle.Height = (int)_camera.Scale(destinationRectangle.Height);
        }

        internal void Draw(SpriteBatch spriteBatch, float logicalX, float logicalY, int width, int height)
        {
            destinationRectangle = new Rectangle((int)_camera.TransformX(logicalX), (int)_camera.TransformY(logicalY), width, height);
            spriteBatch.Draw(_gameObjectTexture, destinationRectangle, Color.White);
        }
    }
}
