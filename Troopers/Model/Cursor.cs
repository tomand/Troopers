using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Troopers.Model
{
    class Cursor
    {
        public Cursor(Vector2 position, float side)
        {
            Position = position;
            Side = side;

        }


        public Vector2 Position { get; set; }
        public float Side { get; set; }
        public float Width { get { return Side; } }
        public float Height { get { return Side ; } }
        public Distance DistanceGrade { get; set; }
        public bool MarksEnemyTrooper { get; set; }

        public Vector2 CenterPosition
        {
            get { return new Vector2(Position.X + Width / 2, Position.Y + Height /2); }
        }

        public bool BlockedByBuilding { get; set; }


        internal void UpdatePosition(Vector2 mousePosition, int levelWidth, int levelHeight)
        {
            Position += 1f * (mousePosition - Position);
            Position = new Vector2((float)Math.Floor(MathHelper.Clamp(Position.X, 0f, levelWidth - Side)), (float)Math.Floor(MathHelper.Clamp(Position.Y, 0f, levelHeight - Side)));
        }
    }

    public enum Distance
    {Close, Medium, Far}
}
