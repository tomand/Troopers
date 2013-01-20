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
    class PauseMenuController : ControllerBase
    {
        private PauseMenu _pauseMenu;
        public MenuView _pauseMenuView { get; set; }

        public event EventHandler RestartGame;
        public event EventHandler ExitGame;
        public event EventHandler ResumeGame;
        public event EventHandler ShowHelp;
        public event EventHandler MainMenuActivated;

        public PauseMenuController(int viewportWidth, int viewportHeight, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _viewportWidth = viewportWidth;
            _viewportHeight = viewportHeight;
            _graphicsDevice = graphicsDevice;
            _content = content;
            _pauseMenu = new PauseMenu(new Vector2(0.1f, 0.1f), 0.12f);
            _pauseMenuView = new MenuView(_graphicsDevice, _content, _pauseMenu, GetCamera());
        }

        private Camera GetCamera()
        {
            return new Camera(_viewportHeight, _viewportWidth, 0, 0, _viewportHeight, _viewportHeight, 1, 1);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (IsKeyPressed(keyboardState, Keys.Enter))
                HandleEnterKeyPress();
            else
            {
                _pauseMenu.Update(gameTime, GetMenuNavigation(keyboardState));  
            }
            
            _oldKeyboardState = keyboardState;
        }

        private void HandleEnterKeyPress()
        {
            if (_pauseMenu.SelectedItem.Text == "Resume")
                OnResume();

            if (_pauseMenu.SelectedItem.Text == "Restart")
                OnRestart();

            if (_pauseMenu.SelectedItem.Text == "Main menu")
                OnMainMenu();

            if (_pauseMenu.SelectedItem.Text == "Help")
                OnShowHelp();

            if (_pauseMenu.SelectedItem.Text == "Exit")
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

        private void OnResume()
        {
            EventHandler handler = ResumeGame;
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

        private void StartFirstLevel()
        {
            EventHandler handler = RestartGame;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        private MenuNavigation GetMenuNavigation(KeyboardState keyboardState)
        {
            if ( IsKeyPressed(keyboardState, Keys.Down)) 
                return MenuNavigation.Next;

            if (IsKeyPressed(keyboardState, Keys.Up))
                return MenuNavigation.Previous;

            return MenuNavigation.None;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _pauseMenuView.Draw(spriteBatch);
        }

        internal void LoadContent()
        {
            _pauseMenuView.LoadContent(_content);
        }


    }
}


