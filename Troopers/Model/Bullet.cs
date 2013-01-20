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

        public Bullet(Vector2 centerPosition,  Trooper target, int damage)
        {
            _startPosition = centerPosition;
            CurrentPosition = centerPosition;
            _targetPosition = target.CenterPosition;
            _direction = _targetPosition - CurrentPosition;
            _direction.Normalize();
            _target = target;
            IsAlive = true;
            _damage = damage;
           
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
               _target.Hit(_damage);
               IsAlive = false;
           }
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