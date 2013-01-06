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
    class LevelController : ControllerBase
    {

        private List<Level> _levels;
        private Camera _levelCamera;
        private LevelView _levelView;
        private int _numberOfXTiles = 30;
        private int _numberOfYTiles = 30;
        private int _xTileSize = 20;
        private int _yTileSize = 20;
        private MouseState _oldMouseState;
        public event EventHandler PauseGame;

        protected virtual void OnPauseGame()
        {
            EventHandler handler = PauseGame;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler LevelFinished;

        protected virtual void OnLevelFinished()
        {
            EventHandler handler = LevelFinished;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        

        public LevelController(int viewportWidth, int viewportHeight, GraphicsDevice graphicsDevice, ContentManager content)
        {
            this._viewportWidth = viewportWidth;
            this._viewportHeight = viewportHeight;
            this._graphicsDevice = graphicsDevice;
            this._content = content;
            _levels = new List<Level> {new Level(_numberOfXTiles, _numberOfYTiles, new Vector2(0, 0))};
            _levelCamera = new Camera(_viewportHeight, _viewportWidth, 50, 10, _xTileSize, _yTileSize, _numberOfXTiles, _numberOfYTiles);
            _levelView = new LevelView(_graphicsDevice, _content, _levels, _levelCamera);
            
           
        }

        public void StartLevel()
        {
            _levels.First<Level>().Start();


        }

        internal void LoadConent()
        {
            _levelView.LoadContent(_content);
        }

        internal void Update(GameTime gameTime)
        {
            if (_levels.First<Level>().IsFinished)
            {
                OnLevelFinished();
            }
            
            KeyboardState keyboardState = Keyboard.GetState();
            if (IsKeyPressed(keyboardState, Keys.Space))
            {
                OnPauseGame();
            }
            
            MouseState newMouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(newMouseState.X, newMouseState.Y);
            Vector2 logicalMousePosition = _levelCamera.TransformScreenToLogic(mousePosition);

            _levels.First<Level>().Update(gameTime, logicalMousePosition, LeftButtonIsClicked(newMouseState));
            
            UpdateMouseState(newMouseState);
            _oldKeyboardState = keyboardState;
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
