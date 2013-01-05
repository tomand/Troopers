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

        public Cursor Cursor
        {
            get { return _cursor; }
        }

        List<Trooper> _troopers;
        private readonly Cursor _cursor;
        private Random _random;
        private int _nextActiveTrooper;

        public Level(int width, int height, Vector2 position)
        {
            _random = new Random();
            _position = position;
            _width = width;
            _height = height;
            _cursor = new Cursor(new Vector2(0, 0),1f);
            _troopers = new List<Trooper>();
            //for (int i = 2; i < 26; i += 2 )
            //{
          
             _troopers.Add(new Trooper(new Vector2(1f,28f), 90f, 1f, 1f, _random.Next(100,200)));
             _troopers.Add(new ComputerControlledTrooper(new Vector2(28f, 1f), 90f, 1f, 1f, _random.Next(100, 200)));

            _troopers.First().Current = true;
            //}

            //for (int i = 2; i < 26; i += 2)
            //{
            //    _troopers.Add(new Trooper(new Vector2((float)i, 2f), 45f, 2f, 2f));

            //}
            //for (int i = 26; i < 51; i += 1)
            //{
            //    _troopers.Add(new Trooper(new Vector2((float)i, 2f), 45f, 1f, 1f));

            //}
        }

        internal IEnumerable<Trooper> GetTroopers()
        {
            return _troopers;
        }


        public void Update(GameTime gameTime, Vector2 mousePosition, bool mouseClicked)
        {
            Trooper trooper = GetCurrentTrooper();
            
            _cursor.UpdatePosition(mousePosition, Width, Height);
            _cursor.MarksEnemyTrooper = IsComputerControlledTrooperOnPosition(_cursor.Position);
            if (trooper.IsControlledByComputer)
            {
                trooper.Update(gameTime);
            }
            else
            {
                trooper.Update(gameTime, _cursor.CenterPosition, _cursor.Position, mouseClicked, _cursor.MarksEnemyTrooper);
            }
            
            _cursor.DistanceGrade = trooper.GetDistanceGrade(_cursor.Position);

            if (trooper.HasNoTimeLeft) 
                UpdateWhoIsCurrent(trooper);
        }

        private bool IsComputerControlledTrooperOnPosition(Vector2 position)
        {
            return _troopers.Exists(t => t.Position.Equals(position) && t.IsControlledByComputer);
        }

        private void UpdateWhoIsCurrent(Trooper trooper)
        {
            trooper.Current = false;
            GetNextTrooper().Current = true;
            SetNextActiveTrooper();
        }

        private Trooper GetCurrentTrooper()
        {
            return _troopers.Find(t => t.Current);
        }

        private void SetNextActiveTrooper()
        {
            _nextActiveTrooper++;
            if (_nextActiveTrooper == _troopers.Count)
                _nextActiveTrooper = 0;
        }

        private Trooper GetNextTrooper()
        {
            return _troopers.OrderByDescending(t => t.Speed).ElementAt(_nextActiveTrooper);
        }

    }
}
