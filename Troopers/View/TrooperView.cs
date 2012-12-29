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
        private Texture2D _tileMark;

        public TrooperView(Camera cam)
            : base(cam)
        {
       
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime, Trooper trooper)
        {
            DestinationRectangle = new Rectangle(Camera.TransformX(trooper.Position.X),
                    Camera.TransformY(trooper.Position.Y)
                    , Camera.TransformSizeX(trooper.Width)
                    , Camera.TransformSizeY(trooper.Height));
            Vector2 size = new Vector2(Camera.TransformSizeX(trooper.Width), Camera.TransformSizeY(trooper.Height));

         //   spriteBatch.Draw(GameObjectTexture, new Vector2(Camera.TransformX(trooper.Position.X), Camera.TransformY(trooper.Position.Y)), null, Color.White, trooper.FaceDirection, origin: 
            //Camera.Transform(new Vector2(trooper.Width / 2,trooper.Height / 2)), effects: SpriteEffects.None, layerDepth: 0);
        //    spriteBatch.Draw(GameObjectTexture, DestinationRectangle, Color.White);

            Vector2 position = new Vector2(Camera.TransformX(trooper.Position.X + trooper.Width / 2), Camera.TransformY(trooper.Position.Y + trooper.Height / 2));
            
            Vector2 origin = new Vector2(Camera.TransformSizeX( trooper.Width/2), Camera.TransformSizeY(trooper.Height/2));
            
            int x = DestinationRectangle.X +  Camera.TransformSizeX(trooper.Width/2);
            int y = DestinationRectangle.Y + Camera.TransformSizeY(trooper.Height / 2);
            spriteBatch.Draw(GameObjectTexture, new Rectangle(x,y, DestinationRectangle.Width, DestinationRectangle.Height), null, Color.White, trooper.FaceDirection, origin, SpriteEffects.None, 0);
            Rectangle? sourceRectangle = new Rectangle(0, 0, _tileMark.Height, _tileMark.Height);
            spriteBatch.Draw(_tileMark, DestinationRectangle, sourceRectangle, Color.White);
          
        }

        internal void LoadContent(ContentManager content)
        {
            GameObjectTexture = content.Load<Texture2D>("trooper");
            _tileMark = content.Load<Texture2D>("tilemark");
        }
    }
}
