using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Troopers.Model
{
    class Level
    {
        private int _width;

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        private int _height;

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }
        private Vector2 _position;

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        List<Trooper> _troopers;

        public Level(int width, int height, Vector2 position)
        {
            _position = position;
            _width = width;
            _height = height;
            _troopers = new List<Trooper>();
            for (int i = 1; i < 51; i += 1 )
            {
          
                _troopers.Add(new Trooper(new Vector2(50f, (float)i), 45f, 1f, 1f));
            }

            for (int i = 1; i < 51; i += 1)
            {
                _troopers.Add(new Trooper(new Vector2((float)i, 1f), 45f, 1f, 1f));

            }
        }




        internal IEnumerable<Trooper> GetTroopers()
        {
            return _troopers;
        }
    }
}
