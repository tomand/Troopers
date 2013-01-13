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

        public bool IsFinished
        {
            get { return _troopers.Count(t => t.IsControlledByComputer && t.IsAlive) == 0 || _troopers.Count(t => !t.IsControlledByComputer && t.IsAlive) == 0; }
          
        }

        public bool PlayerWon
        {
            get { return IsFinished && _troopers.Count(t => !t.IsControlledByComputer && t.IsAlive) > 0; }
        }

        public bool Current { get; set; }
        List<Trooper> _troopers;
        private  Cursor _cursor;
        private readonly Random _random;
        private int _nextActiveTrooper = 1;
        private string _levelId;

        public Level(int width, int height, Vector2 position, string levelId)
        {
            _random = new Random();
            _position = position;
            _width = width;
            _height = height;
            _cursor = new Cursor(new Vector2(0, 0), 1f);
            _levelId = levelId;
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
            return _troopers.Where(t => t.IsAlive);
        }


        public void Update(GameTime gameTime, Vector2 mousePosition, bool mouseClicked)
        {
            Trooper trooper = GetCurrentTrooper();
            
            _cursor.UpdatePosition(mousePosition, Width, Height);
            _cursor.MarksEnemyTrooper = IsComputerControlledTrooperOnPosition(_cursor.Position);
            if (trooper.IsControlledByComputer)
            {
                trooper.Update(gameTime, GetPlayerControlledTroopers());
            }
            else
            {
                trooper.Update(gameTime, _cursor.CenterPosition, _cursor.Position, mouseClicked, _cursor.MarksEnemyTrooper);
                if (_cursor.MarksEnemyTrooper)
                {
                    trooper.ShootingTarget = GetTrooperOnPosition(_cursor.Position);
                }
            }
            
            _cursor.DistanceGrade = trooper.GetDistanceGrade(_cursor.Position);

            if (trooper.HasNoTimeLeft) 
                UpdateWhoIsCurrent(trooper);
        }

        private Trooper GetTrooperOnPosition(Vector2 position)
        {
            return _troopers.Find(t => t.Position.Equals(position));
        }

        private IEnumerable<Trooper> GetPlayerControlledTroopers()
        {
            return _troopers.Where(t => !t.IsControlledByComputer);
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
            _nextActiveTrooper = Math.Min(_nextActiveTrooper, _troopers.Count(t => t.IsAlive) - 1);
            return _troopers.Where(t => t.IsAlive).OrderByDescending(t => t.Speed).ElementAt(_nextActiveTrooper);
        }

        public void Start()
        {
            _nextActiveTrooper = 0;
           
            _troopers = new List<Trooper>();
            
            LoadLevelData();

            GetNextTrooper().Current = true;
            SetNextActiveTrooper();
        }

        private void LoadLevelData()
        {
            switch (_levelId)
            {
                case "level1":
                    {
                        _troopers.Add(new Trooper(new Vector2(1f, 28f), 90f, 1f, 1f, _random.Next(100, 200)));
                        _troopers.Add(GetComputerControlledTrooper(new Vector2(28f, 1f), 90f, 1f, 1f, _random.Next(100, 200)));
                        break;
                    }
                case "level2":
                    {
                        _troopers.Add(new Trooper(new Vector2(1f, 1f), 90f, 1f, 1f, _random.Next(100, 200)));
                        _troopers.Add(GetComputerControlledTrooper(new Vector2(28f, 1f), 90f, 1f, 1f, _random.Next(100, 200)));
                        break;
                    }
                case "level3":
                    {
                        _troopers.Add(new Trooper(new Vector2(28f, 28f), 90f, 1f, 1f, _random.Next(100, 200)));
                        _troopers.Add(GetComputerControlledTrooper(new Vector2(28f, 1f), 90f, 1f, 1f, _random.Next(100, 200)));
                        break;
                    }
            }
            
        }

        private ComputerControlledTrooper GetComputerControlledTrooper(Vector2 startPosition, float faceDirection, float width, float height, int speed)
        {
            return new ComputerControlledTrooper(startPosition, faceDirection, width, height, speed, GetAllPositions());
        }

        private List<Vector2> GetAllPositions()
        {
            List<Vector2> positions = new List<Vector2>();
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    positions.Add(new Vector2(i,j));
                }
            }
            return positions;
        }
    }
}
