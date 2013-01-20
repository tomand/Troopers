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
        private LevelManager _levelManager;
        private TrooperView _trooperView;
        private CursorView _cursorView;
        private MediKitView _mediKitView;
        private BuildingView _buildingView;


        private Level CurrentLevel { get { return _levelManager.CurrentLevel; } }

        public LevelView(GraphicsDevice graphicsDevice, ContentManager content,  LevelManager levelManager, Camera cam)
            : base(cam)
        {
            _levelManager = levelManager;
            _trooperView = new TrooperView(cam);
            _cursorView = new CursorView(cam);
            _mediKitView = new MediKitView(cam);
            _buildingView = new BuildingView(cam);

        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
         
            _cursorView.Cursor = CurrentLevel.Cursor;

            DrawLevelBackground(spriteBatch);

            DrawBuildings(spriteBatch, gameTime);


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

        private void DrawLevelBackground(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch,
                      new Rectangle(Camera.TransformX(CurrentLevel.Position.X),
                                    Camera.TransformY(CurrentLevel.Position.Y)
                                    , Camera.TransformSizeX(CurrentLevel.Width)
                                    , Camera.TransformSizeY(CurrentLevel.Height)));
        }

        private void DrawBuildings(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var building in CurrentLevel.GetBuildings())
            {
                _buildingView.Draw(spriteBatch, gameTime, building);
            }
        }

        internal void LoadContent(ContentManager content)
        {
            GameObjectTexture = content.Load<Texture2D>("ground");
            _trooperView.LoadContent(content);
            _cursorView.LoadContent(content);
            _mediKitView.LoadContent(content);
            _buildingView.LoadContent(content);
        }
    }
}
