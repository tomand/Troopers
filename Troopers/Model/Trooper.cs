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

       

        public Trooper(Vector2 startPosition,float faceDirection ,float width, float height)
        {
            Position = startPosition;
            TargetPosition = startPosition;
            Width = width;
            Height = height;
            FaceDirection =  faceDirection; 
        }

        public void Update(GameTime gameTime, Vector2 cursorCenterPosition, Vector2 cursorPosition, bool startMoving)
        {
            if (startMoving)
            {
                FacePosition(cursorCenterPosition);
                TargetPosition = cursorPosition;
                _direction = TargetPosition - Position;
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

        public Vector2 TargetPosition
        {
            get; set; }

        public void FacePosition(Vector2 positionToFace)
        {
          //  positionToFace = new Vector2(1f, 26.5f);
            Vector2 direction = positionToFace - CenterPosition;
            FaceDirection = (float)(Math.Atan2(direction.Y, direction.X) + (Math.PI/2) );
        }
        
    }
}
