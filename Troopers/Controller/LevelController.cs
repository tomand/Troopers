using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Troopers.Model;
using Microsoft.Xna.Framework;
using Troopers.View;

namespace Troopers.Controller
{
    class LevelController
    {
        private int _viewportWidth;
        private int _viewportHeight;
        private Microsoft.Xna.Framework.Graphics.GraphicsDevice _graphicsDevice;
        private Microsoft.Xna.Framework.Content.ContentManager _content;
        private List<Level> _levels;
        private Camera _levelCamera;
        private LevelView _levelView;
        private int _numberOfXTiles = 120;
        private int _numberOfYTiles = 120;

        public LevelController(int viewportWidth, int viewportHeight, GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            this._viewportWidth = viewportWidth;
            this._viewportHeight = viewportHeight;
            this._graphicsDevice = GraphicsDevice;
            this._content = Content;
            _levels = new List<Level>();
            _levels.Add(new Level(_numberOfXTiles, _numberOfYTiles, new Vector2(0, 0)));
            _levelCamera = new Camera(viewportHeight, viewportWidth, 50, 10, 5, 5, _numberOfXTiles, _numberOfYTiles);
            _levelView = new LevelView(_graphicsDevice, _content, _levels, _levelCamera);
        }

        internal void LoadConent()
        {
            _levelView.LoadContent(_content);
        }

        internal void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _levelView.Draw(spriteBatch, gameTime);
        }
    }
}
