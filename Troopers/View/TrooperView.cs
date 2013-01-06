﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Troopers.Model;

namespace Troopers.View
{
    class TrooperView : GameObjectView
    {
        private Texture2D _tileMark;
        private BulletView _bulletView;

        public TrooperView(Camera cam)
            : base(cam)
        {
            _bulletView = new BulletView(cam);
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime, Trooper trooper)
        {
            DestinationRectangle = new Rectangle(Camera.TransformX(trooper.Position.X),
                    Camera.TransformY(trooper.Position.Y)
                    , Camera.TransformSizeX(trooper.Width)
                    , Camera.TransformSizeY(trooper.Height));
           
         //   spriteBatch.Draw(GameObjectTexture, new Vector2(Camera.TransformX(trooper.Position.X), Camera.TransformY(trooper.Position.Y)), null, Color.White, trooper.FaceDirection, origin: 
            //Camera.Transform(new Vector2(trooper.Width / 2,trooper.Height / 2)), effects: SpriteEffects.None, layerDepth: 0);
        //    spriteBatch.Draw(GameObjectTexture, DestinationRectangle, Color.White);

            DrawTrooper(spriteBatch, trooper);
            DrawBullets(spriteBatch, trooper);

            if (trooper.Current)
            {
                DrawTileMark(spriteBatch);
            }
        }

        private void DrawBullets(SpriteBatch spriteBatch, Trooper trooper)
        {
            foreach (var bullet in trooper.Weapon.GetAliveBullets())
            {
                _bulletView.Draw(spriteBatch, bullet);
            }
           
        }

        private void DrawTrooper(SpriteBatch spriteBatch, Trooper trooper)
        {
            Vector2 origin = new Vector2(Camera.TransformSizeX(trooper.Width/2), Camera.TransformSizeY(trooper.Height/2));

            int x = DestinationRectangle.X + Camera.TransformSizeX(trooper.Width/2);
            int y = DestinationRectangle.Y + Camera.TransformSizeY(trooper.Height/2);
            spriteBatch.Draw(GameObjectTexture, new Rectangle(x, y, DestinationRectangle.Width, DestinationRectangle.Height),
                             GetTrooperSpriteSourceRectangle(trooper), Color.White, trooper.FaceDirection, origin,
                             SpriteEffects.None, 0);
        }

        private void DrawTileMark(SpriteBatch spriteBatch)
        {
            Rectangle? sourceRectangle = new Rectangle(0, 0, _tileMark.Height, _tileMark.Height);
            spriteBatch.Draw(_tileMark, DestinationRectangle, sourceRectangle, Color.White);
        }

        private Rectangle? GetTrooperSpriteSourceRectangle(Trooper trooper)
        {
            int x = 0;
            if (trooper.GetType() == typeof (ComputerControlledTrooper))
                x = 20;
            return new Rectangle(x, 0, GameObjectTexture.Height, GameObjectTexture.Height);
        }

        internal void LoadContent(ContentManager content)
        {
            GameObjectTexture = content.Load<Texture2D>("trooper");
            _tileMark = content.Load<Texture2D>("tilemark");
            _bulletView.LoadContent(content);
        }
    }
}
