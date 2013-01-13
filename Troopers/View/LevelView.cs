using System;
using System.Collections;
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
        private CursorView _cursorView;
        private MediKitView _mediKitView;
        

        private Level CurrentLevel { get { return _levels.Find(l => l.Current); } }

        public LevelView(GraphicsDevice graphicsDevice, ContentManager content,  List<Level> levels, Camera cam)
            : base(cam)
        {
            _levels = levels;
            _trooperView = new TrooperView(cam);
            _cursorView = new CursorView(cam);
            _mediKitView = new MediKitView(cam);

        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
           

            _cursorView.Cursor = CurrentLevel.Cursor;
            base.Draw(spriteBatch,
                new Rectangle(Camera.TransformX(CurrentLevel.Position.X),
                    Camera.TransformY(CurrentLevel.Position.Y)
                    , Camera.TransformSizeX(CurrentLevel.Width)
                    , Camera.TransformSizeY(CurrentLevel.Height)));

            _cursorView.Draw(spriteBatch, gameTime);

            foreach (var mediKit in CurrentLevel.GetMediKits())
            {
                _mediKitView.Draw(spriteBatch, gameTime, mediKit);
            }

            foreach (Trooper trooper in CurrentLevel.GetTroopers())
            {
                _trooperView.Draw(spriteBatch, gameTime, trooper);
            }

            
        }

        internal void LoadContent(ContentManager content)
        {
            GameObjectTexture = content.Load<Texture2D>("ground");
            _trooperView.LoadContent(content);
            _cursorView.LoadContent(content);
            _mediKitView.LoadContent(content);
        }
    }
}
