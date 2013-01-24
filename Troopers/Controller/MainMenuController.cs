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
        public MenuView MenuView { get; set; }

        public event EventHandler StartGame;
        public event EventHandler ExitGame;
        public event EventHandler ShowHelp;

        public MainMenuController(int viewportWidth, int viewportHeight, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _viewportWidth = viewportWidth;
            _viewportHeight = viewportHeight;
            _graphicsDevice = graphicsDevice;
            _content = content;
            _mainMenu = new MainMenu(new Vector2(0.1f, 0.1f), 0.07f);
            MenuView = new MenuView(_graphicsDevice, _content, _mainMenu, GetCamera());
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
                _mainMenu.Update(gameTime, GetMenuNavigation(keyboardState, mouseState.ScrollWheelValue));  
            }
            
            _oldKeyboardState = keyboardState;
            _oldMouseState = mouseState;
        }


        private void HandleEnterKeyPress()
        {
            if (_mainMenu.SelectedItem.Text == "Start")
                StartLevel(1);

            if (_mainMenu.SelectedItem.Text == "Start Level 1")
                StartLevel(1);

            if (_mainMenu.SelectedItem.Text == "Start Level 2")
                StartLevel(2);

            if (_mainMenu.SelectedItem.Text == "Start Level 3")
                StartLevel(3);

            if (_mainMenu.SelectedItem.Text == "Help")
                OnHelpSelected();

            if (_mainMenu.SelectedItem.Text == "Exit")
                OnExitGame();
        }

        private void OnHelpSelected()
        {
            EventHandler handler = ShowHelp ;
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

        private void StartLevel(int levelNumber)
        {
            EventHandler handler = StartGame;
            if (handler != null)
            {
                var level = new LevelNumber();
                level.Number = levelNumber;
                handler(this, level);
            }
        }

        private MenuNavigation GetMenuNavigation(KeyboardState keyboardState, int scrollWheelValue)
        {
            if ( IsKeyPressed(keyboardState, Keys.Down) || scrollWheelValue < _oldMouseState.ScrollWheelValue) 
                return MenuNavigation.Next;

            if (IsKeyPressed(keyboardState, Keys.Up) || scrollWheelValue > _oldMouseState.ScrollWheelValue)
                return MenuNavigation.Previous;

            return MenuNavigation.None;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            MenuView.Draw(spriteBatch);
        }

        internal void LoadContent()
        {
            MenuView.LoadContent(_content);
        }


    }

    public class LevelNumber : EventArgs
    {
        private int _number;
        public int Number
        {
            set
            {
                _number = value;
            }
            get
            {
                return this._number;
            }
        }
    }



}
