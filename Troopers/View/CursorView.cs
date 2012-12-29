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



            Rectangle? sourceRectangle = new Rectangle(GetSourceXValue(), 0, GameObjectTexture.Height, GameObjectTexture.Height);
            spriteBatch.Draw(GameObjectTexture, DestinationRectangle, sourceRectangle, Color.White);
            

        }

        private int GetSourceXValue()
        {
            int returnValue = 0;
            switch (_cursor.DistanceGrade)
            {
                case Distance.Close:
                    returnValue = 0;
                    break;
                case Distance.Medium:
                    returnValue = 27;
                    break;
                default:
                    returnValue = 54;
                    break;

            }
            return returnValue;
        }

        internal void LoadContent(ContentManager content)
        {
            GameObjectTexture = content.Load<Texture2D>("tilemark");
        }
    }
}
