using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Troopers.Model;
using Troopers.View;

namespace Troopers.Controller
{
    internal class GameOverMenuController : ControllerBase
    {
        private GameOverMenu _gameOverMenu;
        public MenuView _gameOverMenuView { get; set; }
        public bool PlayerWon { set { _gameOverMenu.PlayerWon = value; } }

        public event EventHandler RestartGame;
        public event EventHandler ExitGame;
        public event EventHandler ContinueGame;
        public event EventHandler ShowHelp;
        public event EventHandler MainMenuActivated;

        public GameOverMenuController(int viewportWidth, int viewportHeight, GraphicsDevice graphicsDevice,
                                      ContentManager content)
        {
            _viewportWidth = viewportWidth;
            _viewportHeight = viewportHeight;
            _graphicsDevice = graphicsDevice;
            _content = content;
            _gameOverMenu = new GameOverMenu(new Vector2(0.1f, 0.1f), 0.07f);
            _gameOverMenuView = new MenuView(_graphicsDevice, _content, _gameOverMenu, GetCamera());
        }

        private Camera GetCamera()
        {
            return new Camera(_viewportHeight, _viewportWidth, 0, 0, _viewportHeight, _viewportHeight, 1, 1);
        }

        public void Update(GameTime gameTime)
        {

            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (IsKeyPressed(keyboardState, Keys.Enter) || IsMouseLeftClicked(mouseState))
                HandleEnterKeyPress();
            else
            {
                _gameOverMenu.Update(gameTime, GetMenuNavigation(keyboardState, mouseState.ScrollWheelValue));
            }

            _oldKeyboardState = keyboardState;
            _oldMouseState = mouseState;

           
        }

        private void HandleEnterKeyPress()
        {
            if (_gameOverMenu.SelectedItem.Text == "Continue")
                OnContinue();

            if (_gameOverMenu.SelectedItem.Text == "Restart")
                OnRestart();

            if (_gameOverMenu.SelectedItem.Text == "Main menu")
                OnMainMenu();

            if (_gameOverMenu.SelectedItem.Text == "Help")
                OnShowHelp();

            if (_gameOverMenu.SelectedItem.Text == "Exit")
                OnExitGame();
        }

        private void OnMainMenu()
        {
            EventHandler handler = MainMenuActivated;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        private void OnRestart()
        {
            EventHandler handler = RestartGame;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        private void OnContinue()
        
        {
            EventHandler handler = ContinueGame;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        private void OnShowHelp()
        {
            EventHandler handler = ShowHelp;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        private void OnExitGame()
        {
            EventHandler handler = ExitGame;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }


        private MenuNavigation GetMenuNavigation(KeyboardState keyboardState, int scrollWheelValue)
        {
            if (IsKeyPressed(keyboardState, Keys.Down) || scrollWheelValue < _oldMouseState.ScrollWheelValue)
                return MenuNavigation.Next;

            if (IsKeyPressed(keyboardState, Keys.Up) || scrollWheelValue > _oldMouseState.ScrollWheelValue)
                return MenuNavigation.Previous;

            return MenuNavigation.None;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _gameOverMenuView.Draw(spriteBatch);
        }

        internal void LoadContent()
        {
            _gameOverMenuView.LoadContent(_content);
        }



    }
}
