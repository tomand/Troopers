using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Troopers.Model;
using Troopers.View;

namespace Troopers.Controller
{
    internal class InfoController :ControllerBase
    {
        private Info _trooperInfo;
        private InfoView _trooperInfoView;
        private Level _currentLevel;
        private Trooper _currentTrooper;

        public InfoController(int viewportWidth, int viewportHeight, GraphicsDevice graphicsDevice, ContentManager content, int xOffset, int yOffset)
        {
            _viewportWidth = viewportWidth;
            _viewportHeight = viewportHeight;
            _graphicsDevice = graphicsDevice;
            _content = content;
        
            _trooperInfo = new Model.Info(new Vector2(0,1));

            _trooperInfoView = new View.InfoView(new Camera(viewportWidth, viewportHeight, xOffset,yOffset, 180, 30, 1, 20 ), _trooperInfo);
        }



        internal void LoadContent()
        {
            _trooperInfoView.LoadContent(_content);
        }


        public void Update(GameTime gameTime, Level currentLevel)
        {
            _currentLevel = currentLevel;
            _trooperInfo.Trooper = _currentLevel.GetCurrentTrooper();
        }

        internal void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _trooperInfoView.Draw(gameTime, spriteBatch);
        }
    }
}

