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
    class MediKitView : GameObjectView
    {
        private Texture2D _tileMark;
        private BulletView _bulletView;

        public MediKitView(Camera cam)
            : base(cam)
        {
            
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime, MediKit mediKit)
        {
            DestinationRectangle = new Rectangle(Camera.TransformX(mediKit.Position.X),
                    Camera.TransformY(mediKit.Position.Y)
                    , GameObjectTexture.Width
                    , GameObjectTexture.Height);


              spriteBatch.Draw(GameObjectTexture, DestinationRectangle, Color.White);

        }


        internal void LoadContent(ContentManager content)
        {
            GameObjectTexture = content.Load<Texture2D>("medikit");

        }
    }
}
