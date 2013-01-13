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

        private List<Model.Level> _levels;
        private Camera _levelCamera;
        private LevelView _levelView;
        private int _numberOfXTiles = 30;
        private int _numberOfYTiles = 30;
        private int _xTileSize = 20;
        private int _yTileSize = 20;
        private MouseState _oldMouseState;
        public event EventHandler PauseGame;
        public bool PlayerWon { get { return GetCurrentLevel().PlayerWon; } }

        protected virtual void OnPauseGame()
        {
            EventHandler handler = PauseGame;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler LevelFinished;
        private int _currentLevel;
        private KilledTrooperView _killedTrooperView;


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
            _levels = new List<Model.Level>
                {
                    new Model.Level(_numberOfXTiles, _numberOfYTiles, new Vector2(0, 0), "level1"),
                    new Model.Level(_numberOfXTiles, _numberOfYTiles, new Vector2(0, 0), "level2"),
                    new Model.Level(_numberOfXTiles, _numberOfYTiles, new Vector2(0, 0), "level3")
                };
            _levelCamera = new Camera(_viewportHeight, _viewportWidth, 10, 10, _xTileSize, _yTileSize, _numberOfXTiles, _numberOfYTiles);
            _levelView = new LevelView(_graphicsDevice, _content, _levels, _levelCamera);
            _killedTrooperView = new KilledTrooperView(_levelCamera);
           
        }

        public void StartLevel(int levelNumber)
        {
            _levels[levelNumber -1].Current = true;
            GetCurrentLevel().Start();
        }

        public void StartLevel()
        {
            GetCurrentLevel().Start();
        }

        private Level GetCurrentLevel()
        {
            return _levels.Find(l => l.Current);
        }

        internal void LoadConent()
        {
            _levelView.LoadContent(_content);
            _killedTrooperView.LoadContent(_content);
        }

        internal void Update(GameTime gameTime)
        {
            foreach (Trooper trooper in GetCurrentLevel().GetDeadTroopers())
            {
                _killedTrooperView.Play(trooper.CenterPosition);
            }

            if (GetCurrentLevel().IsFinished && !_killedTrooperView.IsAlive)
            {
                OnLevelFinished();
                return;
            }
            
            KeyboardState keyboardState = Keyboard.GetState();
            if (IsKeyPressed(keyboardState, Keys.Space))
            {
                OnPauseGame();
            }
            
            MouseState newMouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(newMouseState.X, newMouseState.Y);
            Vector2 logicalMousePosition = _levelCamera.TransformScreenToLogic(mousePosition);

            GetCurrentLevel().Update(gameTime, logicalMousePosition, LeftButtonIsClicked(newMouseState), IsKeyPressed(keyboardState, Keys.Tab));

            if (_killedTrooperView.IsAlive)
            {
                _killedTrooperView.Update(gameTime);
            }
            

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
            if (_killedTrooperView.IsAlive)
                _killedTrooperView.Draw(gameTime, spriteBatch);
        }


        internal void GotoNextLevel()
        {

            for (int i = 0; i < _levels.Count; i++)
            {
                if (_levels[i].Current && i < _levels.Count -1)
                {
                    _levels[i].Current = false;
                    _levels[i + 1].Current = true;
                    return;
                }
                else if (_levels[i].Current)
                {
                    _levels[i].Current = false;
                    _levels[0].Current = true;
                    return;
                }
            }
        }
    }

  

}
