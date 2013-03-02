using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Troopers.Model;

namespace Troopers.View
{
    class MovementView : GameObjectView
    {
        private Texture2D _tileMark;
        private SpriteFont _font;
        private List<Vector2> _levelPositions;

        public MovementView(Camera cam)
            : base(cam)
        {
           //_levelPositions = levelPositions;
        }

        public void Draw(SpriteBatch spriteBatch, Trooper trooper, List<Vector2> levelPositions)
        {
            foreach (var levelPosition in levelPositions.Where(p => Vector2.DistanceSquared(p, trooper.Position) <= trooper.Time * trooper.Time ))
            {
                DrawDistancePosition(spriteBatch, levelPosition, Vector2.DistanceSquared(levelPosition, trooper.Position), trooper.GetDistanceGrade(levelPosition));
            }
        }

        private void DrawDistancePosition(SpriteBatch spriteBatch, Vector2 levelPosition, float squaredDistance, Distance distanceGrade )
        {
            DestinationRectangle = new Rectangle(Camera.TransformX(levelPosition.X),
                   Camera.TransformY(levelPosition.Y)
                   , Camera.TransformSizeX(1)
                   , Camera.TransformSizeY(1));


            Rectangle? sourceRectangle = new Rectangle(GetSourceXValue(distanceGrade), 0, GameObjectTexture.Height, GameObjectTexture.Height);
            spriteBatch.Draw(GameObjectTexture, DestinationRectangle, sourceRectangle, Color.White);
            spriteBatch.DrawString(_font, Math.Ceiling(Math.Sqrt(squaredDistance)).ToString(), new Vector2(DestinationRectangle.X, DestinationRectangle.Y),
                                   Color.Black);
        }

        private int GetSourceXValue(Distance distanceGrade)
        {
           int returnValue = 0;
           switch (distanceGrade)
            {
                case Distance.Close:
                    returnValue = 0;
                    break;
                case Distance.Medium:
                    returnValue = 27;
                    break;
                default:
                    returnValue = 54;
                    break;

            }
            return returnValue;
        }

        internal void LoadContent(ContentManager content)
        {
            GameObjectTexture = content.Load<Texture2D>("tilemark");
            _font = content.Load<SpriteFont>("TrooperInfoFont");
        }
    }
}
