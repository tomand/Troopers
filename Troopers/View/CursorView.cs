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
    class CursorView : GameObjectView
    {
        private Cursor _cursor;

        public CursorView(Camera cam, Cursor cursor)
            : base(cam)
        {
            _cursor = cursor;
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            DestinationRectangle = new Rectangle(Camera.TransformX(_cursor.Position.X),
                    Camera.TransformY(_cursor.Position.Y)
                    , Camera.TransformSizeX(_cursor.Width)
                    , Camera.TransformSizeY(_cursor.Height));
            
            spriteBatch.Draw(GameObjectTexture, DestinationRectangle, Color.White);

        }

        internal void LoadContent(ContentManager content)
        {
            GameObjectTexture = content.Load<Texture2D>("tilemark");
        }
    }
}
