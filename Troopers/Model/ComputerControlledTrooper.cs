using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Troopers.Model
{
    class ComputerControlledTrooper : Trooper
    {
        private List<Vector2> _levelPositions;

        public ComputerControlledTrooper(Vector2 startPosition, float faceDirection, float width, float height, int speed, List<Vector2> levelPositions )
        : base(startPosition, faceDirection, width, height, speed)
        {
            _isControlledByComputer = true;
            _levelPositions = levelPositions;

        }

        public override void Update(GameTime gameTime, IEnumerable<Trooper> troopers)
        {
            Trooper nearestEnemy = GetNearestEnemy(troopers, Position);
            float nearestEnemySquaredDistance = Vector2.DistanceSquared(nearestEnemy.Position, Position);
            if (nearestEnemySquaredDistance >= 625f && (_time >= 3 || Position != TargetPosition))
            {
                Update(gameTime, GetFaceDirection(GetGotoPosition(troopers, 3f)), GetGotoPosition(troopers, 3f), Position == TargetPosition, false);
            }
            else if (_time >= 3 && nearestEnemySquaredDistance < 81f)
            {
                ShootingTarget = nearestEnemy;
                Update(gameTime, ShootingTarget.CenterPosition, ShootingTarget.Position, true, true);

            }
            else if (_time > 6 || Position != TargetPosition)
            {
                Update(gameTime, GetFaceDirection(GetGotoPosition(troopers)), GetGotoPosition(troopers), Position == TargetPosition, false);    
            }
            else if (_time >= 3)
            {
                ShootingTarget = nearestEnemy;
                Update(gameTime, ShootingTarget.CenterPosition, ShootingTarget.Position, true, true);
            }
            else
            {
                Update(gameTime, ShootingTarget.CenterPosition, ShootingTarget.Position,false,false);
            }
            
        }

        private Trooper GetNearestEnemy(IEnumerable<Trooper> troopers, Vector2 position)
        {
            Trooper closestTrooper = null;
            float squaredDistance = float.MaxValue;
            
            foreach (Trooper trooper in troopers.Where(t => !t.IsControlledByComputer && t.IsAlive))
            {
                if (Vector2.DistanceSquared(trooper.Position, position) < squaredDistance)
                {
                    squaredDistance = Vector2.DistanceSquared(trooper.Position, position);
                    closestTrooper = trooper;
                }
            }
            return closestTrooper;
        }

        private Vector2 GetGotoPosition(IEnumerable<Trooper> troopers, float timeToSpend = 6f)
        {
            IEnumerable<Vector2> positions = _levelPositions.Where(p => this.GetDistanceSquared(p) == timeToSpend * timeToSpend);
           

            Vector2 bestPosition = new Vector2();
            float squaredDistance = float.MaxValue;
            foreach (var position in positions)
            {
                Trooper nearest = GetNearestEnemy(troopers, position);
                if (troopers.Count(t => t.IsAlive && t.Position.Equals(position)) == 0 && Vector2.DistanceSquared(nearest.Position, position) < squaredDistance && Vector2.DistanceSquared(nearest.Position, position) > 0)
                {
                    squaredDistance = Vector2.DistanceSquared(nearest.Position, position);
                    bestPosition = position;
                }
                  
            }

            return bestPosition;
        }

        private Vector2 GetFaceDirection(Vector2 gotoPosition)
        {
             
            return new Vector2(gotoPosition.X + 0.5f, gotoPosition.Y + 0.5f);
        }
    }
}
