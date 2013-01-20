using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Troopers.Model
{
    public class Building
    {
        public float Height { get; set; }

        public float Width { get; set; }

        public Vector2 Position { get; set; }

        public Building(Vector2 position, float width, float height)
        {
            Position = position;
            Width = width;
            Height = height;
        }

        public bool CoversPosition(Vector2 position)
        {
            var rectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height);
            return rectangle.Contains((int)position.X, (int)position.Y);
        }

        public bool IsBetweenPosition(Vector2 position, Vector2 vector2)
        {
            foreach (Line line in GetWalls())
            {
                if (line.Intersects(new Line(position, vector2)))
                    return true;
                
            }
            return false;
        }

        private IEnumerable<Line> GetWalls()
        {
            List<Line> walls = new List<Line>();
            walls.Add(new Line(Position, new Vector2(Position.X + Width, Position.Y)));
            walls.Add(new Line(Position, new Vector2(Position.X, Position.Y + Height)));
            walls.Add(new Line(new Vector2(Position.X, Position.Y + Height), new Vector2(Position.X + Width, Position.Y + Height)));
            walls.Add(new Line(new Vector2(Position.X + Width, Position.Y), new Vector2(Position.X + Width, Position.Y + Height)));
            return walls;
        }
    }
}
