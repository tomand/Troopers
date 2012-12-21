using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Troopers.Model
{
    class Trooper
    {
        public Vector2 Position { get; set; }

        public float Width { get; set; }
        public float Height { get; set; }
        public Vector2 Velocity { get; set; }
        public float FaceDirection { get; set; }

        public Trooper(Vector2 startPosition,float faceDirection ,float width, float height)
        {
            Position = startPosition;
            Width = width;
            Height = height;
            FaceDirection = faceDirection; 
        }


        
    }
}
