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

        public bool IsAlive { get; set; }

        public Vector2 CurrentPosition
        {
            get { return _currentPosition; }
            set { _currentPosition = value; }
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
            _damage = 5;
            _maxDistanceSquared = 900;
        }

        public void Update(GameTime gameTime)
        {
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
            return Math.Max(0, _damage - (int)Math.Floor((float)_damage * (GetSquaredDistanceFromStartingPoint() /  (float)_maxDistanceSquared )));
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