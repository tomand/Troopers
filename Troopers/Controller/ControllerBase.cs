﻿using System;
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
        public bool IsActive { get; set; }

        protected bool IsKeyPressed(KeyboardState keyboardState, Keys key)
        {
            return keyboardState.IsKeyUp(key) && _oldKeyboardState.IsKeyDown(key);
        }
    }
}
