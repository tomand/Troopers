using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Troopers.Model;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Troopers.View
{
    class LevelView : GameObjectView
    {
        private List<Level> _levels;
        public LevelView(GraphicsDevice graphicsDevice, ContentManager content,  List<Level> levels, Camera cam)
            : base(graphicsDevice, content, cam)
        {
            _levels = levels;
            
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch,
                new Rectangle(_camera.TransformX(_levels.First<Level>().Position.X),
                    _camera.TransformY(_levels.First<Level>().Position.Y)
                    , _camera.TransformSizeX(_levels.First<Level>().Width)
                    , _camera.TransformSizeY(_levels.First<Level>().Height)));
        }

        internal void LoadContent(ContentManager content)
        {
            GameObjectTexture = content.Load<Texture2D>("ground");
        }
    }
}
