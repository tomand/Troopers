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
    class HelpController : ControllerBase
    {
        private Help _help;
        public event EventHandler GoBack;
        private HelpView _helpView;

        public HelpController(int viewportWidth, int viewportHeight, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _viewportWidth = viewportWidth;
            _viewportHeight = viewportHeight;
            _graphicsDevice = graphicsDevice;
            _content = content;
            _help = new Help(new Vector2(0.1f, 0.1f));
            _helpView =
                new HelpView(new Camera(_viewportHeight, _viewportWidth, 0, 0, _viewportHeight, _viewportHeight, 1, 1),
                             _help);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (IsKeyPressed(keyboardState, Keys.Back) || IsMouseLeftClicked(mouseState))
                OnGoBack();
            
            _oldKeyboardState = keyboardState;
            _oldMouseState = mouseState;
        }

        private void OnGoBack()
        {
            EventHandler handler = GoBack;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _helpView.Draw(spriteBatch);
        }

        internal void LoadContent()
        {
            _helpView.LoadContent(_content);
        }

    }


}
