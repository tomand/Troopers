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
    class MainMenuView : GameObjectView
    {
        private MainMenu _mainMenu;
        private SpriteFont font;

        public MainMenuView(GraphicsDevice graphicsDevice, ContentManager content, MainMenu mainMenu, Camera cam)
            : base(cam)
        {
            _mainMenu = mainMenu;


        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            float positionY = _mainMenu.Position.Y;
            float positionYIncrement =  _mainMenu.Height / (float)_mainMenu.GetMenuItems().Count()  ;
            foreach (MenuItem menuItem in _mainMenu.GetMenuItems())
            {
                DrawMenuItem(menuItem, spriteBatch, new Vector2(_mainMenu.Position.X, positionY));
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
            GameObjectTexture = content.Load<Texture2D>("tilemark");
        }
    }
}
