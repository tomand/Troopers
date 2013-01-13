using System;
using Microsoft.Xna.Framework;

namespace Troopers.Model
{
    public class Bullet
    {
        private Vector2 _startPosition;
        private Vector2 _currentPosition;
        private Vector2 _targetPosition;
        private Vector2 _direction;
        private Trooper _target;
        private int _damage;
        private int _maxDistanceSquared;
        private int _age;

        public bool IsAlive { get; set; }

        public Vector2 CurrentPosition
        {
            get { return _currentPosition; }
            set { _currentPosition = value; }
        }

        public int Age
        {
            get { return _age; }
        }

        public Bullet(Vector2 centerPosition,  Trooper target)
        {
            _startPosition = centerPosition;
            CurrentPosition = centerPosition;
            _targetPosition = target.CenterPosition;
            _direction = _targetPosition - CurrentPosition;
            _direction.Normalize();
            _target = target;
            IsAlive = true;
            _damage = 8;
            _maxDistanceSquared = 900;
            _age = 0;
        }

        public void Update(GameTime gameTime)
        {
            _age++;
            if (!TargetIsReached())
           {
               UpdatePosition(gameTime);
           }
           else
            {
               _target.Hit(CalculateDamage());
               IsAlive = false;
           }
        }

        private int CalculateDamage()
        {
            int damageBeforeRandomAdjustment = _damage -
                                               (int)
                                               Math.Floor((float) _damage*
                                                          (GetSquaredDistanceFromStartingPoint()/
                                                           (float) _maxDistanceSquared));
            int damageAfterRandomAdjustment = damageBeforeRandomAdjustment - Dice.Roll();
            return Math.Max(0, damageAfterRandomAdjustment);
        }

        private float GetSquaredDistanceFromStartingPoint()
        {
            return Vector2.DistanceSquared(_currentPosition, _startPosition);
        }

        private void UpdatePosition(GameTime gameTime)
        {
            CurrentPosition += _direction * 6.4f * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private bool TargetIsReached()
        {
            return (Math.Abs(CurrentPosition.X - _targetPosition.X) < 0.2 && Math.Abs(CurrentPosition.Y - _targetPosition.Y) < 0.2);
        }


    }
}