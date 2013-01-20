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
        private View.Camera cam;

        public CursorView(Camera cam, Cursor cursor)
            : base(cam)
        {
            Cursor = cursor;
        }

        public CursorView(View.Camera cam)
            : base(cam)
        {
        }

        public Cursor Cursor
        {
            get { return _cursor; }
            set { _cursor = value; }
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            DestinationRectangle = new Rectangle(Camera.TransformX(Cursor.Position.X),
                    Camera.TransformY(Cursor.Position.Y)
                    , Camera.TransformSizeX(Cursor.Width)
                    , Camera.TransformSizeY(Cursor.Height));



            Rectangle? sourceRectangle = new Rectangle(GetSourceXValue(), 0, GameObjectTexture.Height, GameObjectTexture.Height);
            spriteBatch.Draw(GameObjectTexture, DestinationRectangle, sourceRectangle, Color.White);
            

        }

        private int GetSourceXValue()
        {
            if (Cursor.BlockedByBuilding)
            {
                return 108;
            }
            
            if (Cursor.MarksEnemyTrooper)
            {
                return 81;
            }

            int returnValue = 0;
            switch (Cursor.DistanceGrade)
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
