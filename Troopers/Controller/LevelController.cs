using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Troopers.Model;
using Microsoft.Xna.Framework;
using Troopers.View;

namespace Troopers.Controller
{
    class LevelController
    {
        private int _viewportWidth;
        private int _viewportHeight;
        private readonly GraphicsDevice _graphicsDevice;
        private readonly ContentManager _content;
        private readonly List<Level> _levels;
        private Camera _levelCamera;
        private readonly LevelView _levelView;
        private int _numberOfXTiles = 30;
        private int _numberOfYTiles = 30;
        private int _xTileSize = 20;
        private int _yTileSize = 20;
        private MouseState _oldMouseState;


        public LevelController(int viewportWidth, int viewportHeight, GraphicsDevice graphicsDevice, ContentManager content)
        {
            this._viewportWidth = viewportWidth;
            this._viewportHeight = viewportHeight;
            this._graphicsDevice = graphicsDevice;
            this._content = content;
            _levels = new List<Level>();
            _levels.Add(new Level(_numberOfXTiles, _numberOfYTiles, new Vector2(0, 0)));
            _levelCamera = new Camera(viewportHeight, viewportWidth, 50, 10, _xTileSize, _yTileSize, _numberOfXTiles, _numberOfYTiles);
            _levelView = new LevelView(_graphicsDevice, _content, _levels, _levelCamera);
            
        }

        internal void LoadConent()
        {
            _levelView.LoadContent(_content);
        }

        internal void Update(GameTime gameTime)
        {
            MouseState newMouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(newMouseState.X, newMouseState.Y);
            Vector2 logicalMousePosition = _levelCamera.TransformScreenToLogic(mousePosition);

            _levels.First<Level>().Update(gameTime, logicalMousePosition, LeftButtonIsClicked(newMouseState));
            
            UpdateMouseState(newMouseState);
        }

        private bool LeftButtonIsClicked(MouseState newMouseState)
        {
            return newMouseState.LeftButton == ButtonState.Pressed && _oldMouseState.LeftButton == ButtonState.Released;
        }

        private void UpdateMouseState(MouseState newMouseState)
        {
            _oldMouseState = newMouseState;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _levelView.Draw(spriteBatch, gameTime);
        }
    }
}
