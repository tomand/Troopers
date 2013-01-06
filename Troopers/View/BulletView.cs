using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Troopers.Model;

namespace Troopers.View
{
    class BulletView : GameObjectView
    {
        public BulletView(Camera cam)
            :base(cam)
        {
              
  
        }

        internal void Draw(SpriteBatch spriteBatch, Bullet bullet)
        {
             DestinationRectangle = new Rectangle(Camera.TransformX(bullet.CurrentPosition.X),
                    Camera.TransformY(bullet.CurrentPosition.Y)
                    , 3
                    , 3);
            spriteBatch.Draw(GameObjectTexture, DestinationRectangle, Color.White);
        }

        internal void LoadContent(ContentManager content)
        {
            GameObjectTexture = content.Load<Texture2D>("black");
        }
    }
}
