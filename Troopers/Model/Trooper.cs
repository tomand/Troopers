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
        private int _timeToShoot;
        private bool _current;

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


        public Trooper(Vector2 startPosition,float faceDirection ,float width, float height)
        {
            Position = startPosition;
            TargetPosition = startPosition;
            Width = width;
            Height = height;
            FaceDirection =  faceDirection;
            _time = 9;
            _timeToShoot = 3;
        }

        public void Update(GameTime gameTime, Vector2 cursorCenterPosition, Vector2 cursorPosition, bool startMoving)
        {
            if (startMoving && GetDistanceSquared(cursorPosition) <= _time * _time)
            {
                FacePosition(cursorCenterPosition);
                TargetPosition = cursorPosition;
                _direction = TargetPosition - Position;
                _time = _time - (int)Math.Ceiling( Math.Sqrt(GetDistanceSquared(cursorPosition)));
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
            get { return _time == 0; }
            
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
            else if (squaredDistance > (_time - _timeToShoot) * (_time  - _timeToShoot))
                return Distance.Medium;

            return Distance.Close;
        }
    }
}
