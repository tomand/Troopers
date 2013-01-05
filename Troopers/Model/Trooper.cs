using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Troopers.Model
{
    class Trooper
    {
        private Vector2 _position;
        Vector2 _direction;
        private int _time;
        private bool _current;
        protected bool _isControlledByComputer = false;
        private Weapon _weapon;

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        } 
        //public Vector2 Position { get; set; }

        public float Width { get; set; }
        public float Height { get; set; }
        public Vector2 Velocity { get; set; }
        public float FaceDirection { get; set; }
        public Vector2 TargetPosition { get; set; }
        public int Speed { get; set; }

        public bool Current
        {
            get { return _current; } 
            set 
            { 
                if (value) 
                    InitiateNewTurn();
                _current = value;
        } }

        private void InitiateNewTurn()
        {
           _time = 9;
        }


        public Trooper(Vector2 startPosition,float faceDirection ,float width, float height, int speed)
        {
            Position = startPosition;
            TargetPosition = startPosition;
            Width = width;
            Height = height;
            FaceDirection =  faceDirection;
            _time = 9;
           
            Speed = speed;
            _weapon = new Weapon();
        }


        public virtual void Update(GameTime gameTime)
        {
        }

        public void Update(GameTime gameTime, Vector2 cursorCenterPosition, Vector2 cursorPosition, bool mouseClicked, bool enemyIsMarked)
        {
            if (!enemyIsMarked && mouseClicked && GetDistanceSquared(cursorPosition) <= _time * _time)
            {
                FacePosition(cursorCenterPosition);
                TargetPosition = cursorPosition;
                _direction = TargetPosition - Position;
                _time = _time - (int)Math.Ceiling( Math.Sqrt(GetDistanceSquared(cursorPosition)));
            }

            if (enemyIsMarked && mouseClicked)
            {
                FacePosition(cursorCenterPosition);
                Shoot();
            }

            if (!TargetIsReached())
            {
                UpdatePosition(gameTime);
            }
            else
            {
                Position = TargetPosition;
            }

         
            // FaceDirection += 0.01f;
            //if (_position.X >= 50f)
            //{
            //    _position.X = 1f;
            //}
            //if (_position.Y >= 50f || _position.Y == 1f)
            //{
            //    _position.Y = 1f;
            //}

            //_position.Y += 0.1f;
            //if (_position.X != 1.0f)
            //_position.X += 0.1f;
        }

        private void Shoot()
        {
            _time = _time - _weapon.TimeToShoot;
            _weapon.Fire(FaceDirection, CenterPosition);

        }

        private float GetDistanceSquared(Vector2 distantPosition)
        {
            if (_position.Equals(distantPosition))
            {
                return 0;
            }

           return Math.Max(1, Vector2.DistanceSquared(Position, distantPosition));
        }

        private bool TargetIsReached()
        {
            return (Math.Abs(Position.X - TargetPosition.X) < 0.2 && Math.Abs(Position.Y - TargetPosition.Y) < 0.2);
          //  return Position.Equals(TargetPosition);
        }

        private void UpdatePosition(GameTime gameTime)
        {
            _direction.Normalize();
            _position += _direction * 3.2f * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public Vector2 CenterPosition
        {
            get { return new Vector2(Position.X - Width / 2, Position.Y - Height / 2); }
        }

        public bool HasNoTimeLeft
        {
            get { return _time == 0 && (Position == TargetPosition); }
            
        }

        public  bool IsControlledByComputer
        {
            get { return _isControlledByComputer; }          
        }


        public void FacePosition(Vector2 positionToFace)
        {
          //  positionToFace = new Vector2(1f, 26.5f);
            Vector2 direction = positionToFace - CenterPosition;
            FaceDirection = (float)(Math.Atan2(direction.Y, direction.X) + (Math.PI/2) );
        }

        public Distance GetDistanceGrade(Vector2 distantPosition)
        {
            int squaredDistance = (int) Math.Ceiling( GetDistanceSquared(distantPosition));

            if (squaredDistance > _time * _time)
                return Distance.Far;
            else if (squaredDistance > (_time - _weapon.TimeToShoot) * (_time - _weapon.TimeToShoot))
                return Distance.Medium;

            return Distance.Close;
        }
    }
}
