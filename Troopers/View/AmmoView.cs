using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Troopers.Model;

namespace Troopers.View
{
    class AmmoView : GameObjectView
    {

        public AmmoView(Camera cam)
            : base(cam)
        {

        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime, Ammo ammo)
        {
            DestinationRectangle = new Rectangle(Camera.TransformX(ammo.Position.X),
                                                 Camera.TransformY(ammo.Position.Y)
                                                 , Camera.TransformSizeX(ammo.Width)
                                                 , Camera.TransformSizeY(ammo.Height)); ;


            spriteBatch.Draw(GameObjectTexture, DestinationRectangle, Color.White);

        }


        internal void LoadContent(ContentManager content)
        {
            GameObjectTexture = content.Load<Texture2D>("ammoclip");

        }
    }
}