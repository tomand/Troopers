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

        public override void Update(GameTime gameTime, IEnumerable<Trooper> troopers)
        {
            if (_time > 3 || Position != TargetPosition)
            {
                Update(gameTime, GetFaceDirection(GetGotoPosition()), GetGotoPosition(), Position == TargetPosition, false);    
            }
            else
            {
                ShootingTarget = GetNearestEnemy(troopers);
                Update(gameTime, GetFaceDirection(ShootingTarget.Position), ShootingTarget.Position, true, true);
                
            }
            
        }

        private Trooper GetNearestEnemy(IEnumerable<Trooper> troopers)
        {
            return troopers.First(t => !t.IsControlledByComputer);
        }

        private Vector2 GetGotoPosition()
        {
            var x = Position.X;
            var y = Position.Y;
             
            if (y < 25f && x == 28f)
            {
                y = y  + 6;
            }
            else if (y == 25f && x > 4f)
            {
                x = x - 6;
            }
            else if (y > 1f && x == 4f)
            {
                y = y  - 6;
            }
            else if (y == 1f && x < 28f)
            {
                x = x + 6;
            }

            return new Vector2(x, y);
        }

        private Vector2 GetFaceDirection(Vector2 gotoPosition)
        {
             
            return new Vector2(gotoPosition.X - 0.5f, gotoPosition.Y - 0.5f);
        }
    }
}
