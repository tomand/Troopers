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
        private List<TrooperHitView> _trooperHitViews;
        private SpriteFont _font;


        private Level CurrentLevel { get { return _levelManager.CurrentLevel; } }

        public LevelView(GraphicsDevice graphicsDevice, ContentManager content,  LevelManager levelManager, Camera cam)
            : base(cam)
        {
            _levelManager = levelManager;
            _trooperView = new TrooperView(cam);
            _cursorView = new CursorView(cam);
            _mediKitView = new MediKitView(cam);
            _buildingView = new BuildingView(cam);
            _trooperHitViews = new List<TrooperHitView>();

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
                if (trooper.LifeChange < 0)
                {
                    AddTrooperHitView(trooper.CenterPosition, trooper.LifeChange, gameTime);
              //      spriteBatch.DrawString(_font, trooper.LifeChange.ToString(), Camera.Transform(GetDamageTextPosition(trooper.CenterPosition)), Color.Red);
                    trooper.ResetLifeChange();
                }
            }

           DrawHits(spriteBatch, gameTime);

        }

        private void AddTrooperHitView(Vector2 centerPosition, int lifeChange, GameTime gameTime)
        {
            _trooperHitViews.Add(new TrooperHitView(Camera, lifeChange.ToString(), GetDamageTextPosition(centerPosition), _font, gameTime));
           

        }

        private Vector2 GetDamageTextPosition(Vector2 centerPosition)
        {
            Vector2 position = new Vector2();
            position.X = centerPosition.X - 0.5f;
            position.Y = (centerPosition.Y < 1 ? centerPosition.Y + 0.5f : centerPosition.Y - 1.5f);
            return position;
        }

        private void DrawHits(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _trooperHitViews.RemoveAll(h => !h.IsAlive);
            foreach (var trooperHitView in _trooperHitViews.Where(h => h.IsAlive))
            {
                trooperHitView.Draw(spriteBatch, gameTime);
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
            _font = content.Load<SpriteFont>("TrooperInfoFont");
        }
    }
}
