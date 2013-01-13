using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Troopers.View
{
    class PauseMenuView
    {
        private Microsoft.Xna.Framework.Graphics.GraphicsDevice _graphicsDevice;
        private Microsoft.Xna.Framework.Content.ContentManager _content;
        private Model.PauseMenu _pauseMenu;
        private Camera camera;

        public PauseMenuView(Microsoft.Xna.Framework.Graphics.GraphicsDevice _graphicsDevice, Microsoft.Xna.Framework.Content.ContentManager _content, Model.PauseMenu _pauseMenu, Camera camera)
        {
            // TODO: Complete member initialization
            this._graphicsDevice = _graphicsDevice;
            this._content = _content;
            this._pauseMenu = _pauseMenu;
            this.camera = camera;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public void LoadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }
    }
}
