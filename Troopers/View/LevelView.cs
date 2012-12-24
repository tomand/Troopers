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
        private TrooperView _trooperView;
        public LevelView(GraphicsDevice graphicsDevice, ContentManager content,  List<Level> levels, Camera cam)
            : base(graphicsDevice, content, cam)
        {
            _levels = levels;
            _trooperView = new TrooperView(graphicsDevice, content, cam);


        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch,
                new Rectangle(Camera.TransformX(_levels.First<Level>().Position.X),
                    Camera.TransformY(_levels.First<Level>().Position.Y)
                    , Camera.TransformSizeX(_levels.First<Level>().Width)
                    , Camera.TransformSizeY(_levels.First<Level>().Height)));

            foreach (Trooper trooper in _levels.First<Level>().GetTroopers())
            {
                _trooperView.Draw(spriteBatch, gameTime, trooper);
            }
        }

        internal void LoadContent(ContentManager content)
        {
            GameObjectTexture = content.Load<Texture2D>("ground");
            _trooperView.LoadContent(content);
        }
    }
}
