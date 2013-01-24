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

        public ComputerControlledTrooper(Vector2 startPosition, float faceDirection, float width, float height, int speed, List<Vector2> levelPositions, int health = 30 )
        : base(startPosition, faceDirection, width, height, speed, health)
        {
            _isControlledByComputer = true;
            _levelPositions = levelPositions;

        }

        public override void Update(GameTime gameTime, IEnumerable<Trooper> troopers, IEnumerable<Building> buildings )
        {
            _timeSinceLastAction += (float) gameTime.ElapsedGameTime.TotalSeconds;
            NumberOfBullets = 20;

            Trooper nearestEnemy = GetNearestEnemy(troopers, Position);
            float nearestEnemySquaredDistance = Vector2.DistanceSquared(nearestEnemy.Position, Position);
            if (nearestEnemySquaredDistance >= 625f || TargetPositionIsBlockedOrInsideBuilding(buildings, nearestEnemy.CenterPosition) && (Time >= 3 || Position != TargetPosition))
            {
                Vector2 gotoPosition = GetGotoPosition(troopers, buildings, 3f);
                Update(gameTime, GetFaceDirection(gotoPosition), gotoPosition, Position == TargetPosition, false);
            }
            else if (Time >= 3 && nearestEnemySquaredDistance < 81f)
            {
              
                ShootingTarget = nearestEnemy;
                Update(gameTime, ShootingTarget.CenterPosition, ShootingTarget.Position, true, true);

            }
            else if (Time > 6 || Position != TargetPosition)
            {
                Update(gameTime, GetFaceDirection(GetGotoPosition(troopers, buildings)), GetGotoPosition(troopers, buildings), Position == TargetPosition, false);    
            }
            else if (Time >= 3)
            {
             
                ShootingTarget = nearestEnemy;
                Update(gameTime, ShootingTarget.CenterPosition, ShootingTarget.Position, true, true);
            }
            else
            {
                Update(gameTime, ShootingTarget.CenterPosition, ShootingTarget.Position,false,false);
            }
            if (_timeSinceLastAction > 0.5f)
            {
                _timeSinceLastAction = 0f;
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

        private Vector2 GetGotoPosition(IEnumerable<Trooper> troopers,IEnumerable<Building> buildings  , float timeToSpend = 6f)
        {
            IEnumerable<Vector2> positions = _levelPositions.Where(p => TargetPositionIsWithinRightDistance(timeToSpend, p) && !TargetPositionIsBlockedOrInsideBuilding(buildings, GridFunctions.GetCenterPosition(p)));
           

            
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

        private bool TargetPositionIsBlockedOrInsideBuilding(IEnumerable<Building> buildings, Vector2 position)
        {
            foreach (Building building in buildings)
            {
                if (building.IsBetweenPosition(position, CenterPosition))
                {
                    return true;
                }

            }
            return false;
        }

        private bool TargetPositionIsWithinRightDistance(float timeToSpend, Vector2 p)
        {
            return this.GetDistanceSquared(p) == timeToSpend * timeToSpend;
        }

        private Vector2 GetFaceDirection(Vector2 gotoPosition)
        {
             
            return new Vector2(gotoPosition.X + 0.5f, gotoPosition.Y + 0.5f);
        }
    }

    internal static class GridFunctions
    {
        public static Vector2 GetCenterPosition(Vector2 position)
        {
            return new Vector2(position.X + 0.5f, position.Y + 0.5f);
        }
    }
}
