using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Troopers.Model;

namespace Troopers.View
{
    class HelpView : GameObjectView
    {
        private Help _help;
        private SpriteFont font;


        public HelpView(Camera camera, Help help)
            :base(camera)
        {
            _help = help;
        }


        internal void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            font = content.Load<SpriteFont>("HelpFont");
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, _help.HelpText, Camera.Transform(_help.Position), Color.Orange);
        }
    }
}
