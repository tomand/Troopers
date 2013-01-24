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
    class MenuView : GameObjectView
    {
        private BaseMenu _menu;
        private SpriteFont font;
        private SpriteFont _helpFont;

        public MenuView(GraphicsDevice graphicsDevice, ContentManager content, BaseMenu menu, Camera cam)
            : base(cam)
        {
            _menu = menu;
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            float positionY = _menu.Position.Y;
            float positionYIncrement =  _menu.Height / ((float)_menu.GetMenuItems().Count() + 1);
            spriteBatch.DrawString(font, _menu.Header, Camera.Transform(_menu.Position), Color.Orange);
            positionY += positionYIncrement;
            spriteBatch.DrawString(_helpFont, _menu.Help, Camera.Transform(new Vector2(_menu.Position.X, positionY)), Color.Gray);
            positionY += positionYIncrement;
            foreach (MenuItem menuItem in _menu.GetMenuItems())
            {
                DrawMenuItem(menuItem, spriteBatch, new Vector2(_menu.Position.X, positionY));
                positionY += positionYIncrement;
            }
        }

        private void DrawMenuItem(MenuItem menuItem, SpriteBatch spriteBatch, Vector2 menuItemPosition)
        {
            if (menuItem.IsSelected)
            {
                 Rectangle? sourceRectangle = new Rectangle(0, 0, GameObjectTexture.Height, GameObjectTexture.Height);

                DestinationRectangle = new Rectangle((int)Camera.TransformX(menuItemPosition.X - 0.04f), (int)Camera.TransformY(menuItemPosition.Y + 0.01f), Camera.TransformSizeX(0.03f), Camera.TransformSizeY(0.03f));
                spriteBatch.Draw(GameObjectTexture, DestinationRectangle, sourceRectangle, Color.White);
            }
            
                //base.Draw(spriteBatch, menuItemPosition.X - 0.04f, menuItemPosition.Y + 0.01f, Camera.TransformSizeX( 0.03f), Camera.TransformSizeY( 0.03f) );

            spriteBatch.DrawString(font, menuItem.Text, Camera.Transform(menuItemPosition), Color.White);
        }


        public void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("MenuFont");
            _helpFont = content.Load<SpriteFont>("HelpFont");
            GameObjectTexture = content.Load<Texture2D>("tilemark");
        }
    }
}
