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
        public bool PlayerWon { get { return _levelManager.CurrentLevel.PlayerWon; } }

        protected virtual void OnPauseGame()
        {
            EventHandler handler = PauseGame;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler LevelFinished;
        private int _currentLevel;
        private KilledTrooperView _killedTrooperView;
        private LevelManager _levelManager;
        private TrooperInfoController _trooperInfoController;


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
            _levelManager = new Model.LevelManager(_numberOfXTiles, _numberOfYTiles);
          
            _levelCamera = new Camera(_viewportHeight, _viewportWidth, xOffset: 10, yOffset: 10, xTileSize: _xTileSize, yTileSize: _yTileSize, numberOfXTiles: _numberOfXTiles, numberOfYTiles: _numberOfYTiles);
            
            _levelView = new LevelView(_graphicsDevice, _content, _levelManager, _levelCamera);
            _killedTrooperView = new KilledTrooperView(_levelCamera);
            _trooperInfoController = new TrooperInfoController( viewportWidth,  viewportHeight,  graphicsDevice,  content, xOffset: 10 +5 + _xTileSize * _numberOfXTiles, yOffset: 10);
        }

        public void StartLevel(int levelNumber)
        {
            _levelManager.StartLevel(levelNumber);
            _trooperInfoController.Update(null, _levelManager.CurrentLevel);

           
        }

        public void StartLevel()
        {
            _levelManager.StartLevel();
            _trooperInfoController.Update(null, _levelManager.CurrentLevel);
        }

        

        internal void LoadConent()
        {
            _levelView.LoadContent(_content);
            _killedTrooperView.LoadContent(_content);
            _trooperInfoController.LoadContent();
        }

        internal void Update(GameTime gameTime)
        {
            PlayDeadTrooperAnimation();

            if (_levelManager.CurrentLevel.IsFinished && !_killedTrooperView.IsAlive)
            {
                OnLevelFinished();
                return;
            }

            if (_killedTrooperView.IsAlive)
            {
                _killedTrooperView.Update(gameTime);
            }

            if (_levelManager.CurrentLevel.IsFinished)
                return;

            
            KeyboardState keyboardState = Keyboard.GetState();
            if (IsKeyPressed(keyboardState, Keys.Escape) || IsKeyPressed(keyboardState, Keys.P))
            {
                OnPauseGame();
            }
            
            MouseState newMouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(newMouseState.X, newMouseState.Y);
            Vector2 logicalMousePosition = _levelCamera.TransformScreenToLogic(mousePosition);

            _levelManager.CurrentLevel.Update(gameTime, logicalMousePosition, LeftButtonIsClicked(newMouseState), IsKeyPressed(keyboardState, Keys.Tab));

        
            

            UpdateMouseState(newMouseState);
            _oldKeyboardState = keyboardState;

            _trooperInfoController.Update(gameTime, _levelManager.CurrentLevel);
        }

        private void PlayDeadTrooperAnimation()
        {
            foreach (Trooper trooper in _levelManager.CurrentLevel.GetDeadTroopers())
            {
                _killedTrooperView.Play(trooper.CenterPosition);
            }
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

            _trooperInfoController.Draw(gameTime, spriteBatch);
        }


        internal void GotoNextLevel()
        {

            _levelManager.GotoNextLevel();
        }
    }
}


