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
            destinationRectangle = new Rectangle(_camera.TransformX(trooper.Position.X - trooper.Width),
                    _camera.TransformY(trooper.Position.Y - trooper.Height)
                    , _camera.TransformSizeX(trooper.Width)
                    , _camera.TransformSizeY(trooper.Height));

        //    spriteBatch.Draw(GameObjectTexture, destinationRectangle, null, Color.White, trooper.FaceDirection, _camera.Transform(trooper.Position), SpriteEffects.None, 0);
            spriteBatch.Draw(GameObjectTexture, destinationRectangle, Color.White);
            
        }

        internal void LoadContent(ContentManager content)
        {
            GameObjectTexture = content.Load<Texture2D>("trooper");
        }
    }
}
