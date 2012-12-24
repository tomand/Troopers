using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Troopers.Model;

namespace Troopers.View
{
    class TrooperView : GameObjectView
    {
       
        public TrooperView(GraphicsDevice graphicsDevice, ContentManager content, Camera cam)
            : base(graphicsDevice, content, cam)
        {
       
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime, Trooper trooper)
        {
            DestinationRectangle = new Rectangle(Camera.TransformX(trooper.Position.X - trooper.Width),
                    Camera.TransformY(trooper.Position.Y - trooper.Height)
                    , Camera.TransformSizeX(trooper.Width)
                    , Camera.TransformSizeY(trooper.Height));

        //    spriteBatch.Draw(GameObjectTexture, destinationRectangle, null, Color.White, trooper.FaceDirection, _camera.Transform(trooper.Position), SpriteEffects.None, 0);
            spriteBatch.Draw(GameObjectTexture, DestinationRectangle, Color.White);
            
        }

        internal void LoadContent(ContentManager content)
        {
            GameObjectTexture = content.Load<Texture2D>("trooper");
        }
    }
}
