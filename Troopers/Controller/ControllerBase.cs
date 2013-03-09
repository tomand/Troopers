using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Troopers.Controller
{
    class ControllerBase
    {
        protected int _viewportWidth;
        protected int _viewportHeight;
        protected GraphicsDevice _graphicsDevice;
        protected ContentManager _content;
        protected KeyboardState _oldKeyboardState;
        protected MouseState _oldMouseState;
        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (value)
                    _oldMouseState = Mouse.GetState();
                _isActive = value;
            }
        }

        protected bool IsKeyPressed(KeyboardState keyboardState, Keys key)
        {
            return keyboardState.IsKeyUp(key) && _oldKeyboardState.IsKeyDown(key);
        }

        protected bool IsMouseLeftClicked(MouseState mouseState)
        {
            return (mouseState.LeftButton == ButtonState.Pressed && _oldMouseState.LeftButton != ButtonState.Pressed);
        }

        private bool IsMouseRightClicked(MouseState mouseState)
        {
            return (mouseState.RightButton == ButtonState.Pressed && _oldMouseState.RightButton != ButtonState.Pressed);
        }
    }
}
