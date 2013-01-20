using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Troopers.Model;

namespace Troopers.View
{
    class BuildingView : GameObjectView
    {
        
        public BuildingView(Camera cam)
            : base(cam)
        {

        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime, Building building)
        {
            DestinationRectangle = new Rectangle(Camera.TransformX(building.Position.X),
                                                 Camera.TransformY(building.Position.Y)
                                                 , Camera.TransformSizeX(building.Width)
                                                 , Camera.TransformSizeY(building.Height));


            spriteBatch.Draw(GameObjectTexture, DestinationRectangle, Color.White);

        }


        internal void LoadContent(ContentManager content)
        {
            GameObjectTexture = content.Load<Texture2D>("medikit");

        }
    }
}