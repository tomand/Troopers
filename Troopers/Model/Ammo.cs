using Microsoft.Xna.Framework;

namespace Troopers.Model
{
    class Ammo
    {
        public Vector2 Position { get; set; }
        public bool IsTaken { get; set; }
        public int NumberOfBullets { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public Ammo(Vector2 position)
        {
            Position = position;
            IsTaken = false;
            NumberOfBullets = 10;
            Width = 1f;
            Height = 1f;
        }

      
    }
}