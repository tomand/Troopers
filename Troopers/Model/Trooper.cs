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
            Width = width;
            Height = height;
            FaceDirection = 0f; 
        }

        public void Update(GameTime gameTime)
        { 
            FaceDirection += 0.01f;
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

        
    }
}
