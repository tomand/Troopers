using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Troopers.Model
{
    class ComputerControlledTrooper : Trooper
    {

        public ComputerControlledTrooper(Vector2 startPosition, float faceDirection, float width, float height, int speed)
        : base(startPosition, faceDirection, width, height, speed)
        {
            _isControlledByComputer = true;
        }

        public override void Update(GameTime gameTime)
        {
            Update(gameTime, GetFaceDirection(), GetGotoPosition(), Position == TargetPosition, false);
        }

        private Vector2 GetGotoPosition()
        {
            var y = Position.Y + 9;
            if (y > 10f)
            {
                y = 1f;
            }
            return new Vector2(Position.X, y);
        }

        private Vector2 GetFaceDirection()
        {
            Vector2 gotoPosition = GetGotoPosition();
            return new Vector2(gotoPosition.X - 0.5f, gotoPosition.Y - 0.5f);
        }
    }
}
