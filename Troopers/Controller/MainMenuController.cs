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
    class MainMenuController : ControllerBase
    {
        private MainMenu _mainMenu;
        public MainMenuView _mainMenuView { get; set; }

        public event EventHandler StartGame;
        public event EventHandler ExitGame;

        public MainMenuController(int viewportWidth, int viewportHeight, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _viewportWidth = viewportWidth;
            _viewportHeight = viewportHeight;
            _graphicsDevice = graphicsDevice;
            _content = content;
            _mainMenu = new MainMenu(new Vector2(0.1f,0.1f), 0.7f);
            _mainMenuView = new MainMenuView(_graphicsDevice, _content, _mainMenu, GetCamera());
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
                _mainMenu.Update(gameTime, GetMenuNavigation(keyboardState));  
            }
            
            _oldKeyboardState = keyboardState;
        }

        private void HandleEnterKeyPress()
        {
            if (_mainMenu.SelectedItem.Text == "Start")
                StartFirstLevel();

            if (_mainMenu.SelectedItem.Text == "Exit")
                OnExitGame();
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
            EventHandler handler = StartGame;
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
            _mainMenuView.Draw(spriteBatch);
        }

        internal void LoadContent()
        {
            _mainMenuView.LoadContent(_content);
        }


    }


}
